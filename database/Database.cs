using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class Database{
        private static string connStr = Env.dataBaseURL;
        public static MySqlConnection conexao = new MySqlConnection(connStr);

        static public void AbrirConexao(){
           try{
                if(conexao.State == ConnectionState.Open){
                    Console.WriteLine("ABERTA JA");
                }

                if(conexao.State == ConnectionState.Closed){
                    Console.WriteLine("ABRINDO");
                    conexao.Open();
                }
            
            } catch (Exception ex) {
                Console.WriteLine("Ocorreu um erro ao abrir a conexão: " + ex.Message);
            }
        }

        public void ExecuteSql(string sql){
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();
        }
    }
}