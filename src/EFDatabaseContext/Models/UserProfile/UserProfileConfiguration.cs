using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDatabaseContext.Models.UserProfile;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        // Table
        builder.ToTable("Profile");

        // Keys
        builder.HasKey(u => u.Id);
        builder.HasOne(u => u.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(u => u.Name);

        // Constraints
        builder.HasCheckConstraint("Age", "Age > 0 AND Age < 150");

        // Properties
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");
        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnName("user_id");
        builder.Property<string>(p => p.Name!)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(50);
        builder.Property(p => p.Age)
            .IsRequired()
            .HasColumnName("age")
            .HasMaxLength(3);
        //shadow properties
        builder.Property<DateTime>("CreateAt")
            .HasColumnName("create_at")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("DATETIME('now')");
        builder.Property<DateTime>("UpdateAt")
            .HasColumnName("update_at")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("DATETIME('now')");
        builder.Property<long>("Version")
            .HasColumnName("version")
            .IsRowVersion()
            .HasDefaultValue(0);

        builder.OwnsOne<OfficialData>(p => p.OfficialData, b =>
        {
            b.HasIndex(u => u.Passport).IsUnique();
            b.HasIndex(u => u.Phone).IsUnique();

            b.Property(od => od.Passport)
                .IsRequired()
                .HasColumnName("passport")
                .HasMaxLength(50);

            b.Property(od => od.Phone)
                .IsRequired()
                .HasColumnName("phone")
                .HasMaxLength(20);
        });
    }
}