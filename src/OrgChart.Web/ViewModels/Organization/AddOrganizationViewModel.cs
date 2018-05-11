using System.ComponentModel.DataAnnotations;

namespace OrgChart.Web.ViewModels.Organization
{
    public class AddOrganizationViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
