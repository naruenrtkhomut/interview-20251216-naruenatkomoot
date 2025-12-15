using Interview_Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Interview_Test.Infrastructure;

public class InterviewTestDbContext : DbContext
{
    public InterviewTestDbContext(DbContextOptions<InterviewTestDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> UserTb { get; set; }
    public DbSet<UserProfileModel> UserProfileTb { get; set; }
    public DbSet<RoleModel> RoleTb { get; set; }
    public DbSet<UserRoleMappingModel> UserRoleMappingTb { get; set; }
    public DbSet<PermissionModel> PermissionTb { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasOne(urm => urm.UserProfile)
              .WithOne(u => u.User)
              .HasForeignKey<UserProfileModel>(p => p.ProfileId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserProfileModel>(entity =>
        {
            entity.HasOne(urm => urm.User)
              .WithOne(u => u.UserProfile)
              .HasPrincipalKey<UserModel>(u => u.Id)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserRoleMappingModel>(entity =>
        {
            entity.HasOne(urm => urm.User)
              .WithMany(u => u.UserRoleMappings)
              .HasForeignKey("UserId")
              .HasPrincipalKey(u => u.Id)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserRoleMappingModel>(entity =>
        {
            entity.HasOne(urm => urm.Role)
              .WithMany(u => u.UserRoleMappings)
              .HasForeignKey("RoleId")
              .HasPrincipalKey(u => u.RoleId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PermissionModel>(entity =>
        {
            entity.HasOne(urm => urm.Role)
              .WithMany(u => u.Permissions)
              .HasForeignKey("RoleId")
              .HasPrincipalKey(u => u.RoleId)
              .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

public class InterviewTestDbContextDesignFactory : IDesignTimeDbContextFactory<InterviewTestDbContext>
{
    public InterviewTestDbContext CreateDbContext(string[] args)
    {
        string connectionString = "<your database connection string>";
        var optionsBuilder = new DbContextOptionsBuilder<InterviewTestDbContext>()
            .UseSqlServer(connectionString, opts => opts.CommandTimeout(600));

        return new InterviewTestDbContext(optionsBuilder.Options);
    }
}