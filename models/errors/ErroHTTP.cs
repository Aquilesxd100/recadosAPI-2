using System;

namespace recados_api 
{
    public class ErroHTTP : Exception {
        public int StatusErro { get; set; }
        public string ErroMensagem { get; set; }

        public ErroHTTP(int StatusErro, string ErroMensagem)
        {
            this.StatusErro = StatusErro;
            this.ErroMensagem = ErroMensagem;
        }

        public ErroHTTPRespostaModelo MostraErroJSON() 
        {
            ErroHTTPRespostaModelo retorno = new ErroHTTPRespostaModelo(){
                CodigoErro  = StatusErro,
                Mensagem = ErroMensagem
            };
            return retorno;
        }
    }
}