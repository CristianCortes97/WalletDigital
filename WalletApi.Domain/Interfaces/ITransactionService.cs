using WalletApi.Domain.GenericModels;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application.Interface
{
    public interface ITransactionService
    {
        Task<Response<TransactionDTO>> CreateTransactionAsync(TransactionDTO createWalletDto);
        Task<Response<IEnumerable<TransactionDTO>>> GetAllTransactionssAsync();     
    }
}
