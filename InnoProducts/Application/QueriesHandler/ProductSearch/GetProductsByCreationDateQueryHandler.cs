using InnoProducts.Models;
using MediatR;

public class GetProductsByCreationDateQueryHandler : IRequestHandler<GetProductsByCreationDateQuery, List<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByCreationDateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Handle(GetProductsByCreationDateQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);

        if (request.MinDate.HasValue)
            products = products.Where(p => p.CreationDate >= request.MinDate.Value).ToList();

        if (request.MaxDate.HasValue)
            products = products.Where(p => p.CreationDate <= request.MaxDate.Value).ToList();

        return products;
    }
}