using Application.Data.Models;
using Application.Data.Services;
using Application.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

[Route("animals")]
[Authorize(Roles = "Admin")]
public class AnimalController(AnimalService animalService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? query)
    {
        var animals = await animalService.GetAnimals(query);
        var model = new AnimalListViewModel
        {
            Animals = animals
        };
        
        return View(model);
    }
    
    [HttpGet("{id:int}/details")]
    public async Task<IActionResult> Details(int id, string? query)
    {
        var bookings = await animalService.GetBookingsOfAnimal(id, query);
        if (bookings == null)
        {
            return NotFound();
        }
        
        var model = new AnimalDetailsViewModel
        {
            Bookings = bookings 
        };
        
        return View(model);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new AnimalUpdateViewModel
        {
            Id = -1,
            Name = string.Empty,
            Type = AnimalType.Vip,
            Price = 50,
            Image = null,
        };
        return View(model);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(AnimalUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await animalService.CreateAnimal(model.ToDto());
        
        return RedirectToAction("Index");
    }
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var animal = await animalService.GetAnimal(id);
        
        if (animal == null)
        {
            return NotFound();
        }
        
        var model = new AnimalUpdateViewModel
        {
            Id = animal.Id,
            Name = animal.Name,
            Type = animal.Type,
            Price = animal.Price,
            Image = animal.Image
        };
        
        return View(model);
    }
    
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Update(int id, AnimalUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var result = await animalService.UpdateAnimal(id, model.ToDto());
        
        if (!result)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost("{id:int}/delete")]
    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine(id);
        var result = await animalService.DeleteAnimal(id);
        
        if (!result)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
}