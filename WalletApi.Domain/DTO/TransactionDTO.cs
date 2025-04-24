namespace WalletApi.Infraestructure.Models;

public partial class TransactionDTO
{
    public int Id { get; set; }

    public int WalletId { get; set; }

    public decimal Amount { get; set; }

    public bool Type { get; set; }

    public DateTime CreateAt { get; set; }

}
