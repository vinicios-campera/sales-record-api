using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class CommonPaginatedRequest
    {
        [DefaultValue(1)]
        [FromQuery(Name = "_page")]
        public int Page { get; set; }

        [DefaultValue(10)]
        [FromQuery(Name = "_size")]
        public int Size { get; set; } 

        [FromQuery(Name = "_order")]
        public string? Order { get; set; }
    }
}