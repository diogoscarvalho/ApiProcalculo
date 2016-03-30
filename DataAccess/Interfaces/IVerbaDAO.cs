using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IVerbaDAO
    {
        Task<IList<Model.Verba>> ListarVerbas(int idSolicitacao);
    }
}
