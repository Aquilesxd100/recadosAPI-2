namespace recados_api
{
    public class GetRecadosService{
        public RecadoModelo[] Service(string userId){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            return new RecadoRepository()
                .GetRecados(userId);
        }
    }
}