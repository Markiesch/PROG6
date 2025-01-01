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
        var viewModel = new PickYourAnimalViewModel
        {
            Date = date, 
            Animals = animals,
            CustomerCard = null // TODO: Get customer card
        };
        return View(viewModel);
    }

    [HttpGet("customer-details")]
    public IActionResult CustomerDetails()
    {
        var viewModel = new CustomerDetailsViewModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            SelectedAnimals = []
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

    [HttpPost("validate-animal-selection")]
    public async Task<bool> ValidateAnimalSelection([FromBody] dynamic request)
    {
        var jsonElement = (JsonElement)request;
        var animalToAddIdFromJson = jsonElement.GetProperty("animalToAddId").GetInt32();
        var selectedAnimalIdsFromJson = jsonElement.GetProperty("selectedAnimalIds").EnumerateArray().Select(x => x.GetInt32()).ToList();
        var dateFromJson = jsonElement.GetProperty("date").GetString();
        var customerCardFromJson = jsonElement.GetProperty("customerCard").GetString();

        if (dateFromJson == null) return false;
        
        var animalToAdd = await animalService.GetAnimalById(animalToAddIdFromJson);
        var selectedAnimals = await animalService.GetAnimalsByIds(selectedAnimalIdsFromJson);
        var date = DateOnly.Parse(dateFromJson);
        var customerCard = customerCardFromJson != null ? Enum.Parse<CustomerCardType>(customerCardFromJson) : (CustomerCardType?)null;

        if (animalToAdd == null) return false;
        var validation = await BookingRules
            .Validate(animalToAdd, selectedAnimals, date, customerCard)
            .ReadFromJsonAsync<dynamic>();
        
        if (validation == null) return false;
        
        TempData["ValidationMessage"] = validation.reason;
        return validation.isValid;
    }

    [HttpPost("save-selected-animals")]
    public IActionResult SaveSelectedAnimals(DateOnly date, List<int> selectedAnimalIds)
    {
        // validate
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

        HttpContext.Session.SetString("BookingDate", date.ToString());
        HttpContext.Session.SetString("SelectedAnimalIds", JsonSerializer.Serialize(selectedAnimalIds));
        return RedirectToAction("CustomerDetails");
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
}