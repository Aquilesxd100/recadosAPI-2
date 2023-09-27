using System;
using Microsoft.AspNetCore.Mvc;

namespace recados_api 
{
    public class ErroInterno : ControllerBase {
        private int StatusErro { get; set; }
        private string ErroMensagem { get; set; }

        public ErroInterno(int StatusErro, string ErroMensagem)
        {
            this.StatusErro = StatusErro;
            this.ErroMensagem = ErroMensagem;
        }
        
        public IActionResult MandarResposta() 
        {
            switch (this.StatusErro)
            {
                case 400:
                    return BadRequest(this.ErroMensagem);
                case 404:
                    return NotFound(this.ErroMensagem);
                default:
                    return NotFound();
            }
        }
    }
}