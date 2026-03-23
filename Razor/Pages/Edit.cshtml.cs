using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages
{
    public class EditModel(Database database) : PageModel
    {
        private readonly Database _database = database;

        [BindProperty]
        public new User? User { get; set; }

        public IActionResult OnGet(int id)
        {
            var user = _database.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }

            User = user;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _database.UpdateUser(User);
            return RedirectToPage("Index");
        }
    }
}