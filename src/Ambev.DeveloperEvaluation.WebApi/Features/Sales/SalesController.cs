using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    //Neste controller, deixei as respostas com ApiResponseWithData (forma que ja estava no Template),
    //porem acredito que esteja em desacordo com o passado README.md

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SalesController(IMediator mediator, IMapper mapper) : BaseController
    {
        /// <summary>
        /// Adicionar uma venda
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            //TODO::Acho desnecessário, pois o mesmo validador já existe no handler
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

        /// <summary>
        /// Obter uma venda por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetSaleCommand { Id = id };
            var result = await mediator.Send(command, cancellationToken);
            var response = mapper.Map<GetSaleResponse>(result);
            return Okay(response);
        }

        /// <summary>
        /// Obter vendas paginado e ordenado
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSales(CommonPaginatedRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<GetSalesCommand>(request);
            var result = await mediator.Send(command, cancellationToken);
            var items = mapper.Map<List<GetSaleResponse>>(result.Items);
            var response = new PaginatedList<GetSaleResponse>(items, result.TotalItems, request.Page, request.Size);
            return OkayPaginated(response);
        }

        /// <summary>
        /// Editar uma venda
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSale([FromRoute][Required] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            //TODO::Acho desnecessário, pois o mesmo validador já existe no handler
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

        /// <summary>
        /// Cancelar uma venda
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Cancelar um produto de uma venda
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("cancel{id}/item/{productId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ApiResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelItemSale([FromRoute][Required] Guid id, [FromRoute][Required] Guid productId, CancellationToken cancellationToken)
        {
            var command = new CancelSaleItemCommand { SaleId = id, ProductId = productId };
            var response = await mediator.Send(command, cancellationToken);
            return Okay(new ApiResponse
            {
                Detail = $"Sale {id} with item {productId} changed"
            });
        }
    }
}