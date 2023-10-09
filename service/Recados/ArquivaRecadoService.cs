namespace recados_api
{
    public class ArquivarRecadoService{
        public void Service(string userId, string recadoId){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator()
                .PertenceAUsuarioId(userId, recadoId);

            new RecadoRepository()
                .ArquivaRecado(recadoId);

        }
    }
}