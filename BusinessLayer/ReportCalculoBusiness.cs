using DataAccess.Classes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLayer
{
    public class ReportCalculoBusiness
    {
        public async Task<List<Model.ReportSolicitacao>> ConsultarReport(int[] idsCalculo)
        {
            List<Model.ReportSolicitacao> reportList = new List<Model.ReportSolicitacao>();

            for (int i = 0; i < idsCalculo.Count(); i++)
            {
                // Lista os reports(calculos) para um calculo(podem existir n reports para cada calculo dependendo da quantidade de reclamantes)
                var listCalculoReport = await new ReportCalculoDAO().ConsultarReport(idsCalculo[i]);

                if (listCalculoReport != null)
                {
                    // Tras as verbas para cada relatório(calculo)
                    foreach (var calculoReport in listCalculoReport)
                    {
                        var verbas = await new VerbaDAO().ListarVerbas(calculoReport.Calculo.IdSolicitacao);
                        calculoReport.Verbas.AddRange(verbas);
                    }
                    // Insere a lista de relatório do objeto pricipal, onde podem existirn relatório para n solicitacões.
                    var reportSolicitacao = new ReportSolicitacao();
                    reportSolicitacao.Calculos.AddRange(listCalculoReport);

                    reportList.Add(reportSolicitacao);
                }
            }
            return reportList;
        }
    }
}
