using Microsoft.AspNetCore.Mvc;

namespace recados_api 
{
    public class CriaErroHTTP : ControllerBase {
        
        public IActionResult MandarResposta(ErroHTTP erro) 
        {
            switch (erro.StatusErro)
            {
                case 401:
                    return Unauthorized(erro.MostraErroJSON());
                case 400:
                    return BadRequest(erro.MostraErroJSON());
                case 404:
                    return NotFound(erro.MostraErroJSON());
                default:
                    return StatusCode(500, new ErroHTTP(500, "Erro interno inesperado.").MostraErroJSON());
            }
        }
    }
}