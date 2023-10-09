using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class RecadoRepository{
        private static readonly MySqlConnection conn = Database.conexao;

        public void CriarRecado(RecadoModelo modelo){
            Database.AbrirConexao();
            string sql = $"INSERT INTO Recado VALUES ('{modelo.Titulo}', '{modelo.Descricao}', '{modelo.Data}', '{modelo.Horario}', {modelo.Arquivado}, '{modelo.Id}', '{modelo.UsuarioId}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();
        }

        public void DeletarRecado(string recadoId){
            string sql = $"DELETE FROM Recado WHERE Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();
        }

        public void AtualizarRecado(string recadoId, RecadoModelo modelo){
            string sql = $"UPDATE Recado SET Titulo = '{modelo.Titulo}', Descricao = '{modelo.Descricao}', Data = '{modelo.Data}', Horario = '{modelo.Horario}' WHERE Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();;
        }
        public void ArquivaRecado(string recadoId){
            string sql = $"UPDATE Recado SET Arquivado = true WHERE Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();;
        }
        
        public void DesarquivaRecado(string recadoId){
            string sql = $"UPDATE Recado SET Arquivado = false WHERE Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();
            leitor.Close();;
        }

        public List<RecadoModeloGet> GetRecados(string userId){
            string sql = $"SELECT * FROM Recado WHERE Usuario_Id = '{userId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            List<RecadoModeloGet> listaRecados = new List<RecadoModeloGet>();

            while(leitor.Read()){
                RecadoModeloGet recado = new RecadoModeloGet(){
                    Titulo = leitor["Titulo"].ToString(),
                    Descricao = leitor["Descricao"].ToString(),
                    Data = leitor["Data"].ToString(),
                    Horario = leitor["Horario"].ToString(),
                    Arquivado = leitor["Arquivado"].ToString() == "True" , 
                    Id = leitor["Id"].ToString(),
                };

                listaRecados.Add(recado);
            }

            leitor.Close();
            return listaRecados;
        }

        public static RecadoModelo EncontrarRecadoByUserIdERecadoId(string usuarioId, string recadoId){
            string sql = $"SELECT * FROM Recado WHERE Usuario_Id = '{usuarioId}' and Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            RecadoModelo recado = new RecadoModelo();

            while(leitor.Read()){
                recado.Titulo = leitor["Titulo"].ToString();
                recado.Descricao = leitor["Descricao"].ToString();
                recado.Data = leitor["Data"].ToString();
                recado.Horario = leitor["Horario"].ToString();
                recado.Arquivado = leitor["Arquivado"].ToString() == "true";
                recado.Id = leitor["Id"].ToString();
                recado.UsuarioId = leitor["Usuario_Id"].ToString();
            }

            leitor.Close();
            return recado;
        }

        public static RecadoModelo EncontrarRecadoById(string recadoId){
            string sql = $"SELECT * FROM Recado WHERE Id = '{recadoId}'";
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
            return recado;
        }
    }
}