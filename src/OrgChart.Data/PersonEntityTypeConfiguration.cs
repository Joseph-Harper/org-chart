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
            builder.Property(p => p.Id).HasColumnName("PersonId").IsRequired();
            builder.Property(p => p.UserId).HasColumnName("UserId").HasMaxLength(450).IsRequired();
            builder.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(255).IsRequired();
            builder.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(255).IsRequired();
            builder.Property(p => p.EmailAddress).HasColumnName("EmailAddress").HasMaxLength(255);
            builder.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(20);
            builder.Property(p => p.Title).HasColumnName("Title").HasMaxLength(100);
            builder.Property<int>("OrganizationId");
        }
    }
}
