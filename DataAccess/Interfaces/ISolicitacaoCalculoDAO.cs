using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.Classes
{
    public interface ISolicitacaoCalculoDAO
    {
        Task<Calculo> InserirSolicitacaoCalculo(SolicitacaoDeCalculo model);
        Calculo SelecionarSolicitacaoCalculo(int idSolicitacao);
        //ReportCalculo PesquisarReport(Calculo model);
    }
}
