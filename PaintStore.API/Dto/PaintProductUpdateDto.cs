using System;
using PaintStore.Model;

namespace PaintStore.API.Dto;

public class PaintProductUpdateDto
{
    public string Name { get; set; } = null!;

    public PaintType Type { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
