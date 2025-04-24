namespace WalletApi.Infraestructure.Models;

public partial class TransactionDTO
{
    /// <summary>
    /// Identificador único de la transacción
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la billetera asociada a la transacción
    /// </summary>
    public int WalletId { get; set; }

    /// <summary>
    /// Monto de la transacción
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Tipo de operación: true para Crédito, false para Débito
    /// </summary>
    public bool Type { get; set; }
    /// <summary>
    /// Fecha de creación de la transacción
    /// </summary>
    public DateTime CreateAt { get; set; }

}
