using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public class Materiel_DAL
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Categorie { get; set; }
        public string Numero { get; set; }
        public bool EstStocke { get; set; }
        public int? NbUtilisation { get; set; }
        public int? NbMaxUtilisation { get; set; }
        public DateTime? DateExpiration { get; set; }
        public DateTime? DateControle { get; set; }

        public Materiel_DAL(int id, string denomination, string categorie, string numero, bool estStocke, int? nbUtilisation, int? nbMaxUtilisation, DateTime? dateExpiration, DateTime? dateControle)
        {
            Id = id;
            Denomination = denomination;
            Categorie = categorie;
            Numero = numero;
            EstStocke = estStocke;
            NbUtilisation = nbUtilisation;
            NbMaxUtilisation = nbMaxUtilisation;
            DateExpiration = dateExpiration;
            DateControle = dateControle;
        }
    }
}