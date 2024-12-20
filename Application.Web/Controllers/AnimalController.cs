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
    public async Task<IActionResult> Index()
    {
        var animals = await animalService.GetAnimals();
        var model = new AnimalListViewModel
        {
            Animals = animals
        };
        
        return View(model);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
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
}