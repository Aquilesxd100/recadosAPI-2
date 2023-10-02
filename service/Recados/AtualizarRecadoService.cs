namespace recados_api
{
    public class AtualizarRecadoService{
        public void Service(string userId, string recadoId, RecadoModelo modelo){
            new UsuarioValidator()
                .ValidUserToken(userId);

            new RecadoValidator(modelo.Titulo, modelo.Descricao, modelo.Data, modelo.Horario)
                .CamposPreechidosAtualiza()
                .CamposType()
                .QntCaracteres()
                .CaracterInvalido()
                .FormatoData()
                .FormatoDataNow()
                .FormatoHorario()
                .PertenceAUsuarioId(userId, recadoId);
            
            new RecadoRepository()
                .AtualizarRecado(recadoId, modelo);
        }
    }
}