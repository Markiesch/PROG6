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
    public async Task<JsonResult> ValidateAnimalSelection([FromBody] dynamic request)
    {
        // Parse request
        var jsonElement = (JsonElement)request;
        var animalToAddId = jsonElement.GetProperty("animalToAddId").GetInt32();
        var selectedAnimalIds = jsonElement.GetProperty("selectedAnimalIds").EnumerateArray().Select(x => x.GetInt32()).ToList();
        var dateString = jsonElement.GetProperty("date").GetString();
        var customerCardString = jsonElement.GetProperty("customerCard").GetString();

        if (dateString == null) 
            return Json(new { isValid = false, errorMessage = "Ongeldige datum" });

        // Get data from services
        var animalToAdd = await animalService.GetAnimal(animalToAddId);
        var selectedAnimals = await animalService.GetAnimalsByIds(selectedAnimalIds);
        var date = DateOnly.Parse(dateString);
        var customerCard = customerCardString != null ? Enum.Parse<CustomerCardType>(customerCardString) : (CustomerCardType?)null;

        // Validate selection
        string errorMessage = string.Empty;
        var isValid = animalToAdd != null && BookingRules.Validate(animalToAdd, selectedAnimals, date, customerCard, out errorMessage);

        return Json(new { isValid, errorMessage });
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