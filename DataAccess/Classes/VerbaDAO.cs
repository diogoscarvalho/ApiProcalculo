using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class VerbaDAO : IVerbaDAO
    {
        public async Task<IList<Model.Verba>> ListarVerbas(int idSolicitacao)
        {
            IList<Model.Verba> verbas = new List<Model.Verba>();
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = "sp_listar_verbas";

                    command.Parameters.Add(new SqlParameter("@id_solicitacao", idSolicitacao));

                    var dataReader = await command.ExecuteReaderAsync();

                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var verba = new Model.Verba();

                            verba.Titulo = dataReader["verba_titulo"].ToString();
                            verba.Subtitulo = dataReader["verba_subtitulo"].ToString();
                            verba.ValorPrincipal = Convert.ToDouble(dataReader["valor_principal"]);
                            verba.ValorTotal = Convert.ToDouble(dataReader["valor_total"]);
                            verba.PerdaRiscoPossivel = Convert.ToDouble(dataReader["risco_possivel"]);
                            verba.PerdaRiscoProvavel = Convert.ToDouble(dataReader["risco_provavel"]);
                            verba.PerdaRiscoRemoto = Convert.ToDouble(dataReader["risco_remoto"]);
                            verba.RiscoNaoAtribuido = Convert.ToDouble(dataReader["risco_nao_atribuido"]);
                            verba.Juros = Convert.ToDouble(dataReader["valor_juros"]);

                            verbas.Add(verba);
                            
                        }
                    }

                }
                return verbas;
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao listar verbas!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return verbas;
            }
        }
    }
}
