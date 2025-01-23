using System.Security.Claims;
using System.Text.Json;
using Application.Data.Dto;
using Application.Data.Models;
using Application.Data.Services;
using Application.Web.Models;
using Application.Web.Rules;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;
[Route("/booking")]

public class BookingController(AnimalService animalService, AccountService accountService) : Controller
{
    [HttpGet("pick-your-animal/")]
    public async Task<IActionResult> PickYourAnimal()
    {
        var date = GetValidDateFromSession();
        var animals = await animalService.GetAnimalsWithAvailability(date);
        var selectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession());
        var customerCard = await accountService.GetCustomerCardFromUser(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1"));
        
        var viewModel = new PickYourAnimalViewModel
        {
            Date = date, 
            Animals = animals,
            SelectedAnimals = selectedAnimals,
            CustomerCard = customerCard,
            Subtotal = DiscountRules.CalculateSubTotalPrice(selectedAnimals)
        };
        return View(viewModel);
    }

    [HttpGet("customer-details")]
    public async Task<IActionResult> CustomerDetails()
    {
        var selectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession());
        if (selectedAnimals.Count == 0)
        {
            TempData["Alert"] = "Selecteer een dier";
            TempData["AlertDescription"] = "Je moet minimaal één dier selecteren om verder te gaan.";
            return RedirectToAction("PickYourAnimal");
        }
        
        if (User.Identity is { IsAuthenticated: true })
        {
            bool saved = SaveCustomerDetailsToSession(null, 
                await accountService.GetAccount(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1")));
            if (saved) return RedirectToAction("BookingOverview");
            
            TempData["Alert"] = "Er is iets misgegaan";
            TempData["AlertDescription"] = "Er is iets misgegaan bij het ophalen van jouw gegevens. Probeer het opnieuw.";
            return RedirectToAction("Index", "Home");
        }
        
        var viewModel = new CustomerDetailsViewModel
        {
            Date = GetValidDateFromSession(),
            SelectedAnimals = selectedAnimals,
            Subtotal = DiscountRules.CalculateSubTotalPrice(selectedAnimals),
            
            FirstName = string.Empty,
            Infix = string.Empty,
            LastName = string.Empty,
            Address = string.Empty,
            City = string.Empty,
            Email = string.Empty,
            PhoneNumber = string.Empty
        };
        return View(viewModel);
    }

    [HttpGet("booking-overview")]
    public async Task<IActionResult> BookingOverview()
    {
        // validate session data
        var date = GetValidDateFromSession();
        var selectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession());
        if (selectedAnimals.Count == 0)
        {
            TempData["Alert"] = "Selecteer een dier";
            TempData["AlertDescription"] = "Je moet minimaal één dier selecteren om verder te gaan.";
            return RedirectToAction("PickYourAnimal");
        }
        var customerFromSession = GetCustomerDetailsFromSession();
        if (customerFromSession == null)
        {
            TempData["Alert"] = "Vul je gegevens in";
            TempData["AlertDescription"] = "Je moet je gegevens invullen om verder te gaan.";
            return RedirectToAction("CustomerDetails");
        }
        
        // get customer data
        var customer = new CustomerDto
        {
            FullName = customerFromSession["name"],
            Address = customerFromSession["address"],
            Email = string.IsNullOrEmpty(customerFromSession["email"]) ? null : customerFromSession["email"],
            PhoneNumber = string.IsNullOrEmpty(customerFromSession["phone"]) ? null : customerFromSession["phone"]
        };
        var customerCardType = await accountService.GetCustomerCardFromUser(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1"));
        
        // get price and discounts
        var subTotalPrice = DiscountRules.CalculateSubTotalPrice(selectedAnimals);
        var discounts = DiscountRules.GetDiscounts(selectedAnimals, date, customerCardType);
        var totalPrice = DiscountRules.CalculateTotalPrice(subTotalPrice, discounts);
        
        // show if user is logged in
        ViewData["LoggedIn"] = User.Identity is { IsAuthenticated: true };
        
        // show view
        var viewModel = new BookingOverviewViewModel
        {
            Date = GetValidDateFromSession(),
            SelectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession()),
            Customer = customer,
            Discounts = discounts,
            SubTotalPrice = subTotalPrice,
            TotalPrice = totalPrice
        };
        return View(viewModel);
    }
    
    // == POST methods == //
    [HttpPost("start-booking")]
    public IActionResult StartBooking(DateOnly? date)
    {
        HttpContext.Session.Clear();
        
        if (date == null || date < DateOnly.FromDateTime(DateTime.Now))
        {
            TempData["Alert"] = "Selecteer een datum";
            TempData["AlertDescription"] = "Je moet een datum in de toekomst selecteren om een boeking te starten.";
            return RedirectToAction("Index", "Home");
        }
        
        HttpContext.Session.SetString("BookingDate", date.ToString()!);
        return RedirectToAction("PickYourAnimal");
    }

    [HttpPost("save-selected-animals")]
    public async Task<IActionResult> SaveSelectedAnimals(List<int> selectedAnimalIds)
    {
        // validate form data
        if (!ModelState.IsValid)
        {
            TempData["Alert"] = "Er is iets misgegaan";
            TempData["AlertDescription"] = "Er is iets misgegaan bij het selecteren van de dieren. Probeer het opnieuw.";
            return RedirectToAction("PickYourAnimal");
        }
        
        // validate selection
        int animalToAddId = selectedAnimalIds.Find(id => !GetAnimalIdsFromSession().Contains(id));
        DateOnly bookingDate = GetValidDateFromSession();
        var customerCard = await accountService.GetCustomerCardFromUser(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1"));
        
        if (animalToAddId == 0)
        {
            HttpContext.Session.SetString("SelectedAnimalIds", JsonSerializer.Serialize(selectedAnimalIds));
            return RedirectToAction("PickYourAnimal");
        }

        bool result = await ValidateAnimalSelection(animalToAddId, selectedAnimalIds, bookingDate, customerCard);
        if (result)
        {
            HttpContext.Session.SetString("SelectedAnimalIds", JsonSerializer.Serialize(selectedAnimalIds));
        }
        return RedirectToAction("PickYourAnimal");
    }

    [HttpPost("save-customer-details")]
    public async Task<IActionResult> SaveCustomerDetails(CustomerDetailsViewModel model)
    {
        model.SelectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession());
        if (!ModelState.IsValid) return View("CustomerDetails", model);
        
        bool saved = SaveCustomerDetailsToSession(model, null);
        if (saved) 
            return RedirectToAction("BookingOverview");
        
        TempData["Alert"] = "Er is iets misgegaan";
        TempData["AlertDescription"] = "Er is iets misgegaan bij het opslaan van de (contact)gegevens. Probeer het opnieuw.";
        return View("CustomerDetails", model);
    }

    [HttpPost("confirm-booking")]
    public async Task<IActionResult> ConfirmBooking()
    {
        return RedirectToAction("Index", "Home");
    }
    
    // == Private methods == //
    private async Task<bool> ValidateAnimalSelection(int animalToAddId, List<int> selectedAnimalIds, DateOnly date, CustomerCardType? customerCard)
    {
        // Get data from services
        var animalToAdd = await animalService.GetAnimal(animalToAddId);
        var selectedAnimals = await animalService.GetAnimalsByIds(selectedAnimalIds);

        // Validate selection
        string errorMessage = string.Empty;
        var isValid = animalToAdd != null && BookingRules.Validate(animalToAdd, selectedAnimals, date, customerCard, out errorMessage);
        if (!isValid)
        {
            TempData["Alert"] = "Ongeldige selectie";
            TempData["AlertDescription"] = errorMessage;
        }
        return isValid;
    }

    private bool SaveCustomerDetailsToSession(CustomerDetailsViewModel? model, AccountDto? account)
    {
        if (model == null && account == null) return false;

        var customerDetails = new Dictionary<string, string>()
        {
            { "name", string.Empty },
            { "address", string.Empty },
            { "email", string.Empty  },
            { "phone", string.Empty  }
        };
        
        if (model != null && account == null)
        {
            customerDetails["name"] = model.Infix != null ? model.FirstName + " " + model.Infix + " " + model.LastName : model.FirstName + " " + model.LastName;
            customerDetails["address"] = model.Address + " " + model.City;
            customerDetails["email"] = model.Email ?? string.Empty;
            customerDetails["phone"] = model.PhoneNumber ?? string.Empty;
        }

        if (model == null && account != null)
        {
            customerDetails["name"] = account.FullName;
            customerDetails["address"] = account.Address;
            customerDetails["email"] = account.Email ?? string.Empty;
            customerDetails["phone"] = account.PhoneNumber ?? string.Empty;
        }
        
        if (customerDetails.Values.All(value => value == string.Empty)) return false;
        HttpContext.Session.SetString("CustomerDetails", JsonSerializer.Serialize(customerDetails));
        return true;
    }

    private DateOnly GetValidDateFromSession()
    {
        var date = HttpContext.Session.GetString("BookingDate");
        if (date != null && DateTime.Parse(date).Date > DateTime.Now.Date) 
            return DateOnly.Parse(date);
        
        TempData["Alert"] = "Selecteer een datum";
        TempData["AlertDescription"] = "Je moet een datum in de toekomst selecteren om een boeking te starten.";
        Response.Redirect("/");
        return DateOnly.FromDateTime(DateTime.Now);
    }
    
    private List<int> GetAnimalIdsFromSession()
    {
        var sessionSavedAnimals = HttpContext.Session.GetString("SelectedAnimalIds");
        return (sessionSavedAnimals != null ? JsonSerializer.Deserialize<List<int>>(sessionSavedAnimals) : []) ?? new List<int>();
    }
    
    private Dictionary<string, string>? GetCustomerDetailsFromSession()
    {
        var sessionSavedCustomerDetails = HttpContext.Session.GetString("CustomerDetails");
        return sessionSavedCustomerDetails != null ? JsonSerializer.Deserialize<Dictionary<string, string>>(sessionSavedCustomerDetails) : new Dictionary<string, string>();
    }
}