using WalletApi.Domain.GenericModels;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application.Interface
{
    public interface ITransactionService
    {
        /// <summary>
        /// Crea una nueva transacción en la base de datos
        /// </summary>
        /// <param name="createTransactionDto">DTO con los datos de la transacción a crear</param>
        /// <returns>Respuesta con los datos de la transacción creada o errores</returns>
        Task<Response<TransactionDTO>> CreateTransactionAsync(TransactionDTO createWalletDto);

        /// <summary>
        /// Obtiene todas las transacciones registradas en el sistema
        /// </summary>
        /// <returns>Respuesta con la lista de transacciones o errores</returns>
        Task<Response<IEnumerable<TransactionDTO>>> GetAllTransactionssAsync();     
    }
}
