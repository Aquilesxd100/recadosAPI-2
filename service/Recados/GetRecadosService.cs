using System;
using System.Collections.Generic;

namespace recados_api
{
    public class GetRecadosService{
        public List<RecadoModeloGet> Service(string userId, QueriesFiltrosRecadosModelo queries){
            new UsuarioValidator()
                .ValidUserToken(userId);
            
            List<RecadoModelo2> recadosRepository = new RecadoRepository()
                .GetRecados(userId);

            List<RecadoModeloGet> recados = new List<RecadoModeloGet>();

            recadosRepository.ForEach((recado)=>{
                var dataAtual = DateTime.Now;
                var dataInserida = DateTime.Now;

                dataInserida = dataInserida.AddYears(int.Parse(recado.Data.Substring(6, 4)));
                dataInserida = dataInserida.AddMonths(int.Parse(recado.Data.Substring(3, 2)));
                dataInserida = dataInserida.AddDays(int.Parse(recado.Data.Substring(0, 2)));

                dataInserida = dataInserida.AddMinutes(int.Parse(recado.Horario.Substring(3, 2)));
                dataInserida = dataInserida.AddHours(int.Parse(recado.Horario.Substring(0, 2)));

                RecadoModeloGet recadoComVencimento = new RecadoModeloGet(){
                    Titulo = recado.Titulo,
                    Descricao = recado.Descricao,
                    Data = recado.Data,
                    Horario = recado.Horario,
                    Arquivado = recado.Arquivado,
                    Id = recado.Id,
                    Vencido = DateTime.Compare(dataInserida, dataAtual) < 0,
                };
                recados.Add(recadoComVencimento);
            });

            QueriesFiltros recadosQueries = new QueriesFiltros(queries, recados)
                .Buscar()
                .DepoisDe()
                .AntesDe()
                .TurnoDia()
                .Arquivado()
                .Vencido();

            return recadosQueries._recados;
        }
    }
}