using System.ComponentModel.DataAnnotations;
using Application.Data.Dto;

namespace Application.Web.Models;

public class CustomerDetailsViewModel
{
    public required DateOnly Date { get; init; }
    public required IEnumerable<AnimalDto> SelectedAnimals { get; set; }
    
    [Required(ErrorMessage = "Voornaam is verplicht")]
    public required string FirstName { get; init; }
    
    public required string? Infix { get; init; }
    
    [Required(ErrorMessage = "Achternaam is verplicht")]
    public required string LastName { get; init; }
    
    [Required(ErrorMessage = "Adres is verplicht")]
    public required string Address { get; init; }
    
    [Required(ErrorMessage = "Plaats is verplicht")]
    public required string City { get; init; }
    
    public required string? Email { get; init; }
    public required string? PhoneNumber { get; init; }
}