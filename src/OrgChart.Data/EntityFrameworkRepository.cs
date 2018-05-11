using Microsoft.EntityFrameworkCore;
using OrgChart.Core.Entities;
using OrgChart.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OrgChart.Data
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        private readonly OrgChartDbContext _context;

        public EntityFrameworkRepository(OrgChartDbContext orgChartDbContext) => _context = orgChartDbContext;

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(int id) => Delete(_context.Set<T>().Find(id));

        public T GetById(int id) => _context.Set<T>().Find(id);

        public T GetBySpecification(ISpecification<T> specification) => List(specification).FirstOrDefault();

        public IEnumerable<T> List() => _context.Set<T>().ToList();

        public IEnumerable<T> List(ISpecification<T> specification)
        {
            var query = _context.Set<T>().AsQueryable();

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            query = query.Where(specification.Predicate);

            return query.ToList();
        }

        public void Update(T entity)
        {
            _context.Attach(entity);
            _context.SaveChanges();
        }
    }
}