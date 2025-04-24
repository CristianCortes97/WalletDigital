using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Domain.GenericModels
{
    public class Response<T>
    {
        public T? Body { get; set; }

        public IEnumerable<Error>? Errors { get; set; } 

    }
}
