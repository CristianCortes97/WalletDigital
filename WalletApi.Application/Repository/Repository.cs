using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletApi.Application.Interface;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WalletdigitaldbContext Context;
        private DbSet<T> _entities;

        public Repository(WalletdigitaldbContext context)
        {
            Context = context;
            _entities = Context.Set<T>();
        }

        public virtual async Task CreateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await _entities.AddAsync(entity);
        }

        public virtual void DeleteAsync(T? entitytoDelete)
        {
            if (entitytoDelete != null)
                if (Context.Entry(entitytoDelete).State == EntityState.Detached)
                {
                    _entities.Attach(entitytoDelete);
                }
            _entities.Remove(entitytoDelete);



        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
           
           return await _entities.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetByIdAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>>? GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool? tracking = false)
        {
            if (tracking.HasValue && tracking.Value)
            {
                return await BuildQuery(filter, orderBy, includeProperties).ToListAsync();
            }

           return await BuildQuery(filter, orderBy, includeProperties).AsNoTracking().ToListAsync();
        }

        private IQueryable<T>? BuildQuery(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
           IQueryable<T> query = _entities;
            if (filter != null)
            {
               query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                 (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

           
            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;


        }

        public virtual void UpdateAsync(T entity)
        {
            _entities.Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
          
           
        }

    }
}