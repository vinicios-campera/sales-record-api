namespace Ambev.DeveloperEvaluation.Domain.Extensions
{
    public static class NumberExtensions
    {
        public static decimal Round(this decimal value, int decimalPlaces = 2)
            => Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
    }
}