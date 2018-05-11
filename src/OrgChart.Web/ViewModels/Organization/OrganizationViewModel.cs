using System.ComponentModel.DataAnnotations;

namespace OrgChart.Web.ViewModels.Organization
{
    public class OrganizationViewModel
    {
        public OrganizationViewModel(Core.Entities.Organization organization)
        {
            Id = organization.Id;
            Name = organization.Name;
            PeopleCount = organization.People.Count;
        }
        public int Id { get; set; }

        [Display(Name = "Organization")]
        public string Name { get; set; }

        [Display(Name = "People")]
        public int PeopleCount { get; set; }
    }
}
