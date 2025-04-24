using Microsoft.AspNetCore.Mvc;
using WalletApi.Domain.GenericModels;
using WalletApi.Domain.Interface;
using WalletApi.Domain.Service;
using WalletApi.Infraestructure.Models;

namespace WalletApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {


        private readonly IWalletService _walletService;


        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> GetAsync()
        {
            return Ok(await _walletService.GetAllWalletsAsync());
        }

        [HttpGet("GetWalletByIdAsync")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> GetWalletByIdAsync(int id)
        {
            return Ok(await _walletService.GetWalletByIdAsync(id));
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Create(WalletDTO createWalletDto)
        {
            return Ok(await _walletService.CreateWalletAsync(createWalletDto));
        }


        [HttpPut("Update")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Update(WalletDTO createWalletDto)
        {
            return Ok(await _walletService.UpdateWalletAsync(createWalletDto));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Delete(int id)
        {
            return Ok(await _walletService.DeleteWalletAsync(id));
        }


    }
}
