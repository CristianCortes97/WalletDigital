using System;
using System.Collections.Generic;

namespace WalletApi.Infraestructure.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int WalletId { get; set; }

    public decimal Amount { get; set; }

    public bool Type { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}
