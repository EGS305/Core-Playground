using System.ComponentModel;

namespace MVC.Models
{
    public class Car
    {
        public int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public List<Generation> Generations { get; set; } = [];
    }

    public class Generation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
        [DisplayName("Launch year")]
        public int LaunchYear { get; set; }
    }
}