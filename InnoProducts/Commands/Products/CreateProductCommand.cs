using InnoProducts.Models;
using MediatR;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool Availability { get; set; }
    public int CreatorUserID { get; set; }
}