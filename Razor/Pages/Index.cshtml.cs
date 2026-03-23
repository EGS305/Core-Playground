using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages
{
    public class IndexModel(Database database) : PageModel
    {
        private readonly Database _database = database;
        public IEnumerable<User>? Users { get; set; }

        public void OnGet() => Users = _database.GetUsers();
    }
}