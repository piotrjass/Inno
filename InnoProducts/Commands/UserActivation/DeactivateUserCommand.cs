using MediatR;

public class DeactivateUserCommand : IRequest
{
    public string UserId { get; set; }  // UserId to identyfikator użytkownika, którego produkty chcemy zaktualizować
}