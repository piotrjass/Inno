using MediatR;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            return false;

        if (product.CreatorUserID != request.UserId)
            throw new UnauthorizedAccessException("You are not authorized to update this product.");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Availability = request.Availability;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
    
}