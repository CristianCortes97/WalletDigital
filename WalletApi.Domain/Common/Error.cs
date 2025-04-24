using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Domain.GenericModels
{
    public class Error
    {
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        
       
    }
}
