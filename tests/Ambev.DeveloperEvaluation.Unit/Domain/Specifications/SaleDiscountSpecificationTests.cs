using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class SaleDiscountSpecificationTests
    {
        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(2, 1, 0)]
        [InlineData(3, 1, 0)]
        public void Check_Discount_Is_Zero(decimal quantity, decimal price, decimal expectedResult)
        {
            //Given
            var sale = SaleDiscountSpecificationTestsData.GenerateSaleWIthOneProduct(quantity, price);

            //Then
            sale.TotalDiscount.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(4, 1, 0.4)]
        [InlineData(5, 1, 0.5)]
        [InlineData(6, 1, 0.6)]
        [InlineData(7, 1, 0.7)]
        [InlineData(8, 1, 0.8)]
        [InlineData(9, 1, 0.9)]
        [InlineData(10, 1, 2)]
        [InlineData(11, 1, 2.2)]
        [InlineData(12, 1, 2.4)]
        [InlineData(13, 1, 2.6)]
        [InlineData(14, 1, 2.8)]
        [InlineData(15, 1, 3.0)]
        [InlineData(16, 1, 3.2)]
        [InlineData(17, 1, 3.4)]
        [InlineData(18, 1, 3.6)]
        [InlineData(19, 1, 3.8)]
        [InlineData(20, 1, 4.0)]
        public void Check_Discount_Is_Expected(decimal quantity, decimal price, decimal expectedResult)
        {
            //Given
            var sale = SaleDiscountSpecificationTestsData.GenerateSaleWIthOneProduct(quantity, price);

            //Then
            sale.TotalDiscount.Should().Be(expectedResult);
        }
    }
}