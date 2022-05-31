using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Pages.Apps
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private string UserId;

        public IndexModel(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            UserId = _userManager.GetUserId(_signInManager.Context.User);
        }

        public IList<Application> Application { get;set; } = default!;


        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(_signInManager.Context.User);
            if (_context.Apps != null && UserId != null)
            {
                Application = await _context.Apps.Where(a => a.UserId == UserId).ToListAsync();
            }
        }
    }
}
