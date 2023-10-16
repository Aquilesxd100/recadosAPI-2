namespace recados_api
{
    public class UsuarioModelo
    {
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Id { get; set; }

    };
    public class UsuarioBruto
    {
        public object Username { get; set; }
        public object Senha { get; set; }
        public object Id { get; set; }

    };

};