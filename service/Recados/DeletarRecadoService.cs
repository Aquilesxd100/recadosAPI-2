namespace recados_api
{
    public class DeletarRecadoService{
        public void Service(string userId, string recadoId){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            new RecadoValidator()
                .PertenceAUsuarioId(userId, recadoId);

            new RecadoRepository()
                .DeletarRecado(recadoId);

            Database.conexao.Close();
        }
    }
}