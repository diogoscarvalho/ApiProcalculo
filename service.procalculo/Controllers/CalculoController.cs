using Model;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Model.Req;
using service.procalculo.Helpers;

namespace service.procalculo.Controllers
{
    public class CalculoController : ApiController
    {
        /// <summary>
        /// Consulta um cálculo para acompanhamento de status
        /// </summary>
        /// <param name="consultaCalculosReq">Objeto contendo as informações do cálculo passíveis de consulta</param>
        /// <returns>Um objeto json com informações do calculo</returns>
        [AcceptVerbs("GET")]
        public async Task<HttpResponseMessage> Search([FromUri]Model.Req.ConsultaCalculosReq consultaCalculosReq)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Model.Calculo> retornoList = new List<Calculo>();

                    retornoList = await new CalculoBusiness().ListarCalculos(consultaCalculosReq);

                    if (retornoList.Count() > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, retornoList);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cálculos não encontrados!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dados enviados em formato incorreto ou o código do cliente não foi informado.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao consultar Cálculo.");
            }

        }

        /// <summary>
        /// Consulta dados de um cálculo já concluído
        /// </summary>
        /// <param name="idSolicitacao">Código do cálculo</param>
        /// <returns>Objeto json com informações de cálculo, verbas e Report</returns>
        [AcceptVerbs("GET")]
        public async Task<HttpResponseMessage> Report([FromUri]int[] idSolicitacao)
        {
            List<Model.ReportSolicitacao> reportList = null;
            try
            {
                reportList = await new ReportCalculoBusiness().ConsultarReport(idSolicitacao);

                if (reportList.Count() > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, reportList);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Relatório não encontrado!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao pesquisar relatório!");
            }
        }

    }
}