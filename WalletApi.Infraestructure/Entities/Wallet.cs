using System;
using System.Collections.Generic;

namespace WalletApi.Infraestructure.Models;

public partial class Wallet
{
    /// <summary>
    /// Identificador único autogenerado
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Documento de identidad del propietario
    /// </summary>
    public string DocumentId { get; set; } = null!;

    /// <summary>
    /// Nombre del propietario
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Saldo disponible en la billetera
    /// </summary>
    public decimal Balance { get; set; }
    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreateAt { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime UpdateAt { get; set; }

    /// <summary>
    /// Transacciones asociadas a esta billetera
    /// </summary>
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
