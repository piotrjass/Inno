using InnoProducts.Commands.UserActivation;
using MediatR;
using System.Linq;

public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ActivateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        
        var userProducts = products.Where(p => p.CreatorUserID == request.UserId).ToList();
        
        foreach (var product in userProducts)
        {
            product.Availability = true;  
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}