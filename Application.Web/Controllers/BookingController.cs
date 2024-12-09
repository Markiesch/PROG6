using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

public class BookingController : Controller
{
    public IActionResult PickYourAnimal(DateOnly? date)
    {
        if (date == null)
        {
            TempData["Alert"] = "Selecteer een datum";
            TempData["AlertDescription"] = "Je moet een datum selecteren om een boeking te starten.";
            return RedirectToAction("Index", "Home");
        }
        return View((DateOnly)date);
    }
}