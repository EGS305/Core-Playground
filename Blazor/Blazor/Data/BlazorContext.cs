using Blazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data
{
    public class BlazorContext(DbContextOptions<BlazorContext> options) : DbContext(options)
    {
        public DbSet<Object3d> Objects { get; set; } = default!;
    }
}