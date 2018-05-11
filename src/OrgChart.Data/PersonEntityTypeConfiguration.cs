using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgChart.Core.Entities;

namespace OrgChart.Data
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property<int>("OrganizationId");
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.EmailAddress).HasMaxLength(255);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20);
            builder.Property(p => p.Title).HasMaxLength(100);
        }
    }
}
