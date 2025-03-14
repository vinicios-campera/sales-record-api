using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator, IMapper mapper) : BaseController
    {
        /// <summary>
        /// Adicionar um produto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateProductCommand>(request);
            var response = mapper.Map<CreateProductResponse>(request);
            response.Id = await mediator.Send(command, cancellationToken);
            return Created(GetUrl($"/api/products/{response.Id}"), response);
        }

        /// <summary>
        /// Obter um produto pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetProductCommand { Id = id };
            var result = await mediator.Send(command, cancellationToken);
            var response = mapper.Map<GetProductResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Obter produtos paginado, ordenado e filtrado
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<GetProductsCommand>(request);
            var result = await mediator.Send(command, cancellationToken);
            var items = mapper.Map<List<GetProductResponse>>(result.Items);
            var response = new PageResponse<GetProductResponse>(items, result.TotalItems, request.Page, request.Size);
            return Ok(response);
        }

        /// <summary>
        /// Obter categorias de produtos cadastrados
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsCategories(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetProductsCategoriesCommand(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Obter produtos de uma categoria
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(PageResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsByCategory(CommonPaginatedRequest request, [FromRoute] ProductCategory category, CancellationToken cancellationToken)
        {
            var command = mapper.Map<GetProductsCommand>(request);
            command.Category = category;
            var result = await mediator.Send(command, cancellationToken);
            var items = mapper.Map<List<GetProductResponse>>(result.Items);
            var response = new PageResponse<GetProductResponse>(items, result.TotalItems, request.Page, request.Size);
            return Ok(response);
        }

        /// <summary>
        /// Editar um produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateProductResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute][Required] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var response = mapper.Map<UpdateProductResponse>(request);
            response.Id = await mediator.Send(command, cancellationToken);
            return Accepted(GetUrl($"/api/products/{response.Id}"), response);
        }

        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = id };
            await mediator.Send(command, cancellationToken);
            return Ok(new MessageResponse { Message = "Deleted successfully" });
        }
    }
}