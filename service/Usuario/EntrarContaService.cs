using System;

namespace recados_api
{
    public class EntrarContaService
    {
        public string Service(UsuarioModelo modelo){
            new UsuarioValidator(modelo.Username, modelo.Senha)
                .PreencherCampos()
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido();

            var token = new UsuarioRepository()
                .EntrarConta(modelo);

            Database.conexao.Close();

            return token;
        }
    }

}