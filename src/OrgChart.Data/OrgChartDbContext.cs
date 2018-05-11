using Microsoft.EntityFrameworkCore;
using OrgChart.Core.Entities;
using OrgChart.Core.Interfaces;

namespace OrgChart.Data
{
    public class OrgChartDbContext : DbContext
    {
        private readonly string _userId;

        public OrgChartDbContext(DbContextOptions<OrgChartDbContext> dbContextOptions, IUserProvider userService)
            : base(dbContextOptions)
        {
            _userId = userService.GetUserId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.Entity<Person>().HasQueryFilter(p => p.UserId == _userId);
            modelBuilder.Entity<Organization>().HasQueryFilter(o => o.UserId == _userId);
        }
    }
}