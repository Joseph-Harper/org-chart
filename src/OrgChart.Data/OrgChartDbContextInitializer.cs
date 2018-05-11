using Microsoft.EntityFrameworkCore;
using OrgChart.Core.Entities;
using OrgChart.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OrgChart.Data
{
    public class SeedUserService : IUserProvider
    {
        private string _userId;
        public SeedUserService(string userId) => _userId = userId;
        public string GetUserId() => _userId;
    }
    public static class OrgChartDbContextInitializer
    {
        public static async Task SeedAsync(OrgChartDbContext context, string userId)
        {
            var organizations = context.Set<Organization>();

            if (!organizations.Any())
            {
                var dunderMifflin = new Organization
                {
                    Name = "Dunder Mifflin",
                    UserId = userId
                };
                await organizations.AddAsync(dunderMifflin);
                await context.SaveChangesAsync();

                var people = context.Set<Person>();

                var davidWallace = new Person
                {
                    UserId = userId,
                    FirstName = "David",
                    LastName = "Wallace",
                    EmailAddress = "dwallace@dundermifflin.com",
                    PhoneNumber = "111-111-1111",
                    Title = "Chief Financial Officer",
                    ReportsTo = null,
                    Organization = dunderMifflin
                };

                await people.AddAsync(davidWallace);
                await context.SaveChangesAsync();

                var janLevinson = new Person
                {
                    UserId = userId,
                    FirstName = "Jan",
                    LastName = "Levinson",
                    EmailAddress = "jlevinson@dundermifflin.com",
                    PhoneNumber = "222-222-2222",
                    ReportsTo = davidWallace,
                    Organization = dunderMifflin,
                    Title = "Vice President of Northeast Sales"
                };

                await people.AddAsync(janLevinson);
                await context.SaveChangesAsync();

                var michaelScott = new Person
                {
                    UserId = userId,
                    FirstName = "Michael",
                    LastName = "Scott",
                    EmailAddress = "mscott@dundermifflin.com",
                    PhoneNumber = "333-333-3333",
                    ReportsTo = janLevinson,
                    Organization = dunderMifflin,
                    Title = "Regional Manager"
                };

                await people.AddAsync(michaelScott);
                await context.SaveChangesAsync();

                var jimHalpert = new Person
                {
                    UserId = userId,
                    FirstName = "Jim",
                    LastName = "Halpert",
                    EmailAddress = "jhalpert@dundermifflin.com",
                    PhoneNumber = "444-444-4444",
                    ReportsTo = michaelScott,
                    Organization = dunderMifflin,
                    Title = "Assistant Regional Manager"
                };

                var dwightSchrute = new Person
                {
                    UserId = userId,
                    FirstName = "Dwight",
                    LastName = "Schrute",
                    EmailAddress = "dschrute@dundermifflin.com",
                    PhoneNumber = "555-555-5555",
                    ReportsTo = michaelScott,
                    Organization = dunderMifflin,
                    Title = "Assistant to the Regional Manager"
                };

                await people.AddRangeAsync(jimHalpert, dwightSchrute);
                await context.SaveChangesAsync();

                var phyllisLapin = new Person
                {
                    UserId = userId,
                    FirstName = "Phyllis",
                    LastName = "Lapin",
                    EmailAddress = "plapin@dundermifflin.com",
                    PhoneNumber = "666-666-6666",
                    ReportsTo = jimHalpert,
                    Organization = dunderMifflin,
                    Title = "Sales Representative"
                };

                var stanleyHudson = new Person
                {
                    UserId = userId,
                    FirstName = "Stanley",
                    LastName = "Hudson",
                    EmailAddress = "shudson@dundermifflin.com",
                    PhoneNumber = "777-777-7777",
                    ReportsTo = jimHalpert,
                    Organization = dunderMifflin,
                    Title = "Sales Representative"
                };

                var angelaMartin = new Person
                {
                    UserId = userId,
                    FirstName = "Angela",
                    LastName = "Martin",
                    EmailAddress = "amartin@dundermifflin.com",
                    PhoneNumber = "888-888-8888",
                    ReportsTo = jimHalpert,
                    Organization = dunderMifflin,
                    Title = "Senior Accountant"
                };

                await people.AddRangeAsync(phyllisLapin, stanleyHudson, angelaMartin);
                await context.SaveChangesAsync();

                var oscarMartinez = new Person
                {
                    UserId = userId,
                    FirstName = "Oscar",
                    LastName = "Martinez",
                    EmailAddress = "omartinez@dundermifflin.com",
                    PhoneNumber = "999-999-9999",
                    ReportsTo = angelaMartin,
                    Organization = dunderMifflin,
                    Title = "Accountant"
                };

                var kevinMalone = new Person
                {
                    UserId = userId,
                    FirstName = "Kevin",
                    LastName = "Malone",
                    EmailAddress = "kmalone@dundermifflin.com",
                    PhoneNumber = "000-000-0000",
                    ReportsTo = angelaMartin,
                    Organization = dunderMifflin,
                    Title = "Accountant"
                };

                await people.AddRangeAsync(oscarMartinez, kevinMalone);
                await context.SaveChangesAsync();

                var org = context.Set<Organization>().IgnoreQueryFilters().Where(o => o.UserId == userId).ToList();
                System.Console.WriteLine();
            }
        }
    }
}
