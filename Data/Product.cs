using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brickwell.Data;

public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public int? NumParts { get; set; }

    public int Price { get; set; }

    public string? ImgLink { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }
}
