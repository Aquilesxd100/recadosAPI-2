namespace recados_api
{
    public class DeletarContaService
    {
        public void Service(string userId) {
            new UsuarioValidator()
                .ValidUserToken(userId);

            new UsuarioRepository()
                .DeletarConta(userId);

            Database.conexao.Close();
        }
    }
}