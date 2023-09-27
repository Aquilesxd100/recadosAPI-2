using System;

namespace recados_api
{
    public class CriarContaValidator
    {
        
        public ErroInterno ValidatorNovaConta(UsuarioModelo novaConta){
            if(novaConta.Username == null || novaConta.Senha == null){
                return new ErroInterno(400, "Preencha todos os campos.");
            };

            if(novaConta.Username.Length < 5){
                return new ErroInterno(400, "O Username deve ter no minimo 5 caracteres.");
            };
            
            if(novaConta.Senha.Length > 20 || novaConta.Senha.Length < 8) {
                return new ErroInterno(400, "A senha deve ter no minimo 8 caracteres ou no maximo 20.");
            };

            return null;
        }
    }
}