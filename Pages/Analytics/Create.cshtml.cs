using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Pages.Analytics
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppEvent AppEvent { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string? AppId, string? Event, string? Info)
        {
            if (_context.AppEvent == null || !((ModelState.IsValid && AppEvent != null) ||
                (AppId != null && Event != null)))
            {
                return Page();
            }

            try
            {
                if (AppId != null && Event != null)
                {
                    AppEvent = new AppEvent() { AppId = AppId, Event = Event, Info = Info };
                }
                AppEvent.CreatedDate = DateTime.UtcNow;
                _context.AppEvent.Add(AppEvent);
                var application = await _context.Apps.FirstOrDefaultAsync(m => m.AppId == AppEvent.AppId);
                if (application.AppEvents == null) application.AppEvents = new List<AppEvent>();
                application.AppEvents.Add(AppEvent);
                _context.Attach(application).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch
            {
                return NotFound();
            }

            return RedirectToPage("./Index", new { AppId = AppEvent.AppId });
        }
        private bool ApplicationExists(int id)
        {
            return (_context.Apps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
