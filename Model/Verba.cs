using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Verba
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public double ValorPrincipal { get; set; }
        public double PerdaRiscoProvavel { get; set; }
        public double PerdaRiscoPossivel { get; set; }
        public double PerdaRiscoRemoto { get; set; }
        public double RiscoNaoAtribuido { get; set; }
        public double Juros { get; set; }
        public double ValorTotal { get; set; }
    }
}
