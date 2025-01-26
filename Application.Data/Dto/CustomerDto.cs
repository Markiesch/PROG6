namespace Application.Data.Dto;

public class CustomerDto
{
    public string FullName { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}