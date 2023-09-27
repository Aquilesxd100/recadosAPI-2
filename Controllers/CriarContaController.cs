using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

//Caminho da rota é o nome da Classe e suas chamadas interiores sem o "Controller"
namespace recados_api
{
    [ApiController]
    [Route("[controller]")]
    public class CriarContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioModelo modelo)
        {
            try{
                var response = new UsuarioModelo(
                    modelo.Username,
                    modelo.Senha,
                    Guid.NewGuid().ToString()
                );

            new CriarContaService().ValidacaoUsuario(response);
          
            return Ok(response); 

            } catch (System.Exception erro) {
                Console.WriteLine(erro);
                return new ErroInterno(400, "Deu erro").MandarResposta();
            };

        }
    }
}
