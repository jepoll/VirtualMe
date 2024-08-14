using Core.Domain.AddressTables;
using Core.Domain.Entities;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Logs> Logis { get; set; } = default!;
    public DbSet<Activity> Activity { get; set; } = default!;
    public DbSet<ActivityType> ActivityType { get; set; } = default!;
    public DbSet<Avatar> Avatar { get; set; } = default!;
    public DbSet<Chat> Chat { get; set; } = default!;
    public DbSet<Interior> Interior { get; set; } = default!;
    public DbSet<AvatarOwnsInterior> AvatarOwnsInterior { get; set; } = default!;
    public DbSet<Item> Item { get; set; } = default!;
    public DbSet<Message> Message { get; set; } = default!;
    public DbSet<TaskQuest> TaskQuest { get; set; } = default!;
    public DbSet<TaskType> TaskType { get; set; } = default!;
    public DbSet<AvatarsActivity> AvatarsActivity { get; set; } = default!;
    public DbSet<AvatarsTasks> AvatarsTasks { get; set; } = default!;
    public DbSet<Owns> Owns { get; set; } = default!;
    public DbSet<Reward> Reward { get; set; } = default!;
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
      //  AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
      //  AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.LogTo(Console.WriteLine);
    //     base.OnConfiguring(optionsBuilder);
    // }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // disable cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        if (this.Database.ProviderName!.Contains("InMemory"))
        {
            builder.Entity<AppUser>()
                .HasMany(e => e.Avatars)
                .WithOne(e => e.AppUser);
            
            builder.Entity<Avatar>()
                .HasOne(e => e.AppUser)
                .WithMany(e => e.Avatars)
                .HasForeignKey(e => e.AppUserId);

            builder.Entity<Chat>()
                .HasOne(e => e.Avatar1)
                .WithMany()
                .HasForeignKey(e => e.Avatar1Id);
            builder.Entity<Chat>()
                .HasOne(e => e.Avatar2)
                .WithMany()
                .HasForeignKey(e => e.Avatar2Id);

            builder.Entity<Message>()
                .HasOne(e => e.Chat)
                .WithMany()
                .HasForeignKey(e => e.ChatId);
            
            builder.Entity<AvatarOwnsInterior>()
                .HasOne(e => e.Avatar)
                .WithMany()
                .HasForeignKey(e => e.AvatarId);
            builder.Entity<AvatarOwnsInterior>()
                .HasOne(e => e.Interior)
                .WithMany()
                .HasForeignKey(e => e.InteriorId);
            
            builder.Entity<Interior>();
            
            builder.Entity<AvatarsActivity>()
                .HasOne(e => e.Avatar)
                .WithMany()
                .HasForeignKey(e => e.AvatarId);
            builder.Entity<AvatarsActivity>()
                .HasOne(e => e.Activity)
                .WithMany()
                .HasForeignKey(e => e.ActivityId);
            
            builder.Entity<Activity>()
                .HasOne(e => e.ActivityType)
                .WithMany()
                .HasForeignKey(e => e.ActivityTypeId);
            
            builder.Entity<ActivityType>();
            
            builder.Entity<TaskQuest>()
                .HasOne(e => e.TaskType)
                .WithMany()
                .HasForeignKey(e => e.TaskTypeId);
            builder.Entity<TaskQuest>()
                .HasOne(e => e.Activity)
                .WithMany()
                .HasForeignKey(e => e.ActivityId);
            
            builder.Entity<AvatarsTasks>()
                .HasOne(e => e.Avatar)
                .WithMany()
                .HasForeignKey(e => e.AvatarId);
            builder.Entity<AvatarsTasks>()
                .HasOne(e => e.TaskQuest)
                .WithMany()
                .HasForeignKey(e => e.TaskQuestId);
            
            builder.Entity<Reward>()
                .HasOne(e => e.TaskQuest)
                .WithMany()
                .HasForeignKey(e => e.TaskQuestId);
            builder.Entity<Reward>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId);

            builder.Entity<Item>();
            
            builder.Entity<Owns>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId);
            builder.Entity<Owns>()
                .HasOne(e => e.Avatar)
                .WithMany()
                .HasForeignKey(e => e.AvatarId);
            
            builder.Entity<Logs>()
                .HasOne(e => e.Avatar)
                .WithMany()
                .HasForeignKey(e => e.AvatarId);
        }

    }
    
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) // Token Used to cancel operation (in case of error)
    // {
    //     foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted)) // All existing entities
    //     {
    //         foreach (var prop in entity
    //                      .Properties
    //                      .Where(x => x.Metadata.ClrType == typeof(DateTime))) // All DateTime foelds
    //         {
    //             Console.WriteLine(prop);
    //             prop.CurrentValue = ((DateTime) prop.CurrentValue!).ToUniversalTime(); // Override each users time to Universal time
    //             DateTime.SpecifyKind((DateTime) prop.CurrentValue, DateTimeKind.Utc);
    //         }
    //     }
    //     
    //     return base.SaveChangesAsync(cancellationToken);
    // }
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity.Properties.Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                if (prop.CurrentValue != null)
                {
                    DateTime dateTime = (DateTime)prop.CurrentValue;

                    if (dateTime.Kind == DateTimeKind.Local)
                    {
                        dateTime = dateTime.ToUniversalTime();
                    }
                    else if (dateTime.Kind == DateTimeKind.Unspecified)
                    {
                        dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }

                    prop.CurrentValue = dateTime;
                }
            }
        }

        foreach (var avatar in Avatar)
        {
            if (avatar.LastChanges.Kind == DateTimeKind.Local)
            {
                avatar.LastChanges = avatar.LastChanges.ToUniversalTime();
            } else if (avatar.LastChanges.Kind == DateTimeKind.Unspecified)
            {
                avatar.LastChanges = DateTime.SpecifyKind(avatar.LastChanges, DateTimeKind.Utc);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


}