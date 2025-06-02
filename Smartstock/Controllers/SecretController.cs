using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Smartstock.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // ← Esta es la clave
public class SecretController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult GetSecret()
    {
        var username = User.Identity?.Name;
        return Ok(new { message = $"Hola {username}, accediste a una ruta protegida." });
    }
}