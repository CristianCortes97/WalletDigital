using WalletApi.Application.Interface;

namespace WalletApi.Application.UnitOfWork
{
    public interface IUnitOfWork
    {

        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        //ITransactionRepository<TEntity> ITransactionRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
