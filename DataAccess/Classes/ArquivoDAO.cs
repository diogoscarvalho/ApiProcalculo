using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class ArquivoDAO: IArquivoDAO
    {
        public async Task<int> Gravar(int idSolicitacao, Model.Arquivo arquivo)
        {
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "sp_inserir_arquivo_solicitacao";

                    command.Parameters.Add(new SqlParameter("@nome_arquivo", arquivo.FileName));
                    command.Parameters.Add(new SqlParameter("@arquivo_base64", arquivo.File));
                    command.Parameters.Add(new SqlParameter("@id_tb_solicitacao_calculo", idSolicitacao));

                    return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao grava arquivo",
                    StackTrace = ex.StackTrace,
                    ExceptioMessage = ex.Message,
                    DataErro = DateTime.Now
                });
                return -1;
            }
        }

        public List<Model.Arquivo> Listar(int idSolicitacao)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idSolicitacao)
        {
            throw new NotImplementedException();
        }
    }
}
