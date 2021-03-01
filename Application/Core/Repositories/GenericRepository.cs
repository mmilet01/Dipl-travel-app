using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected mmiletaContext _context;
        public GenericRepository(mmiletaContext context)
        {
            _context = context;
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetByPredicate(Expression<Func<T, bool>> Where, params string[] includeProperties)
        {
            T entity;
            var query = _context.Set<T>().AsQueryable();

            foreach (string navigationProperty in includeProperties)
                query = query.Include(navigationProperty); //got to reaffect it.
            entity = query.Where(Where).FirstOrDefault();
            return entity;
        }

        public List<T> GetByPredicateList(Expression<Func<T, bool>> Where, params string[] includeProperties)
        {
            List<T> entity;
            var query = _context.Set<T>().AsQueryable();

            foreach (string navigationProperty in includeProperties)
                query = query.Include(navigationProperty); //got to reaffect it.
            entity = query.Where(Where).ToList();
            return entity;
        }

        public T Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> ListAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> ListAllWithInclude(params string[] includeProperties)
        {
            IEnumerable<T> entity;
            var query = _context.Set<T>().AsQueryable();
            foreach (string navigationProperty in includeProperties)
                query = query.Include(navigationProperty); //got to reaffect it.
            entity = query.ToList();
            return entity;
        }
    }
}
