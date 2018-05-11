using OrgChart.Core.Interfaces;

namespace OrgChart.Core.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}