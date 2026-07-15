using System;

namespace PaintStore.API.Dto;

public class OrderUpdateRequestDto
{
    public List<OrderItemRequestDto> PaintsOrders { get; set; }
    public int UserId { get; set; }
}
