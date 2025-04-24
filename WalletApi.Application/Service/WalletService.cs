using AutoMapper;
using Microsoft.Extensions.Logging;
using WalletApi.Application.UnitOfWork;
using WalletApi.Domain.GenericModels;
using WalletApi.Domain.Interface;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Domain.Service
{
    public class WalletService : IWalletService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<IWalletService> _logger;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IWalletService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<WalletDTO>> CreateWalletAsync(WalletDTO createWalletDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createWalletDto.Name))
                {
                    return new()
                    {
                        Errors = new List<Error>() { new Error() { Message = "El nombre de la billetera no puede estar vacío.", StatusCode = 400, Success = false } }
                    };
                }

                if (await _unitOfWork.Repository<Wallet>().ExistsAsync(X => X.Id == createWalletDto.Id))
                {
                    return new()
                    {
                        Errors = new List<Error>() { new Error() { Message = "Ya existe una billetera con este ID.", StatusCode = 409, Success = false } }
                    };
                }

                Wallet wallet = _mapper.Map<Wallet>(createWalletDto);
                await _unitOfWork.Repository<Wallet>().CreateAsync(wallet);
                await _unitOfWork.SaveChangesAsync();
                return new Response<WalletDTO>()
                {
                    Body = _mapper.Map<WalletDTO>(wallet)
                };
            }
            catch (Exception ex)
            {

                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al crear la billetera.", StatusCode = 500, Success = false } }
                };
            }
        }

        public async Task<Response<WalletDTO>> DeleteWalletAsync(int Id)
        {

            try
            {


                Wallet wallet = (await _unitOfWork.Repository<Wallet>().GetAllAsync(x => x.Id == Id)).FirstOrDefault();
                if (wallet != null)
                {

                    _unitOfWork.Repository<Wallet>().DeleteAsync(wallet);
                    await _unitOfWork.SaveChangesAsync();               

                    return new Response<WalletDTO>()
                    {
                        Body = _mapper.Map<WalletDTO>(wallet)
                    };
                }
                // Si no existe la billetera, retornar un error
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "No existe una billetera con el ID proporcionado.", StatusCode = 404, Success = false } }
                };

            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al eliminar la billetera.", StatusCode = 500, Success = false } }
                };
            }
            
            

        }

        public async Task<Response<IEnumerable<WalletDTO>>> GetAllWalletsAsync()
        {
            try
            {

                return new Response<IEnumerable<WalletDTO>>()
                {
                    Body = _mapper.Map<IEnumerable<WalletDTO>>(await _unitOfWork.Repository<Wallet>().GetAllAsync()).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al obtener las billeteras.", StatusCode = 500, Success = false } }
                };
            }
        }

        public async Task<Response<IEnumerable<WalletDTO>>> GetWalletByIdAsync(int Id)
        {
            try
            {
                var wallet = await _unitOfWork.Repository<Wallet>().GetAllAsync(x => x.Id == Id);
                if (!wallet.Any())
                {
                    return new Response<IEnumerable<WalletDTO>>()
                    {
                        Errors = new List<Error>() { new Error() { Message = "No se encontró ninguna billetera con el ID proporcionado.", StatusCode = 404, Success = false } }
                    };
                }
                return new Response<IEnumerable<WalletDTO>>()
                {
                    Body = _mapper.Map<IEnumerable<WalletDTO>>(wallet)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al obtener la billetera.", StatusCode = 500, Success = false } }
                };
            }
        }

        public async Task<Response<WalletDTO>> UpdateWalletAsync( WalletDTO updateWalletDto)
        {
            try
            {
                // Validación: Asegurar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(updateWalletDto.Name))
                {
                    return new()
                    {
                        Errors = new List<Error>() { new Error() { Message = "El nombre de la billetera no puede estar vacío.", StatusCode = 400, Success = false } }
                    };
                }

                Wallet wallet = (await _unitOfWork.Repository<Wallet>().GetAllAsync(x => x.Id == updateWalletDto.Id)).FirstOrDefault();

                if (wallet != null)
                {
                    _mapper.Map(updateWalletDto, wallet);
                    _unitOfWork.Repository<Wallet>().UpdateAsync(wallet);
                    await _unitOfWork.SaveChangesAsync();
                    return new Response<WalletDTO>()
                    {
                        Body = _mapper.Map<WalletDTO>(wallet)
                    };
                }

                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "No existe una billetera con el ID proporcionado para actualizar.", StatusCode = 404, Success = false } }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al actualizar la billetera.", StatusCode = 500, Success = false } }
                };
            }        
        }

         public async Task<Response<bool>> TransferFundsAsync(int sourceWalletId, int destinationWalletId, decimal amount)
        {
            try
            {
                // Validación 1: Monto mayor que cero
                if (amount <= 0)
                {
                    return new Response<bool>()
                    {
                        Errors = new List<Error>() { new Error() { Message = "El monto de la transferencia debe ser mayor que cero.", StatusCode = 400, Success = false } }
                    };
                }

                // Validación 2: Obtener billetera origen
                var sourceWallet = (await _unitOfWork.Repository<Wallet>().GetAllAsync(w => w.Id == sourceWalletId)).FirstOrDefault();
                if (sourceWallet == null)
                {
                    return new Response<bool>()
                    {
                        Errors = new List<Error>() { new Error() { Message = "No existe la billetera de origen.", StatusCode = 404, Success = false } }
                    };
                }

                // Validación 3: Obtener billetera destino
                var destinationWallet = (await _unitOfWork.Repository<Wallet>().GetAllAsync(w => w.Id == destinationWalletId)).FirstOrDefault();
                if (destinationWallet == null)
                {
                    return new Response<bool>()
                    {
                        Errors = new List<Error>() { new Error() { Message = "No existe la billetera de destino.", StatusCode = 404, Success = false } }
                    };
                }

                // Validación 4: saldo insuficiente
                if (sourceWallet.Balance < amount)
                {
                    return new Response<bool>()
                    {
                        Errors = new List<Error>() { new Error() { Message = "Saldo insuficiente en la billetera de origen.", StatusCode = 400, Success = false } }
                    };
                }

              
                // Realizar la transferencia
                sourceWallet.Balance -= amount;
                destinationWallet.Balance += amount;

                // Actualizar las billeteras
                _unitOfWork.Repository<Wallet>().UpdateAsync(sourceWallet);
                _unitOfWork.Repository<Wallet>().UpdateAsync(destinationWallet);

                // Crear DTOs de transacciones
                var debitTransactionDto = new TransactionDTO
                {
                    WalletId = sourceWalletId,
                    Amount = amount,
                    Type = false,
                    CreateAt = DateTime.UtcNow
                };

                var creditTransactionDto = new TransactionDTO
                {
                    WalletId = destinationWalletId,
                    Amount = amount,
                    Type = true,
                    CreateAt = DateTime.UtcNow
                };

                
                var debitTransaction = _mapper.Map<Transaction>(debitTransactionDto);
                var creditTransaction = _mapper.Map<Transaction>(creditTransactionDto);

               
                await _unitOfWork.Repository<Transaction>().CreateAsync(debitTransaction);
                await _unitOfWork.Repository<Transaction>().CreateAsync(creditTransaction);


                await _unitOfWork.SaveChangesAsync();
         
                return new Response<bool>() { Body = true, Message = "La Transeferencia ha sido exitosa" }; // Transferencia exitosa
            }
            catch (Exception)
            {
               ;

                return new Response<bool>()
                {
                    Errors = new List<Error>() { new Error() { Message = "Error al realizar la transferencia.", StatusCode = 500, Success = false } }
                };
            }
        }
    }
}
    

    

