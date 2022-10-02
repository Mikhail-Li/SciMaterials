using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciMaterials.API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _Next;
    private readonly ILogger<ExceptionHandlerMiddleware> _Logger;

    public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> Logger)
    {
        _Next        = Next;
        _Logger = Logger;
    }

    public async Task Invoke(HttpContext Context)
    {
        try
        {
            await _Next(Context);
        }
        catch (Exception e)
        {
            await HandleErrorAsync(Context, e);
        }
    }

    private async Task HandleErrorAsync(HttpContext Context, Exception error)
    {
        _Logger.LogError(error, "qweqweqwe");

        Context.Response.Clear();
        await Context.Response.WriteAsJsonAsync(
            new
            {
                Code    = 132,
                Message = "Error!",
                Details = error.Message,
            });
    }
}
