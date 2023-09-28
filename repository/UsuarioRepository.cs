using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class UsuarioRepository {
        private static string connStr = Env.dataBaseURL;
        private static MySqlConnection conn = new MySqlConnection(connStr);

        public void CriarConta(UsuarioModelo usuarioInfos){
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = $"INSERT INTO Usuario VALUES ('{usuarioInfos.Username}', '{usuarioInfos.Senha}', '{usuarioInfos.Id}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new ErroHTTP(500, "Ocorreu um erro interno.");
            }
        }
        public string EntrarConta(UsuarioModelo usuarioInfos){
            UsuarioModelo user = EncontrarUsuarioByUsername(usuarioInfos.Username);

            if(user.Username == null && user.Senha == null && user.Id == null || usuarioInfos.Senha != user.Senha){
                throw new ErroHTTP(404, "Nenhum Usuário com esse Username e Senha encontrado.");
            }

            var token = TokenService.GenerateToken(user.Id);
            return token;
        }
        public static UsuarioModelo EncontrarUsuarioByUsername(string Username){
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
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
            conn.Close();
            return user;
        }

        public static UsuarioModelo EncontrarUsuarioById(string Id){
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
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
            conn.Close();
            return user;
        }


    }
}