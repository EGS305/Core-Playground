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
        public IEnumerable<Brand> Get() => [.. context.Brands
            .Include(brand => brand.Country)
            .Include(brand => brand.Group)
            .Select(brand => CreateBrand(brand))];

        [HttpGet("{id}")]
        public Brand Get(int id) => CreateBrand(context.Brands
            .Include(brand => brand.Country)
            .Include(brand => brand.Group)
            .Single(brand => brand.Id == id));

        /// <summary>
        /// Prevent circular references on models and group.
        /// </summary>
        private static Brand CreateBrand(Brand brand) => new Brand
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