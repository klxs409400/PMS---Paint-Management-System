using System;

namespace PaintStore.API.Dto;

public class UserCreateRequestDto
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
