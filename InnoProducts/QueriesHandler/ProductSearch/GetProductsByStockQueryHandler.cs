using InnoProducts.Models;
using MediatR;

public class GetProductsByStockQueryHandler : IRequestHandler<GetProductsByStockQuery, List<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByStockQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Handle(GetProductsByStockQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        return products.Where(p => p.Availability == request.InStock).ToList();
    }
}