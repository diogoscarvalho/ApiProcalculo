using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Classe para representar o objeto SolicitacaoDeCalculo
    /// </summary>
    [Serializable]
    public sealed class SolicitacaoDeCalculo
    {
        [Required]
        public string NomeSolicitante { get; set; }
        [EmailAddress][Required]
        public string EmailSolicitante { get; set; }
        [Required]
        public string NomeReclamada { get; set; }
        [Required]
        public string IdFinalidade { get; set; }
        [Required]
        public string DataFase { get; set; }
        public string PrazoFatal { get; set; }
        public string UnidadeReclamada { get; set; }
        public string DataDistribuicao { get; set; }
        [Required]
        public string NumeroProcesso { get; set; }
        public string Comarca { get; set; }
        public string Uf { get; set; }
        public string Forum { get; set; }
        public string Vara { get; set; }
        public int? IdResponsabilidade { get; set; }
        public string EmpresaTerceira { get; set; }
        [Required]
        public int IdAnalista { get; set; }
        [Required]
        public int IdCaso { get; set; }
        [Required]
        public int IdCliente { get; set; }
        public int IdFaseCalculo { get; set; }
        public List<Arquivo> Arquivos { get; set; }
        public List<Reclamante> Reclamantes { get; set; }
        
    }
}