using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class ReclamanteBusiness
    {
        public IList<Model.Reclamante> ListarReclamantes(int[] idsSolicitacao)
        {
            var reclamantes = new ReclamanteDAO().ListarReclamantes(string.Join(",",idsSolicitacao));

            return reclamantes;
        }
    }
}
