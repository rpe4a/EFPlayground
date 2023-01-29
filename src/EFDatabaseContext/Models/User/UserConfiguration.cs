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
        // one-to-many
        builder.HasOne(u => u.Company)
            .WithMany()
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
        // one-to-one
        builder.HasOne(u => u.Profile)
            .WithOne(u => u.User)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(u => u.Login).IsUnique();
        
        //QueryFilter
        builder.HasQueryFilter(u => !EF.Property<bool>(u.Company, "IsDelete"));
        
        // Constraints

        // Properties
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");
        builder.Property(p => p.CompanyId)
            .IsRequired()
            .HasColumnName("company_id");
        builder.Property<string>(p => p.Login)
            .IsRequired()
            .HasColumnName("login")
            .HasMaxLength(100);
        builder.Property(p => p.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasMaxLength(50);
        //shadow properties
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