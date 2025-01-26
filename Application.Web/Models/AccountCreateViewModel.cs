using System.ComponentModel.DataAnnotations;
using Application.Data.Models;

namespace Application.Web.Models;

public class AccountCreateViewModel
{
    [Required(ErrorMessage = "Voornaam is verplicht")]
    public string FirstName { get; set; }
    public string? Infix { get; set; }
    [Required(ErrorMessage = "Achternaam is verplicht")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Adres is verplicht")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Email is verplicht")]
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public CustomerCardType? CustomerCardType { get; set; }
}