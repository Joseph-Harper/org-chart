using System.Collections.Generic;
using System.Linq;

namespace OrgChart.Core.Entities
{
    public class Organization : Entity
    {
        private readonly List<Person> _people = new List<Person>();

        public string Name { get; set; }
        public IReadOnlyCollection<Person> People => _people.AsReadOnly();

        public void AddPerson(string firstName, 
            string lastName, 
            string emailAddress, 
            string phoneNumber, 
            string title, 
            int? reportsToPersonId)
        {
            var person = new Person
            {
                UserId = UserId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                Title = title,
                ReportsTo = reportsToPersonId.HasValue ? 
                    _people.Single(p => p.Id == reportsToPersonId.Value) : null,
                Organization = this
            };

            _people.Add(person);
        }
    }
}
