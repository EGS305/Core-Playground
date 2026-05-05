using Blazor.Data;
using Microsoft.AspNetCore.Mvc;
using CarType = Blazor.Models.Type;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet("{brandId}")]
        public IEnumerable<CarType> Get(int brandId) => [.. context.Types.Where(type => type.Generation.Brand.Id == brandId)];
    }
}