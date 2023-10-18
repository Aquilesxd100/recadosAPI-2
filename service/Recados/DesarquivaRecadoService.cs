namespace recados_api
{
    public class DesarquivaRecadoService{
        public void Service(string userId, string recadoId){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator()
                .IdRecadoEhValido(recadoId)
                .PertenceAUsuarioId(userId, recadoId, true);

            new RecadoRepository()
                .DesarquivaRecado(recadoId);
            
        }
    }
}