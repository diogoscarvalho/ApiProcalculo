using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class LogDAO : ILogDAO
    {
        public void GravarLog(Model.ErroLog log)
        {
            using (var conn = DAOFactory.getConnection())
            {
                var command = DAOFactory.getFactory().CreateCommand();
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "sp_gravar_log";

                command.Parameters.Add(new SqlParameter("@descricao_erro", log.Descricao));
                command.Parameters.Add(new SqlParameter("@exception_message", log.ExceptioMessage));
                command.Parameters.Add(new SqlParameter("@stack_trace", log.StackTrace));
                command.Parameters.Add(new SqlParameter("@data_erro", log.DataErro));

                command.ExecuteNonQuery();                
            }
        }

        public Model.ErroLog GetLog(Model.ErroLog log)
        {
            throw new NotImplementedException();
        }
    }
}
