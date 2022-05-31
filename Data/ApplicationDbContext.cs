using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Models;

namespace VolgaIT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Application>? Apps { get; set; }

        public DbSet<VolgaIT.Models.AppEvent>? AppEvent { get; set; }
    }
}