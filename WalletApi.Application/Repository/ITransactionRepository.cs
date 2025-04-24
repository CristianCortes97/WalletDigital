using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.Interface
{
    public interface ITransactionRepository<T>
    {   
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByWalletIdAsync(int walletId);
        Task CreateAsync(T transaction);
    }
}
