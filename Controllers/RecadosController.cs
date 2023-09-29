using System;
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

                 var response = new RecadoModelo(){
                    Titulo = modelo.Titulo,
                    Descricao = modelo.Descricao,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Horario = DateTime.Now.ToString("HH:mm:ss"),
                    Arquivado = false,
                    Id = Guid.NewGuid().ToString()
                };


                // new CriarRecadoService().Service(response);
                
                

                return Ok(new {
                    mensagem = "Recado criado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}