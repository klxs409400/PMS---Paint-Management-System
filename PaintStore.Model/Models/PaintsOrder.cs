using System;

namespace PaintStore.Model.Models;

public class PaintsOrder
{
    public int OrderId { get; set; }

    public Order Order { get; set; }

    public PaintProduct PaintProduct { get; set; }

    public int PaintProductId { get; set; }

    public int Id { get; set; }

    public int Quantity { get; set; }
}
