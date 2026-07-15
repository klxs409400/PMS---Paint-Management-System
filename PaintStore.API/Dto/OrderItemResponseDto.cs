using System;
using PaintStore.Model;

namespace PaintStore.API.Dto;

public class OrderItemResponseDto
{
    public int Quantity { get; set; }

    public int PaintProductId { get; set; }

    public string PaintName { get; set; }

    public PaintType PaintType { get; set; }
}
