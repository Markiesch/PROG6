using Application.Data.Models;

namespace Application.Data.Dto;

public class AccountDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string? Infix { get; set; }
    public required string LastName { get; set; }
    public required string? Email { get; set; }
    public required string Address { get; set; }
    public required string? PhoneNumber { get; set; }
    public required CustomerCardType? CustomerCardType { get; set; }
    
    public string FullName => Infix == null ? $"{FirstName} {LastName}" : $"{FirstName} {Infix} {LastName}";
}