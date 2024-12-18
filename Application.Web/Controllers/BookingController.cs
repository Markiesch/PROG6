using Application.Data;
using Application.Data.Models;
using Application.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

public class BookingController : Controller
{
    [HttpGet]
    public IActionResult PickYourAnimal(DateOnly? date)
    {
        // TODO: hier al validatie inbouwen voor x aantal kiezen en vip wel/niet?
        // TODO: clear session data on start of booking
        
        if (date == null)
        {
            TempData["Alert"] = "Selecteer een datum";
            TempData["AlertDescription"] = "Je moet een datum selecteren om een boeking te starten.";
            return RedirectToAction("Index", "Home");
        }

        var animal3 = new Animal
                { Id = 3, Name = "Zebra", Type = AnimalType.Jungle, Price = 50, Image = "/img/animals/zebra.png" };
        var animal4 = new Animal
            { Id = 4, Name = "Leeuw", Type = AnimalType.Jungle, Price = 150, Image = "/img/animals/lion.png" };
        
        // TODO: get animals from database
        var animals = new List<Animal>
        {
            new Animal { Id = 1, Name = "Aap", Type = AnimalType.Jungle, Price = 25, Image = "/img/animals/monkey.png" },
            new Animal { Id = 2, Name = "Olifant", Type = AnimalType.Jungle, Price = 100, Image = "/img/animals/elephant.png" },
            animal3,
            animal4,
            new Animal { Id = 5, Name = "Hond", Type = AnimalType.Farm, Price = 20, Image = "/img/animals/dog.png" },
            new Animal { Id = 6, Name = "Ezel", Type = AnimalType.Farm, Price = 30, Image = "/img/animals/donkey.png" },
            new Animal { Id = 7, Name = "Koe", Type = AnimalType.Farm, Price = 50, Image = "/img/animals/cow.png" },
            new Animal { Id = 8, Name = "Eend", Type = AnimalType.Farm, Price = 15, Image = "/img/animals/duck.png" },
            new Animal { Id = 9, Name = "Kuiken", Type = AnimalType.Farm, Price = 10, Image = "/img/animals/chicken.png" },
            new Animal { Id = 10, Name = "Pinguïn", Type = AnimalType.Snow, Price = 80, Image = "/img/animals/penguin.png" },
            new Animal { Id = 11, Name = "IJsbeer", Type = AnimalType.Snow, Price = 200, Image = "/img/animals/polar-bear.png" },
            new Animal { Id = 12, Name = "Zeehond", Type = AnimalType.Snow, Price = 60, Image = "/img/animals/seal.png" },
            new Animal { Id = 13, Name = "Kameel", Type = AnimalType.Desert, Price = 70, Image = "/img/animals/camel.png" },
            new Animal { Id = 14, Name = "Slang", Type = AnimalType.Desert, Price = 40, Image = "/img/animals/snake.png" },
            new Animal { Id = 15, Name = "T-Rex", Type = AnimalType.Vip, Price = 500, Image = "/img/animals/t-rex.png" },
            new Animal { Id = 16, Name = "Unicorn", Type = AnimalType.Vip, Price = 1000, Image = "/img/animals/unicorn.png" }
        };
        
        // TODO: get unavailable animals from database
        var unavailableAnimals = new List<Animal>
        {
            animal3,
            animal4
        };

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
        return View();
    }

    [HttpPost]
    public IActionResult SaveSelectedAnimals(DateOnly date, List<int> selectedAnimalIds)
    {
        // TODO: save selected animals in session
        
        return RedirectToAction("CustomerDetails");
    }
}