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

namespace VolgaIT.Pages.Analytics
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private string UserId;
        private string AppId;

        public IndexModel(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            UserId = _userManager.GetUserId(_signInManager.Context.User);
        }

        public IList<AppEvent> AppEvent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? AppId)
        {
            UserId = _userManager.GetUserId(_signInManager.Context.User);
            if (AppId != null)
            {
                var application = await _context.Apps.FirstOrDefaultAsync(m => m.AppId == AppId);
                if (application.UserId != UserId) return NotFound();
            }
            if (AppId == null || _context.AppEvent == null || UserId == null)
            {
                return NotFound();
            }
            this.AppId = AppId;
            AppEvent = await _context.AppEvent.Where(a => a.AppId == AppId).ToListAsync();

            //var app = await _context.Apps.Where(a => a.AppId == AppId).FirstAsync();
            //AppEvent = app.AppEvents;

            return Page();
        }
    }
}
