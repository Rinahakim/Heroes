using Heroes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Data
{
    public class HeroesContext: IdentityDbContext<AppUser>
    {
        public HeroesContext(DbContextOptions<HeroesContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HeroModel>()
                .HasOne(h => h.Trainer)
                .WithMany() 
                .HasForeignKey(h => h.TrainerId) 
                .OnDelete(DeleteBehavior.SetNull); 
        }

        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}
