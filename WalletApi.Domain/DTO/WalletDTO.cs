namespace WalletApi.Infraestructure.Models;

public partial class WalletDTO
{
    public int Id { get; set; }

    public string DocumentId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

}
