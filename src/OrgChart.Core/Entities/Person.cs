namespace OrgChart.Core.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public Person ReportsTo { get; set; }
        public Organization Organization { get; set; }
    }
}
