using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brickwell.Data;

public partial class Order
{
    [Key]
    public int TransactionId { get; set; }

    public int CustomerId { get; set; }

    public string Date { get; set; } = null!;

    public string? DayOfWeek { get; set; }

    public int? Time { get; set; }

    public string EntryMode { get; set; } = null!;

    public int? Amount { get; set; }

    public string TypeOfTransaction { get; set; } = null!;

    public string? CountryOfTransaction { get; set; }

    public string? ShippingAddress { get; set; }

    public string Bank { get; set; } = null!;

    public string TypeOfCard { get; set; } = null!;

    public bool Fraud { get; set; }
}
