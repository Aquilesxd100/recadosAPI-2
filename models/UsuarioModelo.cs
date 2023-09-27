using System;

namespace recados_api
{
    public class UsuarioModelo
    {
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Id { get; set; }

        public UsuarioModelo(string Username, string Senha, string Id){
            this.Username = Username;
            this.Senha = Senha;
            this.Id = Id;
        }
    };

};