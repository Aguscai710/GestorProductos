using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PNT1_Grupo6.Models;

public class UsuarioActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var usuario = context.HttpContext.Session.GetObject<Usuario>("Usuario");
        context.HttpContext.Items["Usuario"] = usuario;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No se requiere ninguna acción después de la ejecución de la acción
    }
}