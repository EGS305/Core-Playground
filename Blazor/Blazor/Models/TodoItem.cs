namespace Blazor.Models
{
    public class TodoItem(string? title)
    {
        public string? Title { get; set; } = title;
        public bool IsDone { get; set; }

        public override string ToString() => Title ?? "(untitled)";
    }
}