﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brickwell.Data;

public partial class AspNetRoleClaim
{
    [Key]
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AspNetRole Role { get; set; } = null!;
}
