using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DecrementStock;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.IncrementStock;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Dtos;
using Application.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto?>> GetById([FromRoute] int id)
        {
            var product = await mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest("ID Mismatch");
            await mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IncrementProductStock([FromRoute] int id, [FromRoute] int quantity)
        {
            await mediator.Send(new IncrementStockCommand(id, quantity));

            return Ok();
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DecrementProductStock([FromRoute] int id, [FromRoute] int quantity)
        {
            await mediator.Send(new DecrementStockCommand(id, quantity));

            return Ok();
        }

    }
}
