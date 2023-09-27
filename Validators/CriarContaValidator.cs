namespace recados_api
{
    public class CriarContaValidator
    {
        
        public ErroHTTP Validator(UsuarioModelo novaConta){
            if (novaConta.Username == null || novaConta.Senha == null){
                throw new ErroHTTP(400, "Preencha todos os campos. (username e senha)");
            };

            if (!(novaConta.Username is string) || !(novaConta.Senha is string)) {
                throw new ErroHTTP(400, "Tipo de um ou mais campos inválido.");
            };

            if (novaConta.Username.Length < 5 || novaConta.Username.Length > 20){
                throw new ErroHTTP(400, "O Username deve ter no minimo 5 caracteres e no maximo 20.");
            };

            for (int index = 0; index < novaConta.Username.Length; index++) {
                if(!char.IsLetterOrDigit(novaConta.Username, index)) {
                    throw new ErroHTTP(400, "O Username não pode ter caracteres especiais.");
                }
            }
            
            if (novaConta.Senha.Length > 20 || novaConta.Senha.Length < 8) {
                throw new ErroHTTP(400, "A senha deve ter no minimo 8 caracteres e no maximo 20.");
            };

            bool senhaTemNumero = false;
            bool senhaTemLetraMaiuscula = false;
            bool senhaTemLetraMinuscula = false;
            bool senhaTemCaractereEspecial = false;
            for (int index = 0; index < novaConta.Senha.Length; index++) {
                if (char.IsDigit(novaConta.Senha, index)) {
                    senhaTemNumero = true;
                } else {
                    if (char.IsUpper(novaConta.Senha, index)) {
                        senhaTemLetraMaiuscula = true;
                    } else if (char.IsLower(novaConta.Senha, index)) {
                        senhaTemLetraMinuscula = true;
                    }
                }
                if(!char.IsLetterOrDigit(novaConta.Senha, index)) {
                    senhaTemCaractereEspecial = true;
                }
            }
            if (!senhaTemNumero 
                || !senhaTemLetraMaiuscula 
                || !senhaTemLetraMinuscula 
                || !senhaTemCaractereEspecial
            ) {
                throw new ErroHTTP(400, "A senha deve ter ao menos um número, uma letra minuscula, uma letra maiúscula e um caractere especial.");
            }

            return null;
        }
    }
}