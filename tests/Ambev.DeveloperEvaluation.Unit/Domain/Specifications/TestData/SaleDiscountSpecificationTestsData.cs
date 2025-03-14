using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

public static class SaleDiscountSpecificationTestsData
{
    public static Sale GenerateSaleWIthOneProduct(decimal quantity, decimal price)
    {
        var sale = new Sale { Products = [new SaleProduct { Quantity = quantity, Price = price }] };
        sale.CalculateDiscount();
        return sale;
    }
}