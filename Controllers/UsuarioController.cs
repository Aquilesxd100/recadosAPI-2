using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [Route("/CriarConta")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CriarConta([FromBody] UsuarioModelo modelo)
        {
            try {
                var response = new UsuarioModelo(){
                    Username = modelo.Username,
                    Senha = modelo.Senha,
                    Id = Guid.NewGuid().ToString()
                };

                new CriarContaService().Service(response);
          
                return Ok(new {
                    mensagem = "Conta criada com sucesso!"
                }); 

            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }

        }

        [Route("/EntrarConta")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EntrarConta([FromBody] UsuarioModelo modelo)
        {
            try{
                string result = new EntrarContaService().Service(modelo);

                return Ok(new {
                    mensagem = "Login efetuado com sucesso!",
                    token = $"Bearer {result}"
                });
                
            }catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/DeletarConta")]
        [Authorize]
        [HttpDelete]
        public IActionResult DeletarConta([FromBody] UsuarioModelo modelo)
        {
            try{
                return Ok();
                
            }catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}
