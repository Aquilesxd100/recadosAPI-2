namespace recados_api
{
    public class AtualizarRecadoService{
        public void Service(string userId, string recadoId, RecadoModelo modelo){
            new UsuarioValidator()
                .ValidUserToken(userId);

            RecadoModelo recado = RecadoRepository.EncontrarRecadoById(recadoId);
            RecadoModelo recadoAtualizado = new RecadoModelo{
                Titulo = modelo.Titulo ?? recado.Titulo,
                Descricao = modelo.Descricao ?? recado.Descricao,
                Data = modelo.Data ?? recado.Data,
                Horario = modelo.Horario ?? recado.Horario,
            };

            new RecadoValidator(
                recadoAtualizado.Titulo, 
                recadoAtualizado.Descricao, 
                modelo.Data == null && modelo.Horario == null ? null : recadoAtualizado.Data, 
                modelo.Horario == null && modelo.Data == null ? null : recadoAtualizado.Horario
            )
                    .CamposPreechidosAtualiza()
                    .CamposType()
                    .QntCaracteres()
                    .CaracterInvalido()
                    .FormatoData()
                    .DataFutura()
                    .FormatoHorario()
                    .PertenceAUsuarioId(userId, recadoId);
            
            new RecadoRepository()
                .AtualizarRecado(recadoId, recadoAtualizado);
        }
    }
}