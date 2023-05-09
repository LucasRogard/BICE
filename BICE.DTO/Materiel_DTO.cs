using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DTO
{
    public class Materiel_DTO
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
    }
}
