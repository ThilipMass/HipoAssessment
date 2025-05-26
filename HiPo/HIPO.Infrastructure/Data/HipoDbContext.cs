using Microsoft.EntityFrameworkCore;

namespace HIPO.Infrastructure;

public class HipoDbContext : DbContext
{
    public HipoDbContext(DbContextOptions<HipoDbContext> options) : base(options)
    {
    }

    public DbSet<HipoUsers> HipoUser { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<UserProfiles> UserProfiles { get; set; }
    public DbSet<AssessmentResponses> AssessmentResponses { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Options> Options { get; set; }
    public DbSet<HipoAssessmentQuestions> HipoAssessmentQuestions { get; set; }
    public DbSet<HipoAssessmentOptions> HipoAssessmentOptions { get; set; }
    public DbSet<Cfg_AssessmentStatus> Cfg_AssessmentStatuses { get; set; }
    public DbSet<Cfg_MBTIFactors> Cfg_MBTIFactors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Users ↔ UserProfiles (One-to-Many)
    modelBuilder.Entity<UserProfiles>()
        .HasOne(up => up.User)
        .WithMany()
        .HasForeignKey(up => up.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    // UserProfiles ↔ AssessmentResponses (One-to-Many)
    modelBuilder.Entity<AssessmentResponses>()
        .HasOne(ar => ar.UserProfile)
        .WithMany()
        .HasForeignKey(ar => ar.UserProfileId)
        .OnDelete(DeleteBehavior.Cascade);

    // UserProfiles ↔ Cfg_AssessmentStatus (One-to-Many)
    modelBuilder.Entity<Cfg_AssessmentStatus>()
        .HasOne(cs => cs.UserProfile)
        .WithMany()
        .HasForeignKey(cs => cs.Id)
        .OnDelete(DeleteBehavior.Cascade);

    // Questions ↔ Options (One-to-Many)
    modelBuilder.Entity<Options>()
        .HasOne(opt => opt.Questions)
        .WithMany()
        .HasForeignKey(opt => opt.QuestionId)
        .OnDelete(DeleteBehavior.Cascade);

    // // Configure ResponseJSON to use string (if using string storage)
    // modelBuilder.Entity<AssessmentResponses>()
    //     .Property(ar => ar.ResponseJSON.ToString())
    //     .IsRequired();

    // // Optional: You can set CreatedAt default values globally (optional)
    // modelBuilder.Entity<Users>()
    //     .Property(u => u.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<UserProfiles>()
    //     .Property(u => u.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<AssessmentResponses>()
    //     .Property(a => a.SubmittedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<Cfg_MBTIFactors>()
    //     .Property(m => m.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<Questions>()
    //     .Property(q => q.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<Options>()
    //     .Property(o => o.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");

    // modelBuilder.Entity<Cfg_AssessmentStatus>()
    //     .Property(s => s.CreatedAt)
    //     .HasDefaultValueSql("GETUTCDATE()");
}

}