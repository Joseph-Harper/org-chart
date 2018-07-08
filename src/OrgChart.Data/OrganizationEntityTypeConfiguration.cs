using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgChart.Core.Entities;

namespace OrgChart.Data
{
    public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("OrganizationId").IsRequired();
            builder.Property(o => o.UserId).HasColumnName("UserId").HasMaxLength(450).IsRequired();
            builder.Property(o => o.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();

            builder.Metadata.FindNavigation(nameof(Organization.People)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(o => o.People).WithOne(p => p.Organization).HasForeignKey("OrganizationId");
        }
    }
}
