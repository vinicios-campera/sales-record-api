using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CreateSaleHandler _handler;
    private readonly ITestOutputHelper _output;

    public CreateSaleHandlerTests(ITestOutputHelper output)
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _mediator);
        _output = output;
    }

    [Fact]
    public async Task Handle_Valid_Request()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Branch = command.Branch,
            Customer = command.Customer,
            Products = command.Products.ToList()
                .ConvertAll(x => new SaleProduct { ProductId = x.ProductId, Quantity = x.Quantity, Price = x.Price })
        };

        //Callers
        var result = new CreateSaleResult { Id = sale.Id, };
        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        sale.Products.ToList().ForEach(x =>
        {
            var commandGetProduct = new GetProductCommand { Id = x.ProductId };
            var getProductResult = new GetProductResult { Description = "Product_Description" };
            _mediator.Send(commandGetProduct).Returns(getProductResult);
        });

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(sale, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_InvalidRequest_Products_Empty_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateInvalidCommand_Products_Empty();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        _output.WriteLine(exception.Which.Message);
    }

    [Fact]
    public async Task Handle_InvalidRequest_MaxSize_Branch_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateInvalidCommand_MaxSize_Branch();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        _output.WriteLine(exception.Which.Message);
    }

    [Fact]
    public async Task Handle_InvalidRequest_MaxSize_Customer_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateInvalidCommand_MaxSize_Customer();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        _output.WriteLine(exception.Which.Message);
    }

    [Fact]
    public async Task Handle_InvalidRequest_MaxSize_Products_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateInvalidCommand_MaxSize_Products();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        _output.WriteLine(exception.Which.Message);
    }
}