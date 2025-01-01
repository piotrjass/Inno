using InnoProducts.Models;
using MediatR;

public class GetProductsByPriceQueryHandler : IRequestHandler<GetProductsByPriceQuery, List<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByPriceQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Handle(GetProductsByPriceQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);

        if (request.MinPrice.HasValue)
            products = products.Where(p => p.Price >= request.MinPrice.Value).ToList();

        if (request.MaxPrice.HasValue)
            products = products.Where(p => p.Price <= request.MaxPrice.Value).ToList();

        return products;
    }
}