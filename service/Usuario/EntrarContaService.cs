using System;

namespace recados_api
{
    public class EntrarContaService
    {
        public string Service(UsuarioBruto modeloBruto){

            UsuarioModelo modelo = new UsuarioModelo {
                Username = modeloBruto.Username?.ToString(),
                Senha = modeloBruto.Senha?.ToString(),
            };

            new UsuarioValidator(modelo.Username, modelo.Senha)
                .PreencherCampos()
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido();

            var token = new UsuarioRepository()
                .EntrarConta(modelo);

            return token;
        }
    }

}