using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> ListAll();
        // void Delete(TEntity entityToDelete);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> Where, params string[] includeProperties);
        List<TEntity> GetByPredicateList(Expression<Func<TEntity, bool>> Where, params string[] includeProperties);

        //IEnumerable<TEntity> GetExpression<Func<TEntity, bool>> filter = null,
        //  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
        TEntity GetByID(int id);
        //   IEnumerable<TEntity> GetWithRawSql(string query,
        //      params object[] parameters);
         TEntity Insert(TEntity entity);
        IEnumerable<TEntity> ListAllWithInclude(params string[] includeProperties);
        //     void Update(TEntity entityToUpdate);
    }
}
