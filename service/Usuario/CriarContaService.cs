using System;

namespace recados_api
{
    public class CriarContaService
    {
        public void Service(UsuarioModelo modelo){
            new UsuarioValidator(modelo.Username, modelo.Senha)
                .PreencherCampos()
                .CamposType()
                .QntCaracteres()
                .SenhaValid()
                .CaracterInvalido()
                .UsernameJaExiste();

            var response = new UsuarioModelo(){
                Username = modelo.Username,
                Senha = modelo.Senha,
                Id = Guid.NewGuid().ToString()
            };

            new UsuarioRepository()
                .CriarConta(response);
            
        }
    }
}