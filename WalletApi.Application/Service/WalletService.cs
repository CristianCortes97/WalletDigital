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
                if (await _unitOfWork.Repository<Wallet>().ExistsAsync(X => X.Id == createWalletDto.Id))
                {
                    return new()
                    {
                        Errors = new List<Error>() { new Error() { Message = "Ya exite este numero de registro", StatusCode = 0, Success = false } }
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
                    Errors = new List<Error>() { new Error() { Message = "No puede retornar", StatusCode = 0, Success = false } }
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
                    //return new Response<WalletDTO>()
                    //{
                    //    Errors = new List<Error>() { new Error() { Message = "No existe la wallet", StatusCode = 404, Success = false } }
                    //};

                    return new Response<WalletDTO>()
                    {
                        Body = _mapper.Map<WalletDTO>(wallet)
                    };
                }
                
            }
            catch (Exception ex)
            {

                _logger.LogError("{ex}", ex);
            }
            
            return new()
            {
                Errors = new List<Error>() { new Error() { Message = "No existe el registro para borrar en base de datos", StatusCode = 0, Success = false } }
            };

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
                    Errors = new List<Error>() { new Error() { Message = "No puede retornar", StatusCode = 0, Success = false } }
                };
            }
        }

        public async Task<Response<IEnumerable<WalletDTO>>> GetWalletByIdAsync(int Id)
        {
            try
            {
                return new Response<IEnumerable<WalletDTO>>() { 
                    Body =  _mapper.Map<IEnumerable<WalletDTO>>(await _unitOfWork.Repository<Wallet>().GetAllAsync(x => x.Id == Id))
                };
            }
            catch (Exception ex)
            {


                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "No puede retornar", StatusCode = 0, Success = false } }
                };
            }
        }

        public async Task<Response<WalletDTO>> UpdateWalletAsync( WalletDTO updateWalletDto)
        {
            try
            {
                Wallet wallet = (await _unitOfWork.Repository<Wallet>().GetAllAsync(x => x.Id == updateWalletDto.Id)).FirstOrDefault();
                if (updateWalletDto != null)
                {
                    _mapper.Map(updateWalletDto, wallet);
                    _unitOfWork.Repository<Wallet>().UpdateAsync(wallet);
                    await _unitOfWork.SaveChangesAsync();
                    return new Response<WalletDTO>()
                    {
                        Body = _mapper.Map<WalletDTO>(wallet)
                    };
                }
            }
            catch (Exception ex)
            {


                _logger.LogError("{ex}", ex);
                return new()
                {
                    Errors = new List<Error>() { new Error() { Message = "No se puede actualizar el registro", StatusCode = 0, Success = false } }
                };
            }

            return new()
            {
                Errors = new List<Error>() { new Error() { Message = "No puede retornar", StatusCode = 0, Success = false } }
            };


        }

    }
}
