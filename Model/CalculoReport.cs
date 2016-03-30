using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CalculoReport
    {
        public Calculo Calculo { get;set;}
        public List<Verba> Verbas { get; set; }

        public string UrlReport { get; set; }

        public CalculoReport()
        {
            this.Verbas = new List<Verba>();

        }
    }
}
