using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDatabaseContext.Models.User;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table
        builder.ToTable("User");

        // Keys
        builder.HasKey(u => u.Id);
        builder.HasOne(u => u.Company)
            .WithMany()
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(u => u.Profile)
            .WithOne(u => u.User)
            .HasForeignKey<User>(u => u.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(u => u.Login).IsUnique();

        // Constraints

        // Properties
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");
        builder.Property(p => p.CompanyId)
            .IsRequired()
            .HasColumnName("company_id");
        builder.Property(p => p.ProfileId)
            .IsRequired()
            .HasColumnName("profile_id");
        builder.Property<string>(p => p.Login)
            .IsRequired()
            .HasColumnName("login")
            .HasMaxLength(100);
        builder.Property(p => p.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasMaxLength(50);
        builder.Property<DateTime>("CreateAt")
            .HasColumnName("create_at")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("DATETIME('now')");
        builder.Property<DateTime>("UpdateAt")
            .HasColumnName("update_at")
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("DATETIME('now')");
        builder.Property<long>("Version")
            .HasColumnName("version")
            .IsRowVersion()
            .HasDefaultValue(0);
    }
}