using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OrgChart.Web.ViewModels.Organization
{
    public class AddPersonViewModel
    {
        public AddPersonViewModel()
        {

        }

        public AddPersonViewModel(Core.Entities.Organization organization)
        {
            OrganizationId = organization.Id;

            var people = organization.People.Select(p => new PersonListItemViewModel
            {
                Id = p.Id,
                FullName = p.FirstName + " " + p.LastName
            });

            People = new SelectList(people, 
                nameof(PersonListItemViewModel.Id), 
                nameof(PersonListItemViewModel.FullName));
        }
        [Required]
        [HiddenInput]
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        [Display(Name = "Reports To")]
        public int? ReportsToPersonID { get; set; }
        public SelectList People { get; set; }
    }
}
