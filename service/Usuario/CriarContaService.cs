using System;

namespace recados_api
{
    public class CriarContaService
    {
        public void Service(UsuarioBruto modeloBruto){

            UsuarioModelo modelo = new UsuarioModelo{
                Username = modeloBruto.Username?.ToString(),
                Senha = modeloBruto.Senha?.ToString()
            };

            new UsuarioValidator(modelo.Username, modelo.Senha)
                .PreencherCampos()
                .CamposType()
                .QntCaracteres()
                .UsernameValid()
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