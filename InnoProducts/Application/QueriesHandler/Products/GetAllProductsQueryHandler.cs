using InnoProducts.Models;
using MediatR;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Products.GetAllAsync(cancellationToken);
    }
}