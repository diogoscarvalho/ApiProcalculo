using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class CalculoDAO : ICalculoDAO
    {
        public async Task<List<Model.Calculo>> ListarCalculos(Model.Req.ConsultaCalculosReq calculoParm)
        {   
            List<Model.Calculo> calculos = new List<Model.Calculo>();
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "sp_listar_calculos";

                    command.Parameters.Add(new SqlParameter("@id_solicitacao", calculoParm.IdSolicitacao == null ? 0 : calculoParm.IdSolicitacao));
                    command.Parameters.Add(new SqlParameter("@id_cliente", calculoParm.IdCliente));
                    if (!string.IsNullOrEmpty(calculoParm.DataSolicitacao))
                        command.Parameters.Add(new SqlParameter("@data_solicitacao", Convert.ToDateTime(calculoParm.DataSolicitacao, CultureInfo.GetCultureInfo("pt-BR"))));
                    else
                        command.Parameters.Add(new SqlParameter("@data_solicitacao", System.Data.SqlDbType.Date));

                    command.Parameters.Add(new SqlParameter("@status", calculoParm.Status == null ? 0 : calculoParm.Status));

                    using (var dataReader = await command.ExecuteReaderAsync())
                    {

                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                var calculo = new Model.Calculo();

                                calculo.IdCliente = (int)dataReader["id_cliente"];
                                calculo.IdSolicitacao = (int)dataReader["id_solicitacao"];
                                calculo.DataSolicitacao = Convert.ToDateTime(dataReader["data_solicitacao"]).ToString("dd/MM/yyyy");
                                calculo.Status = (int)dataReader["status"];

                                calculos.Add(calculo);
                            }
                        }
                    }
                }
                return calculos;
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao listar Calculos!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return calculos;
            }
        }

        public async Task<List<Model.Calculo>> ListarCalculos(string idsSolicitacao)
        {
            List<Model.Calculo> calculos = new List<Model.Calculo>();
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "sp_listar_calculos";

                    command.Parameters.Add(new SqlParameter("@idsSolicitacao", idsSolicitacao));

                    var dataReader = await command.ExecuteReaderAsync();

                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var calculo = new Model.Calculo();

                            calculo.IdCliente = (int)dataReader["id_cliente"];
                            calculo.IdSolicitacao = (int)dataReader["id_solicitacao"];
                            calculo.DataSolicitacao = Convert.ToDateTime(dataReader["data_calculo"]).ToShortDateString();
                            calculo.Status = (int)dataReader["status"];

                            calculos.Add(calculo);
                        }
                    }
                }
                return calculos;
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao listar calculos!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return calculos;
            }
        }
    }
}
