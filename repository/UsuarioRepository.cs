using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class UsuarioRepository {
        private static readonly MySqlConnection conn = Database.conexao;

        public void CriarConta(UsuarioModelo usuarioInfos){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Criar Conta");
            string sql = $"INSERT INTO Usuario VALUES ('{usuarioInfos.Username}', '{usuarioInfos.Senha}', '{usuarioInfos.Id}')";
            new Database().ExecuteSql(sql);
        }
        public string EntrarConta(UsuarioModelo usuarioInfos){
            Database.VerificarConexao();
            UsuarioModelo user = EncontrarUsuarioByUsername(usuarioInfos.Username);

            if(user.Username == null && user.Senha == null && user.Id == null || usuarioInfos.Senha != user.Senha){
                throw new ErroHTTP(404, "Nenhum Usuário com esse Username e Senha encontrado.");
            }

            var token = TokenService.GenerateToken(user.Id);
            return token;
        }
        public void DeletarConta(string userId){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Deletar Conta");
            string sql = $"DELETE FROM Usuario WHERE Id = '{userId}'";
            new Database().ExecuteSql(sql);
        }
        public void AtualizarConta(string userId, UsuarioModelo modelo){
            Database.VerificarConexao();
            UsuarioModelo user = EncontrarUsuarioById(userId);
            if(user.Id == null){
                throw new ErroHTTP(404, "Nenhum Usuário com esse Id encontrado.");
            }

            var userAtualizado = new {
                Username = modelo.Username ?? user.Username,
                Senha = modelo.Senha ?? user.Senha
            };

            Console.WriteLine("Connecting to MySQL... Atualizar Conta");
            string sql = $"UPDATE Usuario SET Username = '{userAtualizado.Username}', Senha = '{userAtualizado.Senha}' WHERE Id = '{userId}'";
            new Database().ExecuteSql(sql);
        }
        public static UsuarioModelo  EncontrarUsuarioByUsername(string Username){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Encontrar Usuario By Username");
            string sql = $"SELECT * FROM Usuario WHERE Username = '{Username}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            UsuarioModelo user = new UsuarioModelo();

            while(leitor.Read()){
                user.Username = leitor["Username"].ToString();
                user.Senha = leitor["Senha"].ToString();
                user.Id = leitor["Id"].ToString();
            }

            leitor.Close();
            return user;
        }

        public static UsuarioModelo EncontrarUsuarioById(string Id){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Encontrar Usuario By Id");
            string sql = $"SELECT * FROM Usuario WHERE Id = '{Id}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            UsuarioModelo user = new UsuarioModelo();

            while(leitor.Read()){
                user.Username = leitor["Username"].ToString();
                user.Senha = leitor["Senha"].ToString();
                user.Id = leitor["Id"].ToString();
            }

            leitor.Close();
            return user;
        }
    }
}