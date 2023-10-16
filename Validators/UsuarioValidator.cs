using System.Text.RegularExpressions;

namespace recados_api
{
    public class UsuarioValidator
    {
        readonly string Username;
        readonly string Senha;

        public UsuarioValidator(string username = null, string senha = null) {
            Username = username;
            Senha = senha;
        }

        public UsuarioValidator CaracterInvalido() {
            string regex = "[´`{}']";
            
            if (Username is string && Regex.IsMatch(Username, regex)) {
                throw new ErroHTTP(
                    400, 
                    "O Username não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            };
            
            if (Senha is string && Regex.IsMatch(Senha, regex)) {
                throw new ErroHTTP(
                    400, 
                    "A Senha não pode conter caracteres inválidos. (´, `, {, }, ')"
                );
            }
            return this;
        }
        
        public UsuarioValidator PreencherCampoAtualizar(){
            if (Username is null && Senha is null){
                throw new ErroHTTP(400, "Preencha pelo menos um dos campos (username ou senha).");
            };
            return this;
        }

        public UsuarioValidator PreencherCampos(){
            if (Username is null || Senha is null){
                throw new ErroHTTP(400, "Preencha todos os campos (username e senha).");
            };
            return this;
        }

        public UsuarioValidator CampoTypeAtualizar(){
            if ( !(Username is null) && !(Username is string)){
                throw new ErroHTTP(400, "O Username precisa estar em formato de texto.");
            };
            if (!(Senha is null) && !(Senha is string)){
                throw new ErroHTTP(400, "A senha precisa estar em formato de texto.");
            };
            return this;
        }
        
        public UsuarioValidator CamposType(){
            if (!(Username is null) && !(Username is string) || !(Senha is null) && !(Senha is string)) {
                throw new ErroHTTP(400, "Um ou mais campos inválido(s) ou vazio(s).");
            };
            return this;
        }

        public UsuarioValidator UsernameJaExiste(){
            if (!(Username is string)) {
                return this;
            }
            UsuarioModelo user = UsuarioRepository.EncontrarUsuarioByUsername(Username);
            if (user.Username != null){
                throw new ErroHTTP(400, "Username já existente.");
            };
            return this;
        }

        public UsuarioValidator UsernameValid() {
            bool usernameTemNumero = false;
            for (int index = 0; index < Username.Length; index++) {
                if (char.IsDigit(Username, index)) {
                    usernameTemNumero = true;
                } 
            }
            
            if(usernameTemNumero){
                throw new ErroHTTP(400, "Username não pode conter números.");
            }
            return this;
        }

        public UsuarioValidator QntCaracteres(){
            if (Username is string && (Username.Length < 5 || Username.Length > 20)){
                throw new ErroHTTP(400, "O Username deve ter no minimo 5 caracteres e no maximo 20.");
            };
            if (Senha is string && (Senha.Length > 20 || Senha.Length < 8)) {
                throw new ErroHTTP(400, "A senha deve ter no minimo 8 caracteres e no maximo 20.");
            };
            return this;
        }   

        public UsuarioValidator SenhaValid(){ 
            if(Senha == null){
                return this;
            }
            bool senhaTemNumero = false;
            bool senhaTemLetraMaiuscula = false;
            bool senhaTemLetraMinuscula = false;
            bool senhaTemCaractereEspecial = false;
            for (int index = 0; index < Senha.Length; index++) {
                if (char.IsDigit(Senha, index)) {
                    senhaTemNumero = true;
                } else {
                    if (char.IsUpper(Senha, index)) {
                        senhaTemLetraMaiuscula = true;
                    } else if (char.IsLower(Senha, index)) {
                        senhaTemLetraMinuscula = true;
                    }
                }
                if(!char.IsLetterOrDigit(Senha, index)) {
                    senhaTemCaractereEspecial = true;
                }
            }
            if (!senhaTemNumero 
                || !senhaTemLetraMaiuscula 
                || !senhaTemLetraMinuscula 
                || !senhaTemCaractereEspecial
            ) {
                throw new ErroHTTP(
                    400, 
                    "A senha deve ter ao menos um número, uma letra minuscula, uma letra maiúscula e um caractere especial."
                );
            }
            return this;
        }

        public UsuarioValidator ValidUserToken(string userTokenId)
        {
            UsuarioModelo user = UsuarioRepository.EncontrarUsuarioById(userTokenId);
            if (user.Id == null) {
                throw new ErroHTTP(404, "Essa conta de usuario não existe mais.");
            };
            
            return this;
        }
    }
}