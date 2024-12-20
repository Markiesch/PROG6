using System.ComponentModel.DataAnnotations;

namespace Application.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is verplicht")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht")]
    public required string Password { get; set; }
}