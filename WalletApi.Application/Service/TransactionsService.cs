using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WalletApi.Application.Interface;
using WalletApi.Application.UnitOfWork;
using WalletApi.Domain.GenericModels;
using WalletApi.Domain.Interface;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application.Service
{
    public class TransactionsService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<IWalletService> _logger;


        public TransactionsService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IWalletService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<TransactionDTO>> CreateTransactionAsync(TransactionDTO createWalletDto)
        {
            try
            {            

                if (await _unitOfWork.Repository<Transaction>().ExistsAsync(X => X.Id == createWalletDto.Id))
                {
                    return new()
                    {
                        Errors = new List<Error>() { new Error() { Message = "Ya existe una billetera con este ID.", StatusCode = 409, Success = false } }
                    };
                }

                Transaction wallet = _mapper.Map<Transaction>(createWalletDto);
                await _unitOfWork.Repository<Transaction>().CreateAsync(wallet);
                await _unitOfWork.SaveChangesAsync();
                return new Response<TransactionDTO>()
                {
                    Body = _mapper.Map<TransactionDTO>(wallet)
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

        public async Task<Response<IEnumerable<TransactionDTO>>> GetAllTransactionssAsync()
        {

            try
            {

                return new Response<IEnumerable<TransactionDTO>>()
                {
                    Body = _mapper.Map<IEnumerable<TransactionDTO>>(await _unitOfWork.Repository<Transaction>().GetAllAsync()).ToList()
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

    }
}


