using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class SolicitacaoCalculoDAO : ISolicitacaoCalculoDAO
    {
        public async Task<Model.Calculo> InserirSolicitacaoCalculo(Model.SolicitacaoDeCalculo model)
        {
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    DbCommand command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = "sp_inserir_solicitacao_calculo";

                    // Parâmetros
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_cliente_macdata", model.IdCliente));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_caso", model.IdCaso));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@nome_solicitante", model.NomeSolicitante));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@email_solicitante", model.EmailSolicitante));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@nome_reclamada", model.NomeReclamada));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@finalidade", model.IdFinalidade));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_fase_calculo", model.IdFaseCalculo));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@data_fase", Convert.ToDateTime(model.DataFase, CultureInfo.GetCultureInfo("pt-BR"))));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@prazo_fatal", Convert.ToDateTime(model.PrazoFatal, CultureInfo.GetCultureInfo("pt-BR"))));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@unidade_reclamada", model.UnidadeReclamada));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@data_distribuicao", Convert.ToDateTime(model.DataDistribuicao, CultureInfo.GetCultureInfo("pt-BR"))));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@numero_processo", model.NumeroProcesso));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@comarca", model.Comarca));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@uf", model.Uf));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@forum", model.Forum));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@vara", model.Vara));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_responsabilidade", model.IdResponsabilidade));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@empresa_terceira", model.EmpresaTerceira));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_analista", model.IdAnalista));

                    var parametroDeSaida = new System.Data.SqlClient.SqlParameter("@id_solicitacao", SqlDbType.Int);
                    parametroDeSaida.Direction = ParameterDirection.Output;
                    
                    command.Parameters.Add(parametroDeSaida);
                    
                    await command.ExecuteNonQueryAsync();

                    var calculo = new Model.Calculo();

                    calculo.IdSolicitacao = (int)command.Parameters["@id_solicitacao"].Value;
                    calculo.IdCliente = model.IdCliente;
                    calculo.IdCaso = model.IdCaso;
                    calculo.IdFaseCalculo = model.IdFaseCalculo;
                    calculo.Status = 0; // Aberto
                    calculo.DataSolicitacao = DateTime.Now.ToShortDateString();

                    return calculo;
                }
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao inserir solicitação de cálculo!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return null;
            }
        }

        public Model.Calculo SelecionarSolicitacaoCalculo(int idSolicitacao)
        {
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    DbCommand command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = "sp_selecionar_solicitacao_calculo";

                    var dataReader = command.ExecuteReader();

                    Model.Calculo calculo = null;

                    if(dataReader.HasRows && dataReader.Read())
                    {
                        calculo = new Model.Calculo();

                        calculo.IdSolicitacao = (int)dataReader["Id_solicitacao"];
                        calculo.IdCliente = (int)dataReader["Id_solicitacao"];
                        calculo.DataSolicitacao = dataReader["dt_solicitacao"].ToString();
                        calculo.Status = (int)command.Parameters["@Status"].Value;
                    }

                    conn.Close();

                    return calculo;
                }
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao selecionar Solicitação de cálculo!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });

                return null;
            }
        }
    }
}
