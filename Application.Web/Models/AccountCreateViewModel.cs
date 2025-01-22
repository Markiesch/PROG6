using Application.Data.Models;

namespace Application.Web.Models;

public class AccountCreateViewModel
{
    public string FirstName { get; set; }
    public string Infix { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public CustomerCardType? CustomerCardType { get; set; }
}