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
        [Authorize]
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

        [Route("/DeletarRecado")]
        [HttpDelete]
        [Authorize]
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
        [Route("/AtualizarRecado")]
        [HttpPut]
        [Authorize]
        public IActionResult AtualizarRecado([FromQuery] string recadoId, [FromBody] RecadoModelo modelo)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new AtualizarRecadoService().Service(userId, recadoId, modelo);
                
                return Ok(new {
                    mensagem = "Recado atualizado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/Recados")]
        [HttpGet]
        [Authorize]
        public IActionResult GetRecados()
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                RecadoModelo[] recados = new GetRecadosService().Service(userId);
                
                return Ok(new {
                    mensagem = "Seus recados!",
                    Data = recados
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}