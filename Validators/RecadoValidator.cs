using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    public class RecadoValidator
    {
        readonly RecadoModelo Recado;

        public RecadoValidator(RecadoModelo recado = null) {
            Recado = recado;
        }

        public RecadoValidator CaracterInvalido() {
            string regex = "[´`{}']";
            
            if (Regex.IsMatch(Recado.Titulo, regex)) {
                throw new ErroHTTP(
                    400, 
                    "O titulo do recado não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            };
            
            if (Regex.IsMatch(Recado.Descricao, regex)) {
                throw new ErroHTTP(
                    400, 
                    "A descrição do recado não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            }

            return this;
        }
        
        public RecadoValidator CamposPreechidos(){
            if (Recado.Data == null || Recado.Horario == null || Recado.Descricao == null || Recado.Titulo == null){
                throw new ErroHTTP(400, "Preencha todos os campos. (data, horario, descricao e titulo)");
            };
            return this;
        }
        public RecadoValidator CamposType(){
            if (
                !(Recado.Titulo is null) && !(Recado.Titulo is string)
                && !(Recado.Descricao is null) && !(Recado.Descricao is string)
                && !(Recado.Data is null) && !(Recado.Data is string)
                && !(Recado.Horario is null) && !(Recado.Horario is string)
            ) {
                throw new ErroHTTP(400, "Tipo de um ou mais campos inválido.");
            };
            return this;
        }


        public RecadoValidator QntCaracteres(){
            if (Recado.Titulo is string && Recado.Titulo.Length < 5 || Recado.Titulo.Length > 35){
                throw new ErroHTTP(400, "O titulo do recado deve ter no minimo 5 caracteres e no maximo 35.");
            };
            if (Recado.Descricao is string && Recado.Descricao.Length < 5 || Recado.Descricao.Length > 200) {
                throw new ErroHTTP(400, "A descrição do recado deve ter no minimo 5 caracteres e no maximo 200.");
            };
            return this;
        }   

        public RecadoValidator FormatoData(){
            if (!(Recado.Data is null)) {
                bool dataEhValida = true;

                if (Recado.Data.Length != 10) {
                    dataEhValida = false;
                };

                if (Recado.Data.Substring(2, 1) != "/" || Recado.Data.Substring(5, 1) != "/") {
                    dataEhValida = false;
                };

                if (
                    !int.TryParse(Recado.Data.Substring(0, 2), out _)
                    || !int.TryParse(Recado.Data.Substring(3, 2), out _)
                    || !int.TryParse(Recado.Data.Substring(6, 4), out _)
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
            if (!(Recado.Horario is null)) {
                bool horaEhValida = true;

                if (Recado.Horario.Length != 5) {
                    horaEhValida = false;
                };

                if (Recado.Horario.Substring(2, 1) != ":") {
                    horaEhValida = false;
                };
///////////
                if (
                    !int.TryParse(Recado.Horario.Substring(0, 2), out _)
                    || !int.TryParse(Recado.Horario.Substring(3, 2), out _)
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
            RecadoModelo recado = RecadoRepository.EncontrarRecadoByUserId(userId, recadoId);
            if (recado.Titulo == null) {
                throw new ErroHTTP(403, "Você não tem acesso a esse recurso.");
            };
            return this;
        }        

    }
}