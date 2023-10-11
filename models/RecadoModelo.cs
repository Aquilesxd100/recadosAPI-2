namespace recados_api
{
    public class RecadoModelo2
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public bool Arquivado { get; set; }
        public string Id { get; set; }
    };
    public class RecadoModeloGet : RecadoModelo2
    {
        public bool Vencido { get; set; }
    };
    public class RecadoModelo : RecadoModeloGet
    {
        public string UsuarioId { get; set; }

    };
};