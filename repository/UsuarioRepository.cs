using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class UsuarioRepository {
        private static string connStr = "server=db4free.net;user=javadevstests;database=lembretesjava6;port=3306;password=f262e259";
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
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = $"SELECT * FROM Usuario WHERE Username = '{usuarioInfos.Username}' and Senha = '{usuarioInfos.Senha}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader data = cmd.ExecuteReader();

                if(!data.Read()){
                    conn.Close();
                    throw new ErroHTTP(404, "Nenhum Usuário com esse Username e Senha encontrado.");
                }

                conn.Close();
                return "aaa";

            }catch (Exception ex){
                conn.Close();
                Console.WriteLine(ex.ToString());
                throw new ErroHTTP(500, "Ocorreu um erro interno.");
            }
        }
    }
}