using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess.Classes;

namespace BusinessLayer
{
    public class SolicitacaoCalculoBusiness
    {
        /// <summary>
        /// Insere uma solicitacao de Calculo e seus respectivos reclamantes
        /// </summary>
        /// <param name="solicitacaoCalculo">Objeto que representa a solicitação de cálculo</param>
        /// <returns></returns>
        public async Task<Model.Calculo> InserirSolicitacaoCalculo(Model.SolicitacaoDeCalculo solicitacaoCalculo)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var calculo = await new SolicitacaoCalculoDAO().InserirSolicitacaoCalculo(solicitacaoCalculo).ConfigureAwait(false);

                    for (int i = 0; i < solicitacaoCalculo.Reclamantes.Count; i++)
                    {
                        solicitacaoCalculo.Reclamantes[i].IdSolicitacao = calculo.IdSolicitacao;
                        await new ReclamanteDAO().InserirReclamante(solicitacaoCalculo.Reclamantes[i]).ConfigureAwait(false);
                    }

                    for (int i = 0; i < solicitacaoCalculo.Arquivos.Count; i++)
                    {
                        await new ArquivoDAO().Gravar(calculo.IdSolicitacao, solicitacaoCalculo.Arquivos[i]).ConfigureAwait(false);

                    }

                    scope.Complete();

                    return calculo;
                }
            }
            catch (Exception ex)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao inserir solicitação de cálculo!",
                    ExceptioMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    DataErro = DateTime.Now
                });

                return null;
            }
        }
    }
}
