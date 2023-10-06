using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using recados_api;

public class AbreConexaoMiddleware : IMiddleware
{

 
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Database.conexao.Open();
        await next(context);
    }
}