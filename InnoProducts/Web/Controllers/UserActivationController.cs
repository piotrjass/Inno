using InnoProducts.Commands.UserActivation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InnoProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserActivationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserActivationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Users products are NOT available.");
        }
        
        [HttpPost("activate")]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Users products are now available.");
        }
    }
}