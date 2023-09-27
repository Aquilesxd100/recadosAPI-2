using System;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    [ApiController]
    [Route("[controller]")]
    public class EntrarContaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromBody] UsuarioModelo modelo)
        {
            try{
                var response = new UsuarioModelo(
                    modelo.Username,
                    modelo.Senha,
                    Guid.NewGuid().ToString()
                );
                
                string result = new EntrarContaService().Service(modelo);

                return Ok(result);
                
            }catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}
