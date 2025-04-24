using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WalletApi.Infraestructure.Models;

namespace WalletApi.Application
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            // Mapeo para Wallet
            CreateMap<WalletDTO,Wallet>().ForAllMembers(opts => opts.Condition((src, Dest, srcMember) => srcMember != null));
            CreateMap<Wallet,WalletDTO>();

            // Mapeo para Transaction
            CreateMap<TransactionDTO, Transaction>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Transaction, TransactionDTO>();
        } 
    }
}
