using System;

namespace recados_api
{
    public class CriarContaValidator
    {
        
        public ErroHTTP Validator(UsuarioModelo novaConta){
            if(novaConta == null || novaConta.Username == null || novaConta.Senha == null){
                throw new ErroHTTP(400, "Preencha todos os campos.");
            };

            if(novaConta.Username.Length < 5 || novaConta.Username.Length > 20){
                throw new ErroHTTP(400, "O Username deve ter no minimo 5 caracteres e no maximo 20.");
            };
            
            if(novaConta.Senha.Length > 20 || novaConta.Senha.Length < 8) {
                throw new ErroHTTP(400, "A senha deve ter no minimo 8 caracteres e no maximo 20.");
            };

            return null;
        }
    }
}