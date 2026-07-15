using System;

namespace PaintStore.API.Dto;

public class OrderItemRequestDto
{
    public int Quantity { get; set; }

    public int PaintProductId { get; set; }
}
