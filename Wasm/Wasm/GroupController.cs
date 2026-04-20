using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet]
        public IEnumerable<Group> Get() => [.. context.Groups];
    }
}