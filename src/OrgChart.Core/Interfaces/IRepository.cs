using OrgChart.Core.Entities;
using System.Collections.Generic;

namespace OrgChart.Core.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        T GetById(int id);
        T GetBySpecification(ISpecification<T> specifiation);
        IEnumerable<T> List();
        IEnumerable<T> List(ISpecification<T> specification);
        void Update(T entity);
    }
}
