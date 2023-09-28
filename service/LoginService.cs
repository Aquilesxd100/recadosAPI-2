using System;

namespace recados_api
{
    public class LoginService
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