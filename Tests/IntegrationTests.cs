using AutoFixture;
using InnoProducts.Models;
using InnoProducts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class IntegrationTests
    {

        private readonly IFixture _fixture;
        private readonly IUnitOfWork _unitOfWork; 
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=Inno;";

        public IntegrationTests()
        {
            _fixture = new Fixture();

          
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            var dbContext = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(dbContext); 

        }

        [Fact]
        public async Task Should_AddProductToDatabase()
        {
            // Arrange 
            var product = _fixture.Create<Product>(); // Generate a product using AutoFixture
            product.Name = "Test Product";
            product.Price = 19;
            product.Availability = true;
            product.CreatorUserID = "bd60ada5-43bc-46e7-92ea-5da960479d7f"; // Example UUID as string for creator

            // Ensure CreationDate is in UTC
            product.CreationDate = product.CreationDate.Kind == DateTimeKind.Utc
                                    ? product.CreationDate
                                    : product.CreationDate.ToUniversalTime(); // Convert to UTC if not already in UTC

            // Act 
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            // Assert - Check if product is added to the database
            var savedProduct = await _unitOfWork.Products.GetByIdAsync(product.ID);

            Assert.NotNull(savedProduct); // Ensure the product is found
            Assert.Equal(product.Name, savedProduct.Name); // Assert that the name matches
            Assert.Equal(product.Price, savedProduct.Price); // Assert that the price matches
            Assert.Equal(product.Availability, savedProduct.Availability); // Assert that availability matches
        }

        [Fact]
        public async Task Should_DeleteProductToDatabase()
        {
         
            // Arrange 
            var product = _fixture.Create<Product>(); // Generate a product using AutoFixture
            product.Name = "Test Product";
            product.Price = 19;
            product.Availability = true;
            product.CreatorUserID = "bd60ada5-43bc-46e7-92ea-5da960479d7f"; // Example UUID as string for creator

          
            product.CreationDate = product.CreationDate.Kind == DateTimeKind.Utc
                                    ? product.CreationDate
                                    : product.CreationDate.ToUniversalTime(); // Convert to UTC if not already in UTC

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            // Act
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            // Assert - Check if the product is deleted by attempting to retrieve it
            var deletedProduct = await _unitOfWork.Products.GetByIdAsync(product.ID);

            // Assert that the product no longer exists in the database
            Assert.Null(deletedProduct); // The product should be null since it's deleted

        }
    }
}