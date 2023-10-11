using System.Collections.Generic;

namespace recados_api
{
    public class GetRecadosService{
        public List<RecadoModeloGet> Service(string userId){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            List<RecadoModeloGet> recados = new RecadoRepository()
                .GetRecados(userId);


            return recados;
            
        }
    }
}