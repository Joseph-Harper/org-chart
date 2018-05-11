using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrgChart.Core.Entities;
using OrgChart.Core.Interfaces;
using OrgChart.Data;
using OrgChart.Tests.Mocks;
using System;
using System.Linq;

namespace OrgChart.Tests.Data
{
    [TestFixture]
    public class OrgChartDbContextTests
    {
        private OrgChartDbContext _context;
        private readonly IUserProvider _userService = new MockUserService();

        [SetUp]
        public void SetUp()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrgChartDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new OrgChartDbContext(dbContextOptionsBuilder.Options, _userService);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _context = null;
        }

        [Test]
        public void DbContext_WhenQueried_ShouldOnlyIncludeEntitiesForUser()
        {
            var personSet = _context.Set<Person>();
            personSet.Add(new Person { UserId = "00000000-0000-0000-0000-000000000000" });
            personSet.Add(new Person { UserId = "11111111-1111-1111-1111-111111111111" });
            _context.SaveChanges();

            var people = personSet.ToList();
            Assert.AreEqual(1, people.Count);
        }
    }
}
