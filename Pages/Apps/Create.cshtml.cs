using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VolgaIT.Data;
using VolgaIT.Models;
using Microsoft.AspNetCore.Identity;

namespace VolgaIT.Pages.Apps
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private string UserId;

        public CreateModel(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            UserId = _userManager.GetUserId(_signInManager.Context.User);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Application Application { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            UserId = _userManager.GetUserId(_signInManager.Context.User);
            if (_context.Apps == null || Application == null || UserId == null)
            {
                return Page();
            }

            Application.UserId = UserId;
            Application.CreatedDate = DateTime.UtcNow;
            Application.AppEvents = new List<AppEvent>();

            if (Application.AppId == "" || Application.Name == "" || Application.UserId == "")
            {
                return Page();
            }
            _context.Apps.Add(Application);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
