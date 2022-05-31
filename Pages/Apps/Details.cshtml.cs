using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Pages.Apps
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private string UserId;

        public DetailsModel(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            UserId = _userManager.GetUserId(_signInManager.Context.User);
        }

        public Application Application { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserId = _userManager.GetUserId(_signInManager.Context.User);
            if (id == null || _context.Apps == null || UserId == null)
            {
                return NotFound();
            }

            var application = await _context.Apps.FirstOrDefaultAsync(m => m.Id == id && m.UserId == UserId);
            if (application == null)
            {
                return NotFound();
            }
            else 
            {
                Application = application;
            }
            return Page();
        }
    }
}
