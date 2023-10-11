using System;
using System.Collections.Generic;

namespace recados_api
{
    public class GetRecadosService{
        public List<RecadoModeloGet> Service(string userId){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            List<RecadoModelo2> recadosRepository = new RecadoRepository()
                .GetRecados(userId);
            
            List<RecadoModeloGet> recados = new List<RecadoModeloGet>();

            recadosRepository.ForEach((recado)=>{
                var dataAtual = DateTime.Now;
                var dataInserida = DateTime.Parse(recado.Data);
                dataInserida = dataInserida.AddMinutes(int.Parse(recado.Horario.Substring(3, 2)));
                dataInserida = dataInserida.AddHours(int.Parse(recado.Horario.Substring(0, 2)));

                RecadoModeloGet aaa = new RecadoModeloGet(){
                    Titulo = recado.Titulo,
                    Descricao = recado.Descricao,
                    Data = recado.Data,
                    Horario = recado.Horario,
                    Arquivado = recado.Arquivado,
                    Id = recado.Id,
                    Vencido = DateTime.Compare(dataInserida, dataAtual) < 0,
                };
                recados.Add(aaa);

            });

            return recados;
        }
    }
}