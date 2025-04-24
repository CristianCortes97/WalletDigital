using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WalletApi.Application.Interface;
using System.Threading.Tasks;
using WalletApi.Infraestructure.Models;
using WalletApi.Application.Repository;

namespace WalletApi.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly WalletdigitaldbContext _context;
        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(WalletdigitaldbContext context)
        {
            _context = context;
            _repositories = new();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
               
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                object? repository = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(new Type[] {typeof(TEntity)}), new object[] {_context});
                _repositories.Add(type, repository);
            }

            return _repositories[type] as Repository<TEntity>;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}