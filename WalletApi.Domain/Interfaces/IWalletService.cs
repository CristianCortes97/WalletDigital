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
        Task<Response<IEnumerable<WalletDTO>>> GetAllWalletsAsync();
        Task<Response<IEnumerable<WalletDTO>>> GetWalletByIdAsync(int id);
        Task<Response<WalletDTO>> CreateWalletAsync(WalletDTO createWalletDto);
        Task<Response<WalletDTO>>UpdateWalletAsync(WalletDTO updateWalletDto);
        Task<Response<WalletDTO>>DeleteWalletAsync(int id);
    }
}
