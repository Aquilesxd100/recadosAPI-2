using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace recados_api
{
    public class RecadoRepository{
        private static readonly MySqlConnection conn = Database.conexao;

        public void CriarRecado(RecadoModelo modelo){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Criar Recado");
            string sql = $"INSERT INTO Recado VALUES ('{modelo.Titulo}', '{modelo.Descricao}', '{modelo.Data}', '{modelo.Horario}', {modelo.Arquivado}, '{modelo.Id}', '{modelo.UsuarioId}')";
            new Database().ExecuteSql(sql);
        }

        public void DeletarRecado(string recadoId){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Deletar Recado");
            string sql = $"DELETE FROM Recado WHERE Id = '{recadoId}'";
            new Database().ExecuteSql(sql);
        }

        public void AtualizarRecado(string recadoId, RecadoModelo modelo){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Atualizar Recado");
            string sql = $"UPDATE Recado SET Titulo = '{modelo.Titulo}', Descricao = '{modelo.Descricao}', Data = '{modelo.Data}', Horario = '{modelo.Horario}' WHERE Id = '{recadoId}'";
            new Database().ExecuteSql(sql);
        }
        public void AtualizaStatusArquivadoRecado(string recadoId, bool statusArquivado){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Arquivar Recado");
            string sql = $"UPDATE Recado SET Arquivado = {statusArquivado} WHERE Id = '{recadoId}'";
            new Database().ExecuteSql(sql);
        }

        public List<RecadoModelo2> GetRecados(string userId){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Get Recado");
            string sql = $"SELECT * FROM Recado WHERE Usuario_Id = '{userId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            List<RecadoModelo2> listaRecados = new List<RecadoModelo2>();

            while(leitor.Read()){
                RecadoModelo2 recado = new RecadoModelo2(){
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
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Encontrar Recado By UserId E RecadoId");
            string sql = $"SELECT * FROM Recado WHERE Usuario_Id = '{usuarioId}' and Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            RecadoModelo recado = new RecadoModelo();
            while(leitor.Read()){
                recado.Titulo = leitor["Titulo"].ToString();
                recado.Descricao = leitor["Descricao"].ToString();
                recado.Data = leitor["Data"].ToString();
                recado.Horario = leitor["Horario"].ToString();
                recado.Arquivado = leitor["Arquivado"].ToString() == "True";
                recado.Id = leitor["Id"].ToString();
                recado.UsuarioId = leitor["Usuario_Id"].ToString();
            }

            leitor.Close();
            return recado;
        }

        public static RecadoModelo EncontrarRecadoById(string recadoId){
            Database.VerificarConexao();
            Console.WriteLine("Connecting to MySQL... Encontrar Recado By Id");
            string sql = $"SELECT * FROM Recado WHERE Id = '{recadoId}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader leitor = cmd.ExecuteReader();

            RecadoModelo recado = new RecadoModelo();

            while(leitor.Read()){
                recado.Titulo = leitor["Titulo"].ToString();
                recado.Descricao = leitor["Descricao"].ToString();
                recado.Data = leitor["Data"].ToString();
                recado.Horario = leitor["Horario"].ToString();
                recado.Arquivado = leitor["Arquivado"].ToString() == "True" ; 
                recado.Id = leitor["Id"].ToString();
                recado.UsuarioId = leitor["Usuario_Id"].ToString();
            }

            leitor.Close();
            return recado;
        }
    }
}