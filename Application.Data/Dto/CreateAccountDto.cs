using System.ComponentModel.DataAnnotations;
using Application.Data.Models;

namespace Application.Data.Dto;

public class CreateAccountDto
{
    public required string FirstName { get; set; }
    public required string? Infix { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string Email { get; set; }
    public required string? PhoneNumber { get; set; }
    public required CustomerCardType? CustomerCardType { get; set; }
}