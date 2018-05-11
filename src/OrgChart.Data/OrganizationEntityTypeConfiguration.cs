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
            builder.Metadata.FindNavigation(nameof(Organization.People)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();

            builder.HasMany(o => o.People).WithOne(p => p.Organization).HasForeignKey("OrganizationId");
        }
    }
}
