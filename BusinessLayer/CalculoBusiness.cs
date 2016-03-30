using DataAccess.Classes;
using Model;
using Model.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CalculoBusiness
    {
        public async Task<List<Model.Calculo>> ListarCalculos(Model.Req.ConsultaCalculosReq paramReq)
        {
             return await new CalculoDAO().ListarCalculos(paramReq);

        }
    }
}
