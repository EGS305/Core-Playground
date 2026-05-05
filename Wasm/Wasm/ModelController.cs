using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarType = Blazor.Models.Type;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet("{brandId:int}")]
        public IEnumerable<Model> Get(int brandId) => [.. context.Models
            .Where(model => model.Generations.Any(gen => gen.Brand.Id == brandId))
            .Include(model => model.Generations)
            .ThenInclude(gen => gen.Types)
            .ThenInclude(type => type.Style)
            .Select(model => new Model
            {
                Id = model.Id,
                Generations = model.Generations.Where(gen => gen.Brand.Id == brandId).Select(gen => new Generation
                {
                    Id = gen.Id,
                    SequenceNumber = gen.SequenceNumber,
                    Brand = null!,
                    Types = gen.Types.Select(type => new CarType
                    {
                        Id = type.Id,
                        Name = type.Name,
                        Style = type.Style,
                        Generation = null!,
                        ProductionStart = type.ProductionStart,
                        ProductionEnd = type.ProductionEnd,
                        Quantity = type.Quantity,
                        ImageUrl = type.ImageUrl
                    }).ToList()
                }).ToList()
            })];
    }
}