using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace recados_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           

            CreateHostBuilder(args).Build().Run();

            // Database.conexao.Close();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Database.AbrirConexao();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
