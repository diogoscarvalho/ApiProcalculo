using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VerbaBusiness
    {
        public async Task<IList<Model.Verba>> ListarVerbas(int idSolicitacao)
        {
            try
            {
                var verbas = await new VerbaDAO().ListarVerbas(idSolicitacao);

                return verbas;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao consultar verbas!");
            }
        }
    }
}
