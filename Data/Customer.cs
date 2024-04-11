using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brickwell.Data;

public partial class Customer
{
    [Key]
    public int customer_ID { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string? birth_date { get; set; } = null!;

    public string? country_of_residence { get; set; }

    public string? gender { get; set; }

    public double? age { get; set; }
}
