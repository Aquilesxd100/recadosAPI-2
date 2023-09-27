using System;

namespace recados_api
{
    public class CriarContaValidator
    {
        private ValidatorNovaContaReturn validation;
        public ValidatorNovaContaReturn ValidatorNovaConta(CriarConta novaConta){
            if(novaConta.Username == null || novaConta.Senha == null){
                validation = {
                    Status = false;
                    Message = "Preencha todos os campos.";
                };
                return validation;
            };
            
            if(novaConta.Senha.Length > 20 || novaConta.Senha.Length < 8) {
                validation = {
                    Status = false; 
                    Message = "Senha inválida.";
                };
                return validation;
            };
        }
    }
}