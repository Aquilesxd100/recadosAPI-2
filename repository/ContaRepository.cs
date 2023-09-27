using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class ContaRepository {
        private static string connStr = "server=db4free.net;user=javadevstests;database=lembretesjava6;port=3306;password=f262e259";
        private static MySqlConnection conn = new MySqlConnection(connStr);

        public void CriarNovaConta(UsuarioModelo usuarioInfos){
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = $"INSERT INTO Usuario VALUES ('{usuarioInfos.Username}', '{usuarioInfos.Senha}', '{usuarioInfos.Id}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
            }catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new ArgumentException("Ocorreu um erro interno.");
            }
            conn.Close();
        }
    }
}