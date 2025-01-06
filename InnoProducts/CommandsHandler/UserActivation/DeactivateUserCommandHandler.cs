using MediatR;
using System.Linq;
public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        var userProducts = products.Where(p => p.CreatorUserID == request.UserId).ToList();
        foreach (var product in userProducts)
        {
            product.Availability = false;  
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);  
    }
}