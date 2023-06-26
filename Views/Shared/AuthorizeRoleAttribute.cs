using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PNT1_Grupo6.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly Rol _role;

    public AuthorizeRoleAttribute(Rol role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Session.GetObject<Usuario>("Usuario");
        if (user == null || !IsAuthorized(user.RolUsuario.ToString()))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Error", null);
            context.HttpContext.Session.SetString("ErrorMessage", "No tienes acceso a esta sección.");
        }
    }

    private bool IsAuthorized(string userRole)
    {
        // Comparar el rol del usuario con el rol especificado
        if (userRole.Equals(_role.ToString()) || userRole.Equals(Rol.Admin.ToString()))
        {
            return true;
        }
        return false;
    }
}
