using Blazor.Data;
using Blazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Services
{
    public class DataService(IDbContextFactory<BlazorContext> dbFactory)
    {
        private BlazorContext Db => dbFactory.CreateDbContext();

        public IEnumerable<Group> GetGroups() => [.. Db.Groups];

        public IEnumerable<Brand> GetBrands() => [.. Db.Brands];
    }
}