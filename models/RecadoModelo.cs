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

    public class RecadoBruto
    {
        public object Titulo { get; set; }
        public object Descricao { get; set; }
        public object Data { get; set; }
        public object Horario { get; set; }
        public object Arquivado { get; set; }
        public object Id { get; set; }
        public object Vencido { get; set; }
        public object UsuarioId { get; set; }

    }
};