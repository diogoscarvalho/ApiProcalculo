using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class ReclamanteDAO : IReclamanteDAO
    {
        public async Task<int> InserirReclamante(Model.Reclamante reclamante)
        {
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();

                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_inserir_reclamante";

                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id_solicitacao_calculo", reclamante.IdSolicitacao));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@cpf", reclamante.Cpf));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@nome", reclamante.Nome));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@paradigma", reclamante.Paradigma));

                    return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao inserir Reclamante",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return -1;
            }
        }

        public IList<Model.Reclamante> ListarReclamantes(string idsSolicitacao)
        {
            List<Model.Reclamante> reclamantes = null;
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();

                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_listar_reclamantes";

                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ids_solicitacao", idsSolicitacao));

                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        reclamantes = new List<Model.Reclamante>();
                        while (dataReader.Read())
                        {
                            var reclamante = new Model.Reclamante();

                            reclamante.IdSolicitacao = (int)dataReader["id_solicitacao_calculo"];
                            reclamante.Cpf = dataReader["cpf"].ToString();
                            reclamante.Nome = dataReader["nome"].ToString();
                            reclamante.Paradigma = (int)dataReader["paradigma"];

                            reclamantes.Add(reclamante);
                        }
                    }

                    return reclamantes;
                }
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog(){
                    Descricao = "Erro ao listar reclamantes",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
                return null;
            }
        }
    }
}
