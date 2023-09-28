using System;

namespace recados_api
{
    public class CriarContaService
    {
        public void Service(UsuarioModelo modelo){
            new UsuarioValidator(modelo.Username, modelo.Senha)
                .CampoPreechido()
                .CampoType()
                .QntCaracteres()
                .SenhaValid()
                .CaracterInvalido()
                .UsernameJaExiste();

            new UsuarioRepository().CriarConta(modelo);
        }
    }
}