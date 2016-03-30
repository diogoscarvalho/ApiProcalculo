﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public interface IReportCalculoDAO
    {
        Task<IList<CalculoReport>> ConsultarReport(int idsSolicitacao);
    }
}
