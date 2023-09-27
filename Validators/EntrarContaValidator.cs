namespace recados_api
{
    public class EntrarContaValidator
    {
        
        public ErroHTTP Validator(UsuarioModelo novaConta){
            if(novaConta.Username == null || novaConta.Senha == null){
                throw new ErroHTTP(400, "Preencha todos os campos. (username e senha)");
            };
            return null;
        }
    }
}