using MediatR;

namespace InnoProducts.Commands.UserActivation;

public class ActivateUserCommand : IRequest
{
    public string UserId { get; set; }  // UserId to identyfikator użytkownika, którego produkty chcemy zaktualizować
}