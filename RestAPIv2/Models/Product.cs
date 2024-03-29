﻿using System;
using System.Collections.Generic;

namespace RestAPIv2.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public string? Color { get; set; }

    public float UnitPrice { get; set; }

    public int AvailableQuantity { get; set; }
}
