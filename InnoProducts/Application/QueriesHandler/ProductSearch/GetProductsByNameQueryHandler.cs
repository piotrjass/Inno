using InnoProducts.Models;
using MediatR;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        return products.Where(p => p.Name.StartsWith(request.NameStartsWith, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}