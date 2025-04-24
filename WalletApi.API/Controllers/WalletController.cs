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
            var result = await _walletService.GetAllWalletsAsync();

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }

            return Ok(result);

        }

        [HttpGet("GetWalletByIdAsync/{id}")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> GetWalletByIdAsync(int id)
        {     
            var result = await _walletService.GetWalletByIdAsync(id);

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Create(WalletDTO createWalletDto)
        {
            var result = await _walletService.CreateWalletAsync(createWalletDto);

            if (result.Errors != null && result.Errors.Any())
            {
        
                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }

         
            return Ok(result);

        }


        [HttpPut("Update")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Update(WalletDTO createWalletDto)
        {          
            var result = await _walletService.UpdateWalletAsync(createWalletDto);

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }


            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Delete(int id)
        {       
            var result = await _walletService.DeleteWalletAsync(id);

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }


            return Ok(result);
        }

        [HttpPost("TransferFunds")]
        public async Task<ActionResult<Response<bool>>> TransferFunds(int sourceWalletId, int destinationWalletId, decimal amount)
        {
            var result = await _walletService.TransferFundsAsync(sourceWalletId, destinationWalletId, amount);

            if (result.Errors != null && result.Errors.Any())
            {        
                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }

            return Ok(result);
        }

    }
}
