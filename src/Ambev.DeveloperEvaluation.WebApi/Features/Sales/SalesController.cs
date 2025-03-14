using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SalesController(IMediator mediator, IMapper mapper) : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            //TODO::Acho desnessesario
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = mapper.Map<CreateSaleCommand>(request);
            var response = await mediator.Send(command, cancellationToken);
            return Created(GetUrl($"/api/Sales/{response.Id}"), new ApiResponseWithData<CreateSaleResponse>
            {
                Detail = "Sale created successfully",
                Data = mapper.Map<CreateSaleResponse>(response)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSale([FromRoute][Required] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            //TODO::Acho desnessesario
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;
            var response = await mediator.Send(command, cancellationToken);
            return Accepted(GetUrl($"/api/Sales/{response.Id}"), new ApiResponseWithData<UpdateSaleResponse>
            {
                Detail = "Sale update successfully",
                Data = mapper.Map<UpdateSaleResponse>(response)
            });
        }

        [HttpPatch("cancel/{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ApiResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelSaleCommand { Id = id };
            var response = await mediator.Send(command, cancellationToken);

            return Okay(new ApiResponse
            {
                Detail = $"Sale {id} changed"
            });
        }

        [HttpPatch("cancel{id}/item/{item}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ApiResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelItemSale([FromRoute][Required] Guid id, [FromRoute] string item, CancellationToken cancellationToken)
        {
            var command = new CancelSaleItemCommand { SaleId = id, Product = item };
            var response = await mediator.Send(command, cancellationToken);

            return Okay(new ApiResponse
            {
                Detail = $"Sale {id} with item {item} changed"
            });
        }
    }
}