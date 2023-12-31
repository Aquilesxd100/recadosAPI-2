using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace recados_api
{
    [ApiController]
    public class RecadosController : ControllerBase
    {
        [Route("/CriarRecado")]
        [HttpPost]
        [Authorize]
        public IActionResult CriarRecado([FromBody] RecadoBruto modeloBruto)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new CriarRecadoService().Service(userId, modeloBruto);
                
                return Ok(new {
                    mensagem = "Recado criado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/DeletarRecado")]
        [HttpDelete]
        [Authorize]
        public IActionResult DeletarRecado([FromQuery] string recadoId)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new DeletarRecadoService().Service(userId, recadoId);
                
                return Ok(new {
                    mensagem = "Recado deletado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
        [Route("/AtualizarRecado")]
        [HttpPut]
        [Authorize]
        public IActionResult AtualizarRecado([FromQuery] string recadoId, [FromBody] RecadoBruto modelo)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                new AtualizarRecadoService().Service(userId, recadoId, modelo);
                
                return Ok(new {
                    mensagem = "Recado atualizado com sucesso!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/Recados")]
        [HttpGet]
        [Authorize]
        public IActionResult GetRecados([FromQuery] string buscar, string depoisDe, string antesDe, string turnoDia, string arquivado, string vencido)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;

                QueriesFiltrosRecadosModelo queries = new QueriesFiltrosRecadosModelo(){
                    buscar= buscar,
                    antesDe = antesDe,
                    depoisDe = depoisDe,
                    arquivado = arquivado ??= null,
                    vencido = vencido ??= null,
                    turnoDia = turnoDia,
                };

                List<RecadoModeloGet> recados = new GetRecadosService().Service(userId, queries);
                if (recados.Count == 0) {
                    return Ok(new {
                        mensagem = "Não há nenhum recado para mostrar.",
                    });
                }
                
                return Ok(new {
                    mensagem = "Seus recados!",
                    Recados = recados
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }

        [Route("/AtualizarStatusArquivado")]
        [HttpPut]
        [Authorize]
        public IActionResult AtualizarStatusArquivado([FromQuery] string recadoId, [FromBody] RecadoArquivadoStatusBruto statusArquivadoObjBruto)
        {
            try {
                string userId = User.Claims.First(i => i.Type == "Id").Value;
           
                statusArquivadoObjBruto.StatusArquivado ??= new RecadoArquivadoStatusBruto(){StatusArquivado = ""}.StatusArquivado;

                new AtualizaStatusArquivadoRecadoService().Service(userId, recadoId, statusArquivadoObjBruto.StatusArquivado.ToString().ToLower());
                
                return Ok(new {
                    mensagem = statusArquivadoObjBruto.StatusArquivado.ToString().ToLower() == "true" ? "Recado arquivado!" : "Recado desarquivado!"
                });
            } catch (ErroHTTP erro) {
                return new CriaErroHTTP().MandarResposta(erro);
            }
        }
    }
}