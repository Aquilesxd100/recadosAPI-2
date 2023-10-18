namespace recados_api
{
    public class AtualizarRecadoService{
        public void Service(string userId, string recadoId, RecadoBruto recadoBruto){
            new UsuarioValidator()
                .ValidUserToken(userId);

            RecadoModelo recado = RecadoRepository.EncontrarRecadoById(recadoId);

            RecadoModelo recadoAtualizado = new RecadoModelo{
                Titulo = recadoBruto.Titulo?.ToString() ?? recado.Titulo,
                Descricao = recadoBruto.Descricao?.ToString() ?? recado.Descricao,
                Data = recadoBruto.Data?.ToString() ?? recado.Data,
                Horario = recadoBruto.Horario?.ToString() ?? recado.Horario,
            };

            new RecadoValidator(
                recadoBruto.Titulo?.ToString(), 
                recadoBruto.Descricao?.ToString(), 
                recadoBruto.Data?.ToString() ?? recadoAtualizado.Data, 
                recadoBruto.Horario?.ToString() ?? recadoAtualizado.Horario
            )
                .IdRecadoEhValido(recadoId)
                .CamposPreechidosAtualiza(recadoBruto.Data?.ToString(), recadoBruto.Horario?.ToString())
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido()
                .FormatoData()
                .FormatoHorario()
                .PertenceAUsuarioId(userId, recadoId)
                .DataFutura();
            
            new RecadoRepository()
                .AtualizarRecado(recadoId, recadoAtualizado);
                
        }
    }
}