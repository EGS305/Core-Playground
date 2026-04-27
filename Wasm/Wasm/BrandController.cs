using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController(BlazorContext context) : ControllerBase
    {
        private readonly BlazorContext context = context;

        [HttpGet]
        public IEnumerable<Brand> Get() => [.. context.Brands.Include(brand => brand.Country).Select(brand => new Brand
        {
            Id = brand.Id,
            Name = brand.Name,
            Country = brand.Country,
            LogoUrl = brand.LogoUrl,
            Group = brand.Group
        })]; //Had to prevent a circular reference.

        [HttpGet("{id}")]
        public Brand Get(int id) => context.Brands.Single(brand => brand.Id == id);
    }
}