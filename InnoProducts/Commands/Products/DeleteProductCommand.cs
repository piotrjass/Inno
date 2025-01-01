using MediatR;

public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; set; }
}