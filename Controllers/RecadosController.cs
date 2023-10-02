using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    [ApiController]
    public class RecadosController : ControllerBase
    {
        [Route("/CriarRecado")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CriarRecado([FromBody] RecadoModelo modelo)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new CriarRecadoService().Service(userId, modelo);
                
                return Ok(new {
                    mensagem = "Recado criado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
        public IActionResult DeletarRecado([FromQuery] string recadoId)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new DeletarRecadoService().Service(userId, recadoId);
                
                return Ok(new {
                    mensagem = "Recado deletado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}