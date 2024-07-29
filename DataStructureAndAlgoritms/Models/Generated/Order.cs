using System;
using System.Collections.Generic;

namespace DataStructureAndAlgorithms.Models;

public partial class Order
{
    public int OrdersId { get; set; }

    public DateOnly? OrdersDate { get; set; }

    public int? CustomerId { get; set; }

    public decimal? Mount { get; set; }

    public virtual Customer? Customer { get; set; }
}
