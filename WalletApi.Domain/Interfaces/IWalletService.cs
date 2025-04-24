using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain.GenericModels;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Domain.Interface
{
    public interface IWalletService
    {
        /// <summary>
        /// Obtiene todas las billeteras en el sistema
        /// </summary>
        /// <returns>Lista de billeteras o errores</returns>
        Task<Response<IEnumerable<WalletDTO>>> GetAllWalletsAsync();
        /// <summary>
        /// Obtiene una billetera por su ID
        /// </summary>
        /// <param name="Id">ID de la billetera</param>
        /// <returns>Billetera encontrada o errores</returns>
        Task<Response<IEnumerable<WalletDTO>>> GetWalletByIdAsync(int id);

        /// <summary>
        /// Crea una nueva billetera en el sistema
        /// </summary>
        /// <param name="createWalletDto">DTO con los datos para crear la billetera</param>
        /// <returns>Respuesta con la billetera creada o errores</returns>
        Task<Response<WalletDTO>> CreateWalletAsync(WalletDTO createWalletDto);

        /// <summary>
        /// Transfiere fondos entre dos billeteras
        /// </summary>
        /// <param name="sourceWalletId">ID de la billetera origen</param>
        /// <param name="destinationWalletId">ID de la billetera destino</param>
        /// <param name="amount">Monto a transferir</param>
        /// <returns>Estado de la operación o errores</returns>
        Task<Response<bool>> TransferFundsAsync( int sourceWalletId,  int destinationWalletId, decimal amount);
        /// <summary>
        /// Actualiza los datos de una billetera existente
        /// </summary>
        /// <param name="updateWalletDto">DTO con los datos de actualización</param>
        /// <returns>Billetera actualizada o errores</returns>
        Task<Response<WalletDTO>>UpdateWalletAsync(WalletDTO updateWalletDto);
        /// <summary>
        /// Elimina una billetera por su ID
        /// </summary>
        /// <param name="Id">ID de la billetera a eliminar</param>
        /// <returns>Respuesta con la billetera eliminada o errores</returns>
        Task<Response<WalletDTO>>DeleteWalletAsync(int id);
    }
}
