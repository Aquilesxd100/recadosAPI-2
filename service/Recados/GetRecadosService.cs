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
                var dataAtual = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

                int year = int.Parse(recado.Data.Substring(6, 4));
                int month = int.Parse(recado.Data.Substring(3, 2));
                int day = int.Parse(recado.Data.Substring(0, 2));

                int hours = int.Parse(recado.Horario.Substring(0, 2));
                int minutes = int.Parse(recado.Horario.Substring(3, 2));

                var dataInserida = new DateTime(year, month, day, hours, minutes, 0);

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