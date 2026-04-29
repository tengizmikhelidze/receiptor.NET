using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class CreateReceiptRequestDto
{
    public int? CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}