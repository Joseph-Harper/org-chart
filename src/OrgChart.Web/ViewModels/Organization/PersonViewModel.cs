using System.ComponentModel.DataAnnotations;

namespace OrgChart.Web.ViewModels.Organization
{
    public class PersonViewModel
    {
        public PersonViewModel(Core.Entities.Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            EmailAddress = person.EmailAddress;
            PhoneNumber = person.PhoneNumber;
            Title = person.Title;
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}
