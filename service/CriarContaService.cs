using System;

namespace recados_api
{
    public class CriarContaService
    {
        public void ValidacaoUsuario(UsuarioModelo modelo){
            new CriarContaValidator().ValidatorNovaConta(modelo);

            new ContaRepository().CriarNovaConta(modelo);
        }
    }




}