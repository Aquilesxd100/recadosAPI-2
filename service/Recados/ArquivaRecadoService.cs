namespace recados_api
{
    public class ArquivarRecadoService{
        public void Service(string userId, string recadoId){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator()
                .IdRecadoEhValido(recadoId)
                .PertenceAUsuarioId(userId, recadoId, false);

            new RecadoRepository()
                .ArquivaRecado(recadoId);

        }
    }
}