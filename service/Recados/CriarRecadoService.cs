using System;

namespace recados_api
{
    public class CriarRecadoService
    {
        public void Service(string userId, RecadoModelo modelo){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            new RecadoValidator(modelo)
                .CamposPreechidos()
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido()
                .FormatoData()
                .FormatoHorario();
            
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