using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartsController(IMediator mediator, IMapper mapper) : BaseController
    {
        /// <summary>
        /// Adicionar um carrinho
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCartResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateCartCommand>(request);
            command.UserId = Guid.Parse(GetCurrentUserId());
            var response = mapper.Map<CreateCartResponse>(request);
            response.Id = await mediator.Send(command, cancellationToken);
            response.UserId = Guid.Parse(GetCurrentUserId());
            return Created(GetUrl($"/api/carts/{response.Id}"), response);
        }

        /// <summary>
        /// Obter um carrinho pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCart([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetCartCommand { Id = id, UserId = Guid.Parse(GetCurrentUserId()) };
            var result = await mediator.Send(command, cancellationToken);
            var response = mapper.Map<GetCartResponse>(result);
            response.UserId = Guid.Parse(GetCurrentUserId());
            return Ok(response);
        }

        /// <summary>
        /// Ovter carrinhos paginado e ordenado
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResponse<GetCartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCarts(CommonPaginatedRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<GetCartsCommand>(request);
            command.UserId = Guid.Parse(GetCurrentUserId());
            var result = await mediator.Send(command, cancellationToken);
            var items = mapper.Map<List<GetCartResponse>>(result.Items);
            items.ForEach(x => x.UserId = Guid.Parse(GetCurrentUserId()));
            var response = new PageResponse<GetCartResponse>(items, result.TotalItems, request.Page, request.Size);
            return Ok(response);
        }

        /// <summary>
        /// Editar um carrinho
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateCartResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCart([FromRoute][Required] Guid id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateCartCommand>(request);
            command.Id = id;
            var response = mapper.Map<UpdateCartResponse>(request);
            response.Id = await mediator.Send(command, cancellationToken);
            response.UserId = Guid.Parse(GetCurrentUserId());
            return Accepted(GetUrl($"/api/carts/{response.Id}"), response);
        }

        /// <summary>
        /// Deletar um carrinho
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCartCommand { Id = id };
            await mediator.Send(command, cancellationToken);
            return Ok(new MessageResponse { Message = "Deleted successfully" });
        }
    }
}