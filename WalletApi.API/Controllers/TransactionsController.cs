using Microsoft.AspNetCore.Mvc;
using WalletApi.Application.Interface;
using WalletApi.Domain.GenericModels;
using WalletApi.Domain.Interface;
using WalletApi.Domain.Service;
using WalletApi.Infraestructure.Models;

namespace WalletApi.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;


        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Response<IEnumerable<TransactionDTO>>>> GetAsync()
        {     
            var result = await _transactionService.GetAllTransactionssAsync();

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }

            return Ok(result);
        }



        [HttpPost("Create")]
        public async Task<ActionResult<Response<IEnumerable<WalletDTO>>>> Create(TransactionDTO createWalletDto)
        {       
            var result = await _transactionService.CreateTransactionAsync(createWalletDto);

            if (result.Errors != null && result.Errors.Any())
            {

                var firstError = result.Errors.First();
                return StatusCode(firstError.StatusCode, result);
            }


            return Ok(result);
        }

    }
}
