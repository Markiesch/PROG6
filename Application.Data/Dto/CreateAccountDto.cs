using System.ComponentModel.DataAnnotations;
using Application.Data.Models;

namespace Application.Data.Dto;

public class CreateAccountDto
{
    [Required(ErrorMessage = "Voornaam is verplicht")]
    public required string FirstName { get; set; }
    public required string? Infix { get; set; }
    [Required(ErrorMessage = "Achternaam is verplicht")]
    public required string LastName { get; set; }
    [Required(ErrorMessage = "Adres is verplicht")]
    public required string Address { get; set; }
    [Required(ErrorMessage = "Email is verplicht")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Telefoonnummer is verplicht")]
    public required string? PhoneNumber { get; set; }
    public required CustomerCardType? CustomerCardType { get; set; }
}