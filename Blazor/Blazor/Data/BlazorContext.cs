using Blazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data
{
    public class BlazorContext(DbContextOptions<BlazorContext> options) : DbContext(options)
    {
        public DbSet<Object3d> Objects { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<Group> Groups { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default!;
        public DbSet<Model> Models { get; set; } = default!;
        public DbSet<Generation> Generations { get; set; } = default!;
        public DbSet<Models.Type> Types { get; set; } = default!;
        public DbSet<Style> Styles { get; set; } = default!;
    }
}