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


        /* 
         Descripción general
        API para gestionar transferencias de saldo entre billeteras. Permite operaciones CRUD sobre billeteras y operaciones CR sobre el historial de movimientos.
        /// <summary>
        /// Obtiene todas las billeteras registradas en el sistema
        /// </summary>
        /// <returns>Lista de billeteras</returns>
        /// <response code="200">Devuelve la lista de billeteras</response>
        /// <response code="500">Si hubo un error al obtener las billeteras</response>
         */

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

        /* 
       /// <summary>
        /// Obtiene una billetera por su ID
        /// </summary>
        /// <param name="id">ID de la billetera a buscar</param>
        /// <returns>Billetera encontrada</returns>
        /// <response code="200">Devuelve la billetera solicitada</response>
        /// <response code="404">Si no se encontró la billetera</response>
        /// <response code="500">Si hubo un error al obtener la billetera</response>
         */


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


        
        /// <summary>
        /// Crea una nueva billetera
        /// </summary>
        /// <param name="walletDto">Datos de la billetera a crear</param>
        /// <returns>Billetera creada</returns>
        /// <response code="201">Devuelve la billetera creada</response>
        /// <response code="400">Si los datos proporcionados son inválidos</response>
        /// <response code="409">Si ya existe una billetera con el mismo ID</response>
        /// <response code="500">Si hubo un error al crear la billetera</response>
         

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

        /// <summary>
        /// Actualiza una billetera existente
        /// </summary>
        /// <param name="walletDto">Datos actualizados de la billetera</param>
        /// <returns>Billetera actualizada</returns>
        /// <response code="200">Devuelve la billetera actualizada</response>
        /// <response code="400">Si los datos proporcionados son inválidos</response>
        /// <response code="404">Si no se encontró la billetera a actualizar</response>
        /// <response code="500">Si hubo un error al actualizar la billetera</response>

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


        /// <summary>
        /// Elimina una billetera por su ID
        /// </summary>
        /// <param name="id">ID de la billetera a eliminar</param>
        /// <returns>Billetera eliminada</returns>
        /// <response code="200">Devuelve la billetera eliminada</response>
        /// <response code="404">Si no se encontró la billetera</response>
        /// <response code="500">Si hubo un error al eliminar la billetera</response>

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


        /**
       * URL :/api/TransferFunds
        Método :POST
        Respuesta :
        Código: 200
        Contenido: truey mensaje de éxito
        Errores :
        Código: 400 - Monto inválido o saldo insuficiente
        Código: 404 - Billetera no encontrada
        Código: 500 - Error interno del servidor      
         */
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
