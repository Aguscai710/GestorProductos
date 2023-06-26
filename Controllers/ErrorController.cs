using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    public IActionResult AccessDenied()
    {
        var errorMessage = TempData["ErrorMessage"] as string;
        errorMessage = "El usuario no tiene permisos adecuados para esta acción.";
        ViewBag.ErrorMessage = errorMessage;
        return View();
    }
}
