using BusinessLayer;
using Model;
using service.procalculo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace service.procalculo.Controllers
{
    public class SolicitacaoCalculoController : ApiController
    {
        /// <summary>
        /// Envia uma lista de solicitações de Cálculo
        /// </summary>
        /// <param name="solicitacaoReq">Lista de Objetos de request contendo as informações necessárias para inclusão de solições de cálculo.</param>
        /// <returns>HttpMessage contendo o status da requisição mais o objeto de retorno(Pode ser as informações que acabaram de ser inseridas ou mensagem de erro.</returns>
        [AcceptVerbs("POST")]
        public async Task<HttpResponseMessage> Post(List<Model.SolicitacaoDeCalculo> solicitacaoReq)
        {
            try
            {
                if (solicitacaoReq == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                        new HttpError("Verifique o formato dos dados enviados!"));
                }
                else
                {
                    List<Model.Calculo> retornoList = new List<Model.Calculo>();

                    if (ModelState.IsValid)
                    {
                        foreach (var calculo in solicitacaoReq)
                        {
                            var Calculo = await new SolicitacaoCalculoBusiness().InserirSolicitacaoCalculo(calculo);
                            retornoList.Add(Calculo);
                        }
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                            new HttpError("Verifique os dados enviados!"));

                    if (retornoList.Count() > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, retornoList);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent,
                                        new HttpError("Erro ao inserir solicitação de Calculo!"));
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                        new HttpError("Erro ao inserir solicitação de Calculo!"));
            }
        }

        ///// <summary>
        ///// Envia uma solicitação de Cálculo
        ///// </summary>
        ///// <param name="solicitacaoReq">Objeto de request contendo as informações necessárias para inclusão de solições de cálculo.</param>
        ///// <returns>HttpMessage contendo o status da requisição mais o objeto de retorno(Pode ser as informações que acabaram de ser inseridas ou mensagem de erro.</returns>
        //[AcceptVerbs("POST")]
        //public async Task<HttpResponseMessage> Post(Model.SolicitacaoDeCalculo solicitacaoReq)
        //{
        //    if (solicitacaoReq == null)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //                                            new HttpError("Verifique o formato dos dados enviados!"));
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var calculo = await new SolicitacaoCalculoBusiness().InserirSolicitacaoCalculo(solicitacaoReq);

        //            if (calculo != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, calculo);
        //            else
        //                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
        //                                new HttpError("Erro ao inserir solicitação de Calculo!"));
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //                                                new HttpError("Verifique os dados enviados!"));

        //    }
        //}
    }
}
