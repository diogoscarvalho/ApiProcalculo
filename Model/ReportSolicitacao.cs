using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class ReportSolicitacao
    {
        public List<CalculoReport> Calculos { get; set; }

        public ReportSolicitacao()
        {
            Calculos = new List<CalculoReport>();;
        }
    }
}
