namespace Razor
{
    public class User
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public string? About { get; set; }

        public override string ToString() => Username;
    }
}