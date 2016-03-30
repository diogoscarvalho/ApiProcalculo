using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UsuarioBusiness
    {
        public async Task<Model.Usuario> SelecionarUsuario(string userId, string password)
        {
            return await new UsuarioDAO().SelecionarUsuario(userId, password).ConfigureAwait(false);
        }
    }
}
