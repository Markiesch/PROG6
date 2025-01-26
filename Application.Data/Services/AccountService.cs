using System.Text;
using Application.Data.Dto;
using Application.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Data.Services;

public class AccountService(MainContext context, UserManager<User> userManager)
{
    public async Task<AccountDto?> GetAccount(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        return new AccountDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            Infix = user.Infix,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            CustomerCardType = user.CustomerCardType
        };
    }

    public async Task<CustomerCardType?> GetCustomerCardFromUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        return user?.CustomerCardType;
    }

    public async Task<(string?, Dictionary<string, string> Errors)> CreateAccount(CreateAccountDto dto)
    {
        var errors = new Dictionary<string, string>();
        
        if (await userManager.FindByNameAsync(dto.Email) != null)
        {
            errors["Email"] = "Deze email is al in gebruik";
            return (null, errors);
        }
        
        var user = new User
        {
            UserName = dto.Email,
            FirstName = dto.FirstName,
            Infix = dto.Infix,
            LastName = dto.LastName,
            Email = dto.Email,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            CustomerCardType = dto.CustomerCardType
        };
        var password = CreateRandomPassword();

        var result = await userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, UserRole.Customer.ToString());
            return (password, errors);
        }
        
        foreach (var error in result.Errors)
        {
            errors[error.Code] = error.Description;
        }
        
        return (null, errors);
    }

    private string CreateRandomPassword()
    {
        const int length = 12;

        var password = new StringBuilder();
        var random = new Random();
        for (var i = 0; i < length; i++)
        {
            password.Append((char)random.Next(33, 126));
        }

        return password.ToString();
    }
}