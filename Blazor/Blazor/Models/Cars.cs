using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Country
    {
        [Key]
        public required string Code { get; set; }
        public required string Name { get; set; }
        public ICollection<Group> Groups { get; set; } = [];
        public ICollection<Brand> Brands { get; set; } = [];
    }

    public class Group
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Country Country { get; set; }
        public ICollection<Brand> Brands { get; set; } = [];
    }

    public class Brand
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Country Country { get; set; }
        public string? LogoUrl { get; set; }
        public ICollection<Model> Models { get; set; } = [];

        public override string ToString() => Name ?? "?";
    }

    public class Model
    {
        public required int Id { get; set; }
        public ICollection<Generation> Generations { get; set; } = [];
    }

    public class Generation
    {
        public required int Id { get; set; }
        public required int SequenceNumber { get; set; }
        public required Brand Brand { get; set; }
        public ICollection<Type> Types { get; set; } = [];
    }

    public class Type
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Style Style { get; set; }
        public required Generation Generation { get; set; }
        public required string ProductionStart { get; set; }
        public required string ProductionEnd { get; set; }
        public required int Quantity { get; set; }
        public required string ImageUrl { get; set; }
    }

    public class Style
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}