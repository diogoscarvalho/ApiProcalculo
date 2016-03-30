using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class UsuarioDAO : IUsuarioDAO
    {
        public async Task<Model.Usuario> SelecionarUsuario(string userId, string password)
        {
            try
            {
                using (var conn = DAOFactory.getConnection())
                {
                    var command = DAOFactory.getFactory().CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = conn;

                    command.CommandText = "sp_selecionar_usuario";
                    command.Parameters.Add(new SqlParameter("@user_id", userId));
                    command.Parameters.Add(new SqlParameter("@password", password));

                    using (var dataReader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {

                        if (dataReader.HasRows)
                        {
                            var usuario = new Model.Usuario();
                            while (await dataReader.ReadAsync().ConfigureAwait(false))
                            {
                                usuario.UserId = dataReader["user_id"].ToString();
                                usuario.Password = dataReader["password"].ToString();
                            }

                            return usuario;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao selecionar usuário!",
                    ExceptioMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    DataErro = DateTime.Now
                });

                return null;
            }
        }

        public Task<int> InserirUsuario(Model.Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletarUsuario(Model.Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Model.Usuario>> ListarUsuario()
        {
            throw new NotImplementedException();
        }
    }
}
