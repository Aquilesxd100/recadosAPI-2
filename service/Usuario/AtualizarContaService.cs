namespace recados_api
{
    public class AtualizarContaService
    {
        public void Service(string userId, UsuarioModelo modelo){
            new UsuarioValidator(modelo.Username, modelo.Senha)
                .ValidUserToken(userId)
                .PreencherCampoAtualizar()
                .CampoTypeAtualizar()
                .CaracterInvalido()
                .QntCaracteres()
                .SenhaValid()
                .UsernameJaExiste();

            new UsuarioRepository()
                .AtualizarConta(userId, modelo);
            
            Database.conexao.Close();
        }
    }
}