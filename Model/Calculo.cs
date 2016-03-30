using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Calculo
    {
        public int IdSolicitacao { get; set; }
        public int IdCaso { get; set; }
        public int IdCliente { get; set; }
        public int IdFaseCalculo { get; set; }
        public string DataSolicitacao { get; set; }
        public string DataAtualizacao { get; set; }
        public string DataProcessamento { get; set; }
        public int Status { get; set; }
    }
}
