using System;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Bogus;
using static Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleCommand;

namespace Ambev.DeveloperEvaluation.Functional.Drivers.TestData
{
    public static class SaleControllerTestData
    {
        public static CreateSaleCommand GenerateCreateValidCommand()
        {
            var products = new Faker<CreateSaleCommandProducts>()
                .RuleFor(p => p.ProductId, f => Guid.NewGuid())
                .RuleFor(p => p.Quantity, f => f.Random.Decimal(1, 100))
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));

            return new Faker<CreateSaleCommand>()
                .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
                .RuleFor(u => u.Customer, f => f.Person.FullName)
                .RuleFor(s => s.Products, f => products.Generate(f.Random.Int(1, 5)))
            .Generate();
        }

        public static CreateSaleRequest GenerateCreateValidRequest()
        {
            var products = new Faker<CreateSaleProductRequest>()
                .RuleFor(p => p.ProductId, f => Guid.NewGuid())
                .RuleFor(p => p.Quantity, f => f.Random.Decimal(1, 100))
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));

            return new Faker<CreateSaleRequest>()
                .RuleFor(u => u.Branch, f => f.Company.Random.AlphaNumeric(20))
                .RuleFor(u => u.Customer, f => f.Person.FullName)
                .RuleFor(s => s.Products, f => products.Generate(f.Random.Int(1, 5)))
            .Generate();
        }
    }
}