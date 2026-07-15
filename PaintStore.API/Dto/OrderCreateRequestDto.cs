using System;

namespace PaintStore.API.Dto;

public class OrderCreateRequestDto
{
    public List<OrderItemRequestDto> PaintsOrders { get; set; }
    public int UserId { get; set; }
}
