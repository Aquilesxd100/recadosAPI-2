using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    [ApiController]
    public class EntrarContaController : ControllerBase
    {
        [Route("/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UsuarioModelo modelo)
        {
            try{
                string result = new LoginService().Service(modelo);

                return Ok(new {
                    mensagem = "Login efetuado com sucesso!",
                    token = $"Bearer {result}"
                });
                
            }catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/delete")]
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody] UsuarioModelo modelo)
        {
            try{
                var token = GetUserId();
                return Ok();
                
            }catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
        private string GetUserId()
        {
            return User.Claims.First(i => i.Type == "Id").Value;
        }
    }
}
