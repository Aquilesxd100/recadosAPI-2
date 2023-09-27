using System;

namespace recados_api
{
    public class CriarContaService
    {
        public void Service(UsuarioModelo modelo){
            new CriarContaValidator().Validator(modelo);

            new UsuarioRepository().CriarConta(modelo);
        }
    }
}