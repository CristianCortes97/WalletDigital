using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>>? GetAllAsync(
            Expression<Func<T, bool>>?  filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool?  tracking = false
        );

        Task<IEnumerable<T>> GetTransactionsByIdAsync(int id);
        Task CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T _entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
    }

}
