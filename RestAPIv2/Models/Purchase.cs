using System;
using System.Collections.Generic;

namespace RestAPIv2.Models;

public partial class Purchase
{
    public int PruchaseId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public string? ReferenceId { get; set; }

    public string? Status { get; set; }

    public DateTime? PurchaseDate { get; set; }
}
