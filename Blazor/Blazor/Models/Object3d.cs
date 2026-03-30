namespace Blazor.Models
{
    public class Object3d
    {
        public int Id { get; set; }
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        [VectorSize]
        public Vector3 Size { get; set; } = new Vector3(1, 1, 1);
        public Vector3 Color { get; set; } = new Vector3(1, 0, 0);

        public override string ToString() => $@"<span class=""badge bg-primary"">{Position}</span>&nbsp;<span class=""badge bg-info"">{Size}</span>&nbsp;<span class=""badge bg-secondary"">{Color}</span>";
    }
}