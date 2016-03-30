using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface ILogDAO
    {
        void GravarLog(Model.ErroLog log);
        Model.ErroLog GetLog(Model.ErroLog log);
    }
}
