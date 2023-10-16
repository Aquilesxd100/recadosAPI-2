using System;

namespace recados_api
{
    public class CriarRecadoService
    {
        public void Service(string userId, RecadoModelo modelo){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            new RecadoValidator(modelo.Titulo, modelo.Descricao, modelo.Data, modelo.Horario)
                .CamposPreechidos()
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido()
                .FormatoHorario()
                .FormatoData()
                .DataFutura();
            
            var response = new RecadoModelo(){
                Titulo = modelo.Titulo,
                Descricao = modelo.Descricao,
                Data = modelo.Data,
                Horario = modelo.Horario,
                Arquivado = false,
                Id = Guid.NewGuid().ToString(),
                UsuarioId = userId
            };

            new RecadoRepository()
                .CriarRecado(response);

        }
    }
} 