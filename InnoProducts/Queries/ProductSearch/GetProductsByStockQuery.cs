using InnoProducts.Models;
using MediatR;

public class GetProductsByStockQuery : IRequest<List<Product>>
{
    public bool InStock { get; set; }

    public GetProductsByStockQuery(bool inStock)
    {
        InStock = inStock;
    }
}