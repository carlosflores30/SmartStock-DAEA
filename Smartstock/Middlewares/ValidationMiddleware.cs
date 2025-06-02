using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Smartstock.Middlewares;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.Body.Position = 0;

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                await ReturnBadRequest(context, "El cuerpo de la solicitud no puede estar vacío.");
                return;
            }

            try
            {
                var json = JsonSerializer.Deserialize<Dictionary<string, object>>(body);

                if (json != null)
                {
                    // Validar email
                    if (json.TryGetValue("email", out var emailObj))
                    {
                        var email = emailObj?.ToString();
                        if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email!))
                        {
                            await ReturnBadRequest(context, "El correo electrónico no tiene un formato válido.");
                            return;
                        }
                    }

                    // Validar password
                    if (json.TryGetValue("password", out var passwordObj))
                    {
                        var password = passwordObj?.ToString();
                        if (string.IsNullOrWhiteSpace(password) || password!.Length < 8)
                        {
                            await ReturnBadRequest(context, "La contraseña debe tener al menos 8 caracteres.");
                            return;
                        }
                    }
                }
            }
            catch
            {
                await ReturnBadRequest(context, "El cuerpo JSON no tiene un formato válido.");
                return;
            }
        }

        await _next(context);
    }

    private bool IsValidEmail(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    private async Task ReturnBadRequest(HttpContext context, string message)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        var response = JsonSerializer.Serialize(new { error = message });
        await context.Response.WriteAsync(response);
    }
}