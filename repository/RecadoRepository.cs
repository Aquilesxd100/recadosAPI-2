using System;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class RecadoRepository{
        private static string connStr = Env.dataBaseURL;
        private static MySqlConnection conn = new MySqlConnection(connStr);

        public void CriarRecado(RecadoModelo modelo){

            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = $"INSERT INTO Recado VALUES ('{modelo.Titulo}', '{modelo.Descricao}', '{modelo.Data}', '{modelo.Horario}', '{modelo.Arquivado}', '{modelo.Id}', '{modelo.UsuarioId}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        public void DeletarRecado(string recadoId){

            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = $"DELETE FROM Recado WHERE Id = {recadoId}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteReader();
            conn.Close();
        }

        public static RecadoModelo EncontrarRecadoByUserId(string usuarioId, string recadoId){
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = $"SELECT * FROM Recado WHERE Usuario_Id = '{usuarioId}' and Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            RecadoModelo recado = new RecadoModelo();

            while(leitor.Read()){
                recado.Titulo = leitor["Titulo"].ToString();
                recado.Descricao = leitor["Descricao"].ToString();
                recado.Data = leitor["Data"].ToString();
                recado.Horario = leitor["Horario"].ToString();
                recado.Arquivado = leitor["Arquivado"].ToString() == "true" ; 
                recado.Id = leitor["Id"].ToString();
                recado.UsuarioId = leitor["Usuario_Id"].ToString();
            }

            leitor.Close();
            conn.Close();
            return recado;
        }
    }
}