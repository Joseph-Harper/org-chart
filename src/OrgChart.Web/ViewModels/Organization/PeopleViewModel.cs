using System.Collections.Generic;

namespace OrgChart.Web.ViewModels.Organization
{
    public class PeopleViewModel
    {
        public int OrganizationId { get; set; }
        public IEnumerable<PersonViewModel> People { get; set; }
    }
}
