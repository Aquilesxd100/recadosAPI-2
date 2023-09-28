namespace recados_api
{
    public class EntrarContaService
    {
        public string Service(UsuarioModelo modelo){
            new UsuarioValidator(modelo.Username, modelo.Senha)
                .CampoPreechido()
                .CampoType()
                .QntCaracteres()
                .CaracterInvalido();
            return new UsuarioRepository().EntrarConta(modelo);
        }
    }

}