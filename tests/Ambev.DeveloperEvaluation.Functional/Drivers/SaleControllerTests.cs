using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Functional.Drivers.TestData;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Drivers
{
    public class SaleControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SaleControllerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
        }

        [Fact]
        public async Task CreateSale_Valid_Response_Type()
        {
            // Arrange
            var controller = new SalesController(_mediator, _mapper);
            var command = SaleControllerTestData.GenerateCreateValidCommand();
            var request = SaleControllerTestData.GenerateCreateValidRequest();

            // Callers
            _mediator.Send(command, CancellationToken.None).Returns(new CreateSaleResult { Id = Guid.NewGuid() });
            _mapper.Map<CreateSaleCommand>(request).Returns(command);

            // Act
            var result = await controller.CreateSale(request, CancellationToken.None);
            var method = typeof(SalesController).GetMethod(nameof(controller.CreateSale));
            var attributes = method?.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), false)
                .Cast<ProducesResponseTypeAttribute>()
                .ToList();

            // Assert
            var okResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(okResult.StatusCode, 201);
            Assert.Contains(attributes, x => x.Type == okResult?.Value?.GetType());
        }

        [Fact]
        public async Task GetSale_Valid_Response_Type()
        {
            var guid = Guid.NewGuid();

            // Arrange
            var controller = new SalesController(_mediator, _mapper);

            // Callers
            var command = new GetSaleCommand { Id = guid };
            await _mediator.Send(command, CancellationToken.None);
            _mapper.Map<GetSaleResponse>(null).Returns(new GetSaleResponse());

            // Act
            var result = await controller.GetSale(guid, CancellationToken.None);
            var method = typeof(SalesController).GetMethod(nameof(controller.GetSale));
            var attributes = method?.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), false)
                .Cast<ProducesResponseTypeAttribute>()
                .ToList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okResult.StatusCode, 200);
            Assert.Contains(attributes, x => x.Type == okResult?.Value?.GetType());
        }
    }
}