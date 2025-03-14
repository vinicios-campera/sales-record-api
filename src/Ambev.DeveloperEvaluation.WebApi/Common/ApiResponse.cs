using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponse
{
    public string Type { get; set; } = string.Empty;
    public string? Detail { get; set; } = string.Empty;
    public string? Error { get; set; } = string.Empty;
}
