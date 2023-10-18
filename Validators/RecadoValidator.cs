using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    public class RecadoValidator
    {
        readonly string Titulo;
        readonly string Descricao;
        readonly string Data;
        readonly string Horario;

        public RecadoValidator(string titulo = null, string descricao = null, string data = null, string horario = null) {
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            Horario = horario;
        }

        public RecadoValidator CaracterInvalido() {
            string regex = "[´`{}']";
            
            if (Titulo is string && Regex.IsMatch(Titulo, regex)) {
                throw new ErroHTTP(
                    400, 
                    "O titulo do recado não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            };
            
            if (Descricao is string && Regex.IsMatch(Descricao, regex)) {
                throw new ErroHTTP(
                    400, 
                    "A descrição do recado não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            }

            return this;
        }
        
        public RecadoValidator CamposPreechidos(){
            if (Data == null || Horario == null || Descricao == null || Titulo == null){
                throw new ErroHTTP(400, "Preencha todos os campos. (data, horario, descricao e titulo)");
            };
            return this;
        }

        public RecadoValidator CamposPreechidosAtualiza(string dataBruta, string horarioBruto){
            if (dataBruta == null && horarioBruto == null && Descricao == null && Titulo == null){
                throw new ErroHTTP(400, "Preencha pelo menos um dos campos. (data, horario, descricao ou titulo)");
            };
            return this;
        }
        public RecadoValidator CamposType(){
            if (
                !(Titulo is null) && !(Titulo is string)
                && !(Descricao is null) && !(Descricao is string)
                && !(Data is null) && !(Data is string)
                && !(Horario is null) && !(Horario is string)
            ) {
                throw new ErroHTTP(400, "Tipo de um ou mais campos inválido.");
            };
            return this;
        }


        public RecadoValidator QntCaracteres(){
            if (Titulo is string && (Titulo.Length < 5 || Titulo.Length > 35)){
                throw new ErroHTTP(400, "O titulo do recado deve ter no minimo 5 caracteres e no maximo 35.");
            };
            if (Descricao is string && (Descricao.Length < 5 || Descricao.Length > 200)) {
                throw new ErroHTTP(400, "A descrição do recado deve ter no minimo 5 caracteres e no maximo 200.");
            };
            return this;
        }   

        public RecadoValidator FormatoData(){
            bool formatoDataEhValido(){
                if (Data.Length != 10) {
                    return false;
                };

                if (Data.Substring(2, 1) != "/" || Data.Substring(5, 1) != "/") {
                    return false;
                };

                if (
                    !int.TryParse(Data.Substring(0, 2), out _)
                    || !int.TryParse(Data.Substring(3, 2), out _)
                    || !int.TryParse(Data.Substring(6, 4), out _)
                ) {
                    return false;
                };

                if (int.Parse(Data.Substring(0, 2)) > 31 ||  int.Parse(Data.Substring(0, 2)) <= 0) {
                    return false;
                };

                if (int.Parse(Data.Substring(3, 2)) > 12 ||  int.Parse(Data.Substring(3, 2)) <= 0) {
                    return false;
                };

                return true;
            };
            if (!(Data is null) && !formatoDataEhValido()) {
                throw new ErroHTTP(400, "O formato da data do recado é invalida. (xx/xx/xxxx)");
            };

            return this;
        }


        public RecadoValidator DataFutura(){
            if(!(Data is null)){
                var dataAtual = DateTime.Now;
                var dataInserida = DateTime.Parse(Data);
                dataInserida = dataInserida.AddMinutes(int.Parse(Horario.Substring(3, 2)));
                dataInserida = dataInserida.AddHours(int.Parse(Horario.Substring(0, 2)));

                if (DateTime.Compare(dataInserida, dataAtual) < 0) {
                    throw new ErroHTTP(400, "Data inválida, insira uma data presente ou futura.");
                }
            }
            return this;
        }

        
        public RecadoValidator FormatoHorario(){
            bool formatoHoraEhValido(){
                if (Horario.Length != 5) {
                    return false;
                };

                if (Horario.Substring(2, 1) != ":") {
                    return false;
                };

                if (
                    !int.TryParse(Horario.Substring(0, 2), out _)
                    || !int.TryParse(Horario.Substring(3, 2), out _)
                ) {
                    return false;
                };

                if (int.Parse(Horario.Substring(0, 2)) < 0 || int.Parse(Horario.Substring(0, 2)) > 23) {
                    return false;
                };

                if (int.Parse(Horario.Substring(3, 2)) < 0 || int.Parse(Horario.Substring(3, 2)) > 59) {
                    return false;
                };

                return true;
            }
            if (!(Horario is null) && !formatoHoraEhValido()) {
                throw new ErroHTTP(400, "O formato do horário do recado é invalido. (xx:xx)");
            };
            return this;
        } 

        public RecadoValidator IdRecadoEhValido(string recadoId){
            if (recadoId == null || recadoId is string && recadoId.Length != 36) {
                throw new ErroHTTP(400, "Id de recado invalido.");
            };
            return this;
        }

        public RecadoValidator PertenceAUsuarioId(string userId, string recadoId){
            RecadoModelo recado = RecadoRepository.EncontrarRecadoByUserIdERecadoId(userId, recadoId);
            if (recado.Titulo == null) {
                throw new ErroHTTP(404, "Nenhum recado com esse Id foi encontrado.");
            };
            return this;
        }        

    }
}