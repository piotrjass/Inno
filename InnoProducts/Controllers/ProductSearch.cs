using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Endpoint do wyszukiwania po nazwie (pierwsze 3 litery)
        [HttpGet("search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string nameStartsWith)
        {
            var query = new GetProductsByNameQuery(nameStartsWith);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Endpoint do wyszukiwania po cenie
        [HttpGet("search-by-price")]
        public async Task<IActionResult> SearchByPrice([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var query = new GetProductsByPriceQuery(minPrice, maxPrice);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Endpoint do wyszukiwania po dostępności
        [HttpGet("search-by-stock")]
        public async Task<IActionResult> SearchByStock([FromQuery] bool inStock)
        {
            var query = new GetProductsByStockQuery(inStock);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Endpoint do wyszukiwania po dacie stworzenia
        [HttpGet("search-by-creation-date")]
        public async Task<IActionResult> SearchByCreationDate([FromQuery] DateTime? minDate, [FromQuery] DateTime? maxDate)
        {
            var query = new GetProductsByCreationDateQuery(minDate, maxDate);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}