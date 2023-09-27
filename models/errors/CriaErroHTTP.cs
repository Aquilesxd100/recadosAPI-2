using System;
using Microsoft.AspNetCore.Mvc;

namespace recados_api 
{
    public class CriaErroHTTP : ControllerBase {
        
        public IActionResult MandarResposta(ErroHTTP erro) 
        {
            switch (erro.StatusErro)
            {
                case 400:
                    return BadRequest(erro.MostraErroJSON());
                case 404:
                    return NotFound(erro.MostraErroJSON());
                default:
                    return NotFound();
            }
        }
    }
}