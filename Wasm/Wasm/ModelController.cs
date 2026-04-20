using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet]
        public IEnumerable<Model> Get() => [.. context.Models];
    }
}