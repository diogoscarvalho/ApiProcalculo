using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class ReportCalculoDAO : IReportCalculoDAO
    {
        public async Task<IList<Model.CalculoReport>> ConsultarReport(int idSolicitacao)
        {
            IList<Model.CalculoReport> reportList = null;
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    DbCommand command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "sp_consulta_reports";

                    command.Parameters.Add(new SqlParameter("@id_solicitacao", idSolicitacao));

                    var dataReader = await command.ExecuteReaderAsync();

                    if (dataReader.HasRows)
                    {
                        reportList = new List<Model.CalculoReport>();
                        var report = new Model.CalculoReport();
                        while (await dataReader.ReadAsync())
                        {
                            var calculo = new Model.Calculo();

                            calculo.IdSolicitacao = (int)dataReader["Id_solicitacao"];
                            calculo.IdCliente = (int)dataReader["Id_solicitacao"];
                            calculo.Status = (int)dataReader["status"];
                            calculo.DataSolicitacao = dataReader["data_solicitacao"].ToString() != "" ? Convert.ToDateTime(dataReader["data_solicitacao"]).ToString("dd/MM/yyyy") : "";
                            calculo.DataAtualizacao = dataReader["data_atualizacao"].ToString() != "" ? Convert.ToDateTime(dataReader["data_atualizacao"]).ToString("dd/MM/yyyy") : "";
                            calculo.DataProcessamento = dataReader["data_processamento"].ToString() != "" ? Convert.ToDateTime(dataReader["data_processamento"]).ToString("dd/MM/yyyy") : "";

                            report.UrlReport = dataReader["report_url"].ToString();

                            report.Calculo = calculo;

                            reportList.Add(report);
                        }
                    }

                    return reportList;
                }
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao consultar Report",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return null;
            }
        }
    }
}
