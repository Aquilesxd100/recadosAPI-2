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
            if (!(Data is null)) {
                bool dataEhValida = true;

                if (Data.Length != 10) {
                    dataEhValida = false;
                };

                if (Data.Substring(2, 1) != "/" || Data.Substring(5, 1) != "/") {
                    dataEhValida = false;
                };

                if (
                    !int.TryParse(Data.Substring(0, 2), out _)
                    || !int.TryParse(Data.Substring(3, 2), out _)
                    || !int.TryParse(Data.Substring(6, 4), out _)
                ) {
                    dataEhValida = false;
                };

                if (!dataEhValida) {
                    throw new ErroHTTP(400, "O formato da data do recado é invalida. (xx/xx/xxxx)");
                };
            }
            return this;
        }
        public RecadoValidator FormatoHorario(){
            if (!(Horario is null)) {
                bool horaEhValida = true;

                if (Horario.Length != 5) {
                    horaEhValida = false;
                };

                if (Horario.Substring(2, 1) != ":") {
                    horaEhValida = false;
                };
///////////
                if (
                    !int.TryParse(Horario.Substring(0, 2), out _)
                    || !int.TryParse(Horario.Substring(3, 2), out _)
                ) {
                    horaEhValida = false;
                };

                if (!horaEhValida) {
                    throw new ErroHTTP(400, "O formato da data do recado é invalida. (xx:xx)");
                };
            }
            return this;
        } 
        public RecadoValidator PertenceAUsuarioId(string userId, string recadoId){
            RecadoModelo recado = RecadoRepository.EncontrarRecadoByUserIdERecadoId(userId, recadoId);
            if (recado.Titulo == null) {
                throw new ErroHTTP(403, "Você não tem acesso a esse recurso.");
            };
            return this;
        }        

    }
}