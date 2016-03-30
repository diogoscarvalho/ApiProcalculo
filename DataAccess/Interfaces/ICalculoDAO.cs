using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    interface ICalculoDAO
    {
        Task<List<Model.Calculo>> ListarCalculos(Model.Req.ConsultaCalculosReq calculo);

        Task<List<Model.Calculo>> ListarCalculos(string idsSolicitacao);
    }
}
