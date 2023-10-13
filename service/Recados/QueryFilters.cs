using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace recados_api
{
    public class QueriesFiltros{
        public QueriesFiltrosRecadosModelo _queries;

        public List<RecadoModeloGet> _recados;

        public QueriesFiltros(QueriesFiltrosRecadosModelo queries, List<RecadoModeloGet> recados)
        {
            _queries = queries;
            _recados = recados;
        }
        //FALTA TESTAR
        public QueriesFiltros Buscar() {

            if (_queries.buscar is string) {
                var filter = _queries.buscar.ToLower();

                _recados = _recados.FindAll(
                    (recado) => 
                    recado.Descricao.ToLower().Contains(filter) 
                    || recado.Titulo.ToLower().Contains(filter)
                );
            }
            return this;
        }
        //FALTA TESTAR
        public QueriesFiltros TurnoDia() {

            if (_queries.turnoDia is string) {
                var filter = _queries.turnoDia.ToLower();
                var filtroTurno = new {
                    inicioTurno = "",
                    finalTurno = ""
                };
                switch(filter) {
                    case "madrugada":
                        filtroTurno = new {
                            inicioTurno = "00:00",
                            finalTurno = "04:59"
                        };
                    break;
                    case "manhã": case "manha":
                        filtroTurno = new {
                            inicioTurno = "05:00",
                            finalTurno = "12:59"
                        };
                    break;
                    case "tarde":
                        filtroTurno = new {
                            inicioTurno = "13:00",
                            finalTurno = "18:59"
                        };
                    break;
                    case "noite":
                        filtroTurno = new {
                            inicioTurno = "19:00",
                            finalTurno = "23:59"
                        };
                    break;
                    default:
                        filtroTurno = null;
                    break;
                }
                if (!(filtroTurno is null)) {
                    int inicioHoraTurno = int.Parse(filtroTurno.inicioTurno.Substring(0, 2));
                    int finalHoraTurno = int.Parse(filtroTurno.finalTurno.Substring(0, 2));

                    _recados = _recados.FindAll(
                        (recado) => {
                            int horaRecado = int.Parse(recado.Horario.Substring(0, 2));
                            if (horaRecado >= inicioHoraTurno && horaRecado <= finalHoraTurno) {
                                return true;
                            }
                            return false;
                        }
                        
                    );
                }
            }
            return this;
        }

//PAREI AQUI
        public QueriesFiltros Arquivado() {
            if(!(_queries.arquivado is null)){
                var filter = _queries.arquivado == "true";
                if (!filter) {
                    _recados = _recados.FindAll(
                        (recado) => 
                        !recado.Arquivado 
                    );
                }
            }
            return this;
        }

        public QueriesFiltros DepoisDe(){
            if (_queries.depoisDe is string && Regex.IsMatch(_queries.depoisDe, @"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])/\d{4}$")){
                _recados = _recados.FindAll(
                    (recado) => {
                        var dataAtual = DateTime.Now;
                        var dataInserida = DateTime.Parse(recado.Data);
                        if(DateTime.Compare(dataAtual, dataInserida) > 0){
                            return true;
                        }
                        return false;
                    }
                );
            }
            return this;            
        }
        public QueriesFiltros AntesDe(){
            if (_queries.antesDe is string && Regex.IsMatch(_queries.antesDe, @"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])/\d{4}$")){
                _recados = _recados.FindAll(
                    (recado) => {
                        var dataAtual = DateTime.Now;
                        var dataInserida = DateTime.Parse(recado.Data);
                        if(DateTime.Compare(dataAtual, dataInserida) < 0){
                            return true;
                        }
                        return false;
                    }
                );
            }
            return this;            
        }

        public QueriesFiltros Vencido(){
            if(!(_queries.vencido is null)){
                if(_queries.vencido.ToLower() == "true"){
                    _recados = _recados.FindAll((recado)=>{
                        if(recado.Vencido){
                            return true;
                        }
                        return false;
                    });
                }
                if(_queries.vencido.ToLower() == "false"){
                    _recados = _recados.FindAll((recado)=>{
                        if(!recado.Vencido){
                            return true;
                        }
                        return false;
                    });
                }
            }
            return this;
        }
    }
}