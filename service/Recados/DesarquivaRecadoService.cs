namespace recados_api
{
    public class DesarquivaRecadoService{
        public void Service(string userId, string recadoId){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator()
                .PertenceAUsuarioId(userId, recadoId);

            new RecadoRepository()
                .DesarquivaRecado(recadoId);
            
            Database.conexao.Close();
        }
    }
}