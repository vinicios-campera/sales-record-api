using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Unit.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application
{
    public class SaleEventHandlerTests
    {
        private readonly CreateSaleEventHandler _createSaleEventHandler;
        private readonly UpdateSaleEventHandler _updateSaleEventHandler;
        private readonly CancelSaleEventHandler _cancelSaleEventHandler;
        private readonly CancelSaleItemEventHandler _cancelSaleItemEventHandler;

        public SaleEventHandlerTests()
        {
            _createSaleEventHandler = new CreateSaleEventHandler(Substitute.For<ILogger<CreateSaleEventHandler>>());
            _updateSaleEventHandler = new UpdateSaleEventHandler(Substitute.For<ILogger<UpdateSaleEventHandler>>());
            _cancelSaleEventHandler = new CancelSaleEventHandler(Substitute.For<ILogger<CancelSaleEventHandler>>());
            _cancelSaleItemEventHandler = new CancelSaleItemEventHandler(Substitute.For<ILogger<CancelSaleItemEventHandler>>());
        }

        [Fact]
        public async Task Handle_Should_Process_SaleCreated_Notification()
        {
            // Arrange
            var notification = SaleEventHandlerTestsData.GenerateValidSaleCreatedEvent();

            // Act
            var act = () => _createSaleEventHandler.Handle(notification, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Handle_Should_Process_SaleUpdate_Notification()
        {
            // Arrange
            var notification = SaleEventHandlerTestsData.GenerateValidSaleUpdateEvent();

            // Act
            var act = () => _updateSaleEventHandler.Handle(notification, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Handle_Should_Process_SaleCanceled_Notification()
        {
            // Arrange
            var notification = SaleEventHandlerTestsData.GenerateValidSaleCanceledEvent();

            // Act
            var act = () => _cancelSaleEventHandler.Handle(notification, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Handle_Should_Process_SaleItemCanceled_Notification()
        {
            // Arrange
            var notification = SaleEventHandlerTestsData.GenerateValidSaleItemCanceledEvent();

            // Act
            var act = () => _cancelSaleItemEventHandler.Handle(notification, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}