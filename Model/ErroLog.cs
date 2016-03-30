using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ErroLog
    {
        public int IdLog { get; set; }
        public string Descricao { get; set; }
        public string ExceptioMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime DataErro { get; set; }

    }
}
