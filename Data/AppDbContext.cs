using Microsoft.EntityFrameworkCore;
using Teguk_API.Models;

namespace Teguk_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<UserProfile> UserProfiles
        { get; set; }

        public DbSet<HealthExpert> HealthExperts
        { get; set; }

        public DbSet<WaterIntake> WaterIntakes { get; set; }

        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<ActivityTracking> ActivityTrackings { get; set; }

        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<ConsultationMessage> ConsultationMessages { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<Consultation>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(x => x.Expert)
                .WithMany()
                .HasForeignKey(x => x.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsultationMessage>()
                .HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        
    }
}