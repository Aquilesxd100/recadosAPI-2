### API feita para iniciar no estudo de C# 

**Usuário**

    Path
    Cadastrar: /CriarConta
        Request:
            Body {
                Username: string,
                Senha: string
            }
        Response:
            {
                mensagem: string
            }

    Login: /EntrarConta
        Request:
            Body {
                Username: string,
                Senha: string
            }
        Response:
            {
                mensagem: string,
                token: string
            }

    Deletar: /DeletarConta
        Request:
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }

    Atualizar: /AtualizarConta
        Request:
            Body {
                Username: string [opcional],
                Senha: string [opcional]
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }

**Recado**

    Path
    Criar: /CriarRecado
        Request:
            Body {
                Titulo: string,
                Descricao: string,
                Data: string (xx/xx/xx),
                Horario: string (xx:xx)
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }
            
    Deletar: /DeletarRecado
        Request:
            Query {
                recadoId: string
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }

    Atualizar: /AtualizarRecado
        Request:
            Body {
                Titulo: string [opcional],
                Descricao: string [opcional],
                Data: string (xx/xx/xx) [opcional],
                Horario: string (xx:xx) [opcional]
            },
            Query {
                recadoId: string
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }

    Mostrar Recados: /Recados
        Request:
            Query {
                buscar: string [opcional],
                depoisDe: string (xx/xx/xx) [opcional],
                antesDe: string (xx/xx/xx) [opcional],
                turnoDia: string (madrugada || manha || tarde || noite) [opcional],
                arquivado: boolean [opcional],
                vencido: boolean [opcional]
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string,
                recados: []
            }

    Arquivar/Desarquivar: /AtualizarStatusArquivado
        Request:
            Body {
                statusArquivado: boolean
            },
            Query {
                recadoId: string
            },
            Header {
                Authorization: Bearer token
            }
        Response:
            {
                mensagem: string
            }

