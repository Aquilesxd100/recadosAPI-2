using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;

//Caminho da rota é o nome da Classe e suas chamadas interiores sem o "Controller"
namespace recados_api
{
    [ApiController]
    [Route("[controller]")]
    public class CriarContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CriarConta modelo)
        {
            var response = new CriarConta()
            {
                Username = modelo.Username,
                Senha = modelo.Senha,
                Id = Guid.NewGuid().ToString()
            };

            ValidatorNovaContaReturn validacao = new CriarContaValidator().ValidatorNovaConta(response);

            string connStr = "server=db4free.net;user=javadevstests;database=lembretesjava6;port=3306;password=f262e259";
            MySqlConnection conn = new MySqlConnection(connStr);
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = $"INSERT INTO Usuario VALUES ('{response.Username}', '{response.Senha}', '{response.Id}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();

            }catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            return Ok(response);
        }
    }
}
