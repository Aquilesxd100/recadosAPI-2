namespace recados_api
{
    public class AtualizaStatusArquivadoRecadoService{
        public void Service(string userId, string recadoId, string statusArquivado){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator()
                .IdRecadoEhValido(recadoId)
                .PertenceAUsuarioId(userId, recadoId, statusArquivado);

            new RecadoRepository()
                .AtualizaStatusArquivadoRecado(recadoId, statusArquivado == "true");

        }
    }
}