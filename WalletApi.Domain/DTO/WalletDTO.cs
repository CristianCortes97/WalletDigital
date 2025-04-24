namespace WalletApi.Infraestructure.Models;

public partial class WalletDTO
{
    /// <summary>
    // Identificador único de la billetera
    /// </summary>
    public int Id { get; set; }


    /// <summary>
    /// Documento de identidad del propietario
    /// </summary>
    public string DocumentId { get; set; } = null!;

    /// <summary>
    /// Nombre del propietario de la billetera
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Saldo disponible en la billetera
    /// </summary>
    public decimal Balance { get; set; }
    /// <summary>
    /// Fecha de creación de la billetera
    /// </summary>
    public DateTime CreateAt { get; set; }
    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime UpdateAt { get; set; }

}
