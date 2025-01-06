using InnoProducts.Models;
using MediatR;

public class GetAllProductsQuery : IRequest<List<Product>>
{
}