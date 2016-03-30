using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUsuarioDAO
    {
        Task<Model.Usuario> SelecionarUsuario(string userId, string password);
        Task<int> InserirUsuario(Model.Usuario usuario);
        Task<int> DeletarUsuario(Model.Usuario usuario);
        Task<List<Model.Usuario>> ListarUsuario();
    }
}
