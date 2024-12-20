using System.Text.Json;
using Application.Data.Models;
using Application.Data.Services;
using Application.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

public class BookingController(AnimalService animalService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> PickYourAnimal(DateOnly? date)
    {
        // TODO: clear session
        
        if (date == null || date < DateOnly.FromDateTime(DateTime.Now))
        {
            TempData["Alert"] = "Selecteer een datum";
            TempData["AlertDescription"] = "Je moet een datum in de toekomst selecteren om een boeking te starten.";
            return RedirectToAction("Index", "Home");
        }

        var animals = await animalService.GetAnimalsWithAvailability((DateOnly)date);
        
        // TODO: get unavailable animals from database
        var unavailableAnimals = new List<Animal>
        {
       
        };

        // TODO: hier al validatie inbouwen voor x aantal kiezen en vip wel/niet?
        // TODO: rename models naar zelfde als viewnamen
        var viewModel = new BookingStep2ViewModel
        {
            Date = (DateOnly)date,
            Animals = animals,
            UnavailableAnimals = unavailableAnimals
        };
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult CustomerDetails()
    {
        var date = HttpContext.Session.GetString("BookingDate");
        var selectedAnimalIds = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("SelectedAnimalIds") ?? string.Empty);
        var selectedAnimals = new List<Animal>(); // TODO: get animals from database by id
        
        if (date == null)
        {
            TempData["Alert"] = "Selecteer een datum";
            TempData["AlertDescription"] = "Je moet een datum selecteren om verder te gaan.";
            return RedirectToAction("Index", "Home");
        }
        if (selectedAnimalIds == null || selectedAnimalIds.Count == 0)
        {
            TempData["Alert"] = "Selecteer een dier";
            TempData["AlertDescription"] = "Je moet minimaal één dier selecteren om verder te gaan.";
            return RedirectToAction("PickYourAnimal", new { date });
        }

        var viewModel = new BookingStep3ViewModel()
        {
            Date = DateOnly.Parse(date),
            SelectedAnimals = selectedAnimals
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SaveSelectedAnimals(DateOnly date, List<int> selectedAnimalIds)
    {
        HttpContext.Session.SetString("BookingDate", date.ToString());
        HttpContext.Session.SetString("SelectedAnimalIds", JsonSerializer.Serialize(selectedAnimalIds));
        return RedirectToAction("CustomerDetails");
    }
}