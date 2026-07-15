using System;

namespace PaintStore.API.Dto;

public class UserResponseDto
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;


}
