using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Data.Services;

public class AccountService(MainContext context)
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
}