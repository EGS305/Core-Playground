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

        [HttpGet("{groupName}")]
        public IEnumerable<Brand> Get(string groupName) => [.. context.Brands
            .Where(brand => brand.Group.Name == groupName)
            .Include(brand => brand.Country)
            .Include(brand => brand.Group)
            .Select(brand => CreateBrand(brand))];

        [HttpGet("{id:int}")]
        public Brand Get(int id) => CreateBrand(context.Brands
            .Include(brand => brand.Country)
            .Include(brand => brand.Group)
            .Single(brand => brand.Id == id));

        /// <summary>
        /// Prevent circular references on models and group.
        /// </summary>
        private static Brand CreateBrand(Brand brand) => new()
        {
            Id = brand.Id,
            Name = brand.Name,
            Country = new Country
            {
                Code = brand.Country.Code,
                Name = brand.Country.Name
            },
            LogoUrl = brand.LogoUrl,
            Group = new Group
            {
                Id = brand.Group.Id,
                Name = brand.Group.Name,
                Country = new Country
                {
                    Code = brand.Group.Country.Code,
                    Name = brand.Group.Country.Name
                }
            }
        };
    }
}