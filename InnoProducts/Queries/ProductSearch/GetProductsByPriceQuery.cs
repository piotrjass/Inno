using InnoProducts.Models;
using MediatR;

public class GetProductsByPriceQuery : IRequest<List<Product>>
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public GetProductsByPriceQuery(decimal? minPrice, decimal? maxPrice)
    {
        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }
}