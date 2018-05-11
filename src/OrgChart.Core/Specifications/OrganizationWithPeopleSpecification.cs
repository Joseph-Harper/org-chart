using OrgChart.Core.Entities;

namespace OrgChart.Core.Specifications
{
    public class OrganizationWithPeopleSpecification : Specification<Organization>
    {
        public OrganizationWithPeopleSpecification() : base(o => true)
        {
            AddInclude(o => o.People);
        }

        public OrganizationWithPeopleSpecification(int organizationId) : base(o => o.Id == organizationId)
        {
            AddInclude(o => o.People);
        }
    }
}
