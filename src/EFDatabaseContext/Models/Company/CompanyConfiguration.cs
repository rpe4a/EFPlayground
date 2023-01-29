using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDatabaseContext.Models.Company;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Table
        builder.ToTable("Company");

        // Keys
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes
        builder.HasIndex(c => c.Name);

        //QueryFilter
        builder.HasQueryFilter(c => !EF.Property<bool>(c, "IsDelete"));
        
        // Properties
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");
        builder.Property<string>(p => p.Name!)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(30);
        //shadow properties
        builder.Property<bool>("IsDelete")
            .HasColumnName("is_delete")
            .HasDefaultValue(false);
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