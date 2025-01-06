using InnoProducts.Models;
using MediatR;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}