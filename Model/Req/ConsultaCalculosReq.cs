using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Req
{
    public class ConsultaCalculosReq
    {
        public int? IdSolicitacao { get; set; }
        [Required]
        public int IdCliente { get; set; }
        public string DataSolicitacao { get; set; }
        public int? Status { get; set; }
    }
}
