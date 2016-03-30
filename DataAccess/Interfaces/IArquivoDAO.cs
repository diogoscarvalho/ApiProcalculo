using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    interface IArquivoDAO
    {
        Task<int> Gravar(int idSolicitacao, Model.Arquivo arquivo);
        List<Model.Arquivo> Listar(int idSolicitacao);
        void Deletar(int idSolicitacao);
    }
}
