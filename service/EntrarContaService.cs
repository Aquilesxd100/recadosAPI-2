namespace recados_api
{
    public class EntrarContaService
    {
        public string Service(UsuarioModelo modelo){
            new EntrarContaValidator().Validator(modelo);
            return new UsuarioRepository().EntrarConta(modelo);
        }
    }

}