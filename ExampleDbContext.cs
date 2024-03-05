using EFCore.Examples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WrongTranslationsExample;


public class ExampleDbContext(DbContextOptions<ExampleDbContext> options) : DbContext(options)
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<WorkingScheme> WorkingScheme { get; set; }
    public virtual DbSet<WorkingDay> WorkingDays { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("timestamptz").HavePrecision(0)
            .HaveConversion<DateValueConverter>();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDbFunction(GetType().GetMethod(nameof(Split))!).HasName("string_split");
        modelBuilder.Entity<Result>().HasNoKey().ToTable(e => e.ExcludeFromMigrations());


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(d => d.Orders).WithOne(p => p.User)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);


            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<WorkingScheme>(e =>
        {
            e.HasIndex(p => p.UserId)
             .IsUnique();

            e.HasOne(p => p.User)
             .WithOne(s => s.WorkingScheme)
             .HasForeignKey<WorkingScheme>(w => w.UserId);
        });

        modelBuilder.Entity<WorkingDay>(e =>
        {
            e.HasOne(p => p.WorkingScheme)
                .WithMany(s => s.WorkingDays)
                .HasForeignKey(p => p.WorkingSchemeId);

            e.HasIndex(p => new { p.WorkingSchemeId, p.Day })
                .IsUnique();

            e.Property(p => p.Day)
                .HasConversion(new EnumToStringConverter<DayOfWeek>());
        });


    }
    public IQueryable<Result> Split(string input, string separator)
    => FromExpression(() => Split(input, separator)); }

public class DateValueConverter : ValueConverter<DateTime, DateTime>
{
    public DateValueConverter() : base(v => DateTime.SpecifyKind(v, DateTimeKind.Utc), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
public class Result
{
    public string Value { get; set; }
}
