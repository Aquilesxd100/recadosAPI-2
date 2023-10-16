using System;

namespace recados_api
{
    public class CriarRecadoService
    {
        public void Service(string userId, RecadoBruto modeloBruto){
            new UsuarioValidator()
                .ValidUserToken(userId);
                
            RecadoModelo modelo = new RecadoModelo {
                Titulo = modeloBruto.Titulo?.ToString(),
                Descricao = modeloBruto.Descricao?.ToString(),
                Data = modeloBruto.Data?.ToString(),
                Horario = modeloBruto.Horario?.ToString(),
            };
            
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