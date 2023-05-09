using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public class Intervention_DAL
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Denomination { get; set; }

        public Intervention_DAL(int id, DateTime date, string? description, string? denomination)
        {
            Id = id;
            Date = date;
            Description = description;
            Denomination = denomination;
        }
    }
}
