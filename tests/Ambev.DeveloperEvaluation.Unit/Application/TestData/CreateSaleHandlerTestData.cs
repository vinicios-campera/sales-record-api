using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;
using static Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleCommand;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class CreateSaleHandlerTestData
{
    private static Faker<CreateSaleCommandProducts> CreateValidSaleProduct(Guid? guid = null)
    {
        guid ??= Guid.NewGuid();

        return new Faker<CreateSaleCommandProducts>()
            .RuleFor(p => p.ProductId, f => guid)
            .RuleFor(p => p.Quantity, f => f.Random.Decimal(1, 100))
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));
    }

    public static CreateSaleCommand GenerateValidCommand()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
            .RuleFor(u => u.Customer, f => f.Person.FullName)
            .RuleFor(s => s.Products, f => CreateValidSaleProduct().Generate(f.Random.Int(1, 5)))
        .Generate();
    }

    public static CreateSaleCommand GenerateInvalidCommand_Products_Empty()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
            .RuleFor(u => u.Customer, f => f.Person.FullName)
            .RuleFor(s => s.Products, f => [])
        .Generate();
    }

    public static CreateSaleCommand GenerateInvalidCommand_MaxSize_Customer()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
            .RuleFor(u => u.Customer, f => f.Company.Random.AlphaNumeric(21))
            .RuleFor(s => s.Products, f => CreateValidSaleProduct().Generate(f.Random.Int(1, 5)))
        .Generate();
    }

    public static CreateSaleCommand GenerateInvalidCommand_MaxSize_Branch()
    {
        return new Faker<CreateSaleCommand>()
            .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(21))
            .RuleFor(u => u.Customer, f => f.Person.FullName)
            .RuleFor(s => s.Products, f => CreateValidSaleProduct().Generate(f.Random.Int(1, 5)))
        .Generate();
    }

    public static CreateSaleCommand GenerateInvalidCommand_MaxSize_Products()
    {
        var idProduct = Guid.NewGuid();
        return new Faker<CreateSaleCommand>()
            .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
            .RuleFor(u => u.Customer, f => f.Person.FullName)
            .RuleFor(s => s.Products, f => CreateValidSaleProduct(idProduct).Generate(21))
        .Generate();
    }
}