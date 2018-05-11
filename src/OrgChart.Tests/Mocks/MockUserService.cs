using OrgChart.Core.Interfaces;

namespace OrgChart.Tests.Mocks
{
    public class MockUserService : IUserProvider
    {
        public string GetUserId() => "00000000-0000-0000-0000-000000000000";
    }
}
