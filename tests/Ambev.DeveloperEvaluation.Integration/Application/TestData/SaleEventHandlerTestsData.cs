using Ambev.DeveloperEvaluation.Domain.Events;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class SaleEventHandlerTestsData
{
    public static SaleCreatedEvent GenerateValidSaleCreatedEvent()
    {
        return new Faker<SaleCreatedEvent>()
            .CustomInstantiator(f => new SaleCreatedEvent(Guid.NewGuid(), f.Person.FullName, f.Random.Int(1, 5)))
        .Generate();
    }

    public static SaleUpdateEvent GenerateValidSaleUpdateEvent()
    {
        return new Faker<SaleUpdateEvent>()
            .CustomInstantiator(f => new SaleUpdateEvent(Guid.NewGuid(), f.Person.FullName, f.Random.Int(1, 5)))
        .Generate();
    }

    public static SaleCanceledEvent GenerateValidSaleCanceledEvent()
    {
        return new Faker<SaleCanceledEvent>()
            .CustomInstantiator(f => new SaleCanceledEvent(Guid.NewGuid()))
        .Generate();
    }

    public static SaleItemCanceledEvent GenerateValidSaleItemCanceledEvent()
    {
        return new Faker<SaleItemCanceledEvent>()
            .CustomInstantiator(f => new SaleItemCanceledEvent(Guid.NewGuid(), Guid.NewGuid()))
        .Generate();
    }
}