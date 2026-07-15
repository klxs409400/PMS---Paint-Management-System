using System;
using PaintStore.Model;
using PaintStore.Model.Models;

namespace PaintStore.API.Dto;

public class PaintProductResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public PaintType Type { get; set; }


    public decimal Price { get; set; }

    public int Stock { get; set; }
}
