using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet]
        public IEnumerable<Model> Get() => [.. context.Models
            .Include(model => model.Generations)
            .ThenInclude(gen => gen.Types)];
    }
}