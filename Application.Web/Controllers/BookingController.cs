using System.Text.Json;
using Application.Data.Dto;
using Application.Data.Models;
using Application.Data.Services;
using Application.Web.Models;
using Application.Web.Rules;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;
[Route("/booking")]

public class BookingController(AnimalService animalService) : Controller
{
    [HttpGet("pick-your-animal/")]
    public async Task<IActionResult> PickYourAnimal()
    {
        var date = GetValidDateFromSession();
        var animals = await animalService.GetAnimalsWithAvailability(date);
        var selectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession());
        
        var viewModel = new PickYourAnimalViewModel
        {
            Date = date, 
            Animals = animals,
            SelectedAnimals = selectedAnimals,
            CustomerCard = null // TODO: Get customer card
        };
        return View(viewModel);
    }

    [HttpGet("customer-details")]
    public async Task<IActionResult> CustomerDetails()
    {
        var viewModel = new CustomerDetailsViewModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            SelectedAnimals = await animalService.GetAnimalsByIds(GetAnimalIdsFromSession())
        };
        return View(viewModel);
    }
    
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

        if (selectedAnimalIds.Count == 0)
        {
            TempData["Alert"] = "Selecteer een dier";
            TempData["AlertDescription"] = "Je moet minimaal één dier selecteren om verder te gaan.";
            return RedirectToAction("PickYourAnimal");
        }
        
        // validate selection
        int animalToAddId = selectedAnimalIds.Find(id => !GetAnimalIdsFromSession().Contains(id));
        DateOnly bookingDate = GetValidDateFromSession();
        CustomerCardType? customerCard = null; // TODO: Get customer card
        
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
}