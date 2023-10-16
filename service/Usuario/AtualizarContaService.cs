namespace recados_api
{
    public class AtualizarContaService
    {
        public void Service(string userId, UsuarioBruto modeloBruto){

            UsuarioModelo user = UsuarioRepository.EncontrarUsuarioById(userId);

            UsuarioModelo modelo = new UsuarioModelo{
                Username = modeloBruto.Username?.ToString() ?? user.Username,
                Senha = modeloBruto.Senha?.ToString() ?? user.Senha
            };

            new UsuarioValidator(modeloBruto.Username?.ToString(), modeloBruto.Senha?.ToString())
                .ValidUserToken(userId)
                .PreencherCampoAtualizar()
                .CampoTypeAtualizar()
                .CaracterInvalido()
                .QntCaracteres()
                .UsernameValid()
                .SenhaValid()
                .UsernameJaExiste();

            new UsuarioRepository()
                .AtualizarConta(userId, modelo);
            
        }
    }
}