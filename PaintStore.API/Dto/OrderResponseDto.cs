using System;

namespace PaintStore.API.Dto;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<OrderItemResponseDto> PaintsOrders { get; set; }

    public decimal TotalPrice { get; set; }

}
