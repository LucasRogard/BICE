using BICE.BLL;
using BICE.DAL;
using BICE.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.SRV
{
    public class Intervention_SRV : IIntervention_SRV
    {

        #region Champs
        protected IDepot_DAL<Intervention_DAL> depot_intervention;
        #endregion

        public Intervention_SRV(IDepot_DAL<Intervention_DAL> depot)
        {
            this.depot_intervention = depot;
        }
        public Intervention_SRV()
            : this(new Intervention_Depot_DAL())
        {

        }

        public Intervention_DTO Ajouter(Intervention_DTO intervention)
        {
            var intervention_DAL = new Intervention_DAL(
                intervention.Id,
                intervention.Date,
                intervention.Description,
                intervention.Denomination
                );
            depot_intervention.Insert(intervention_DAL);
            return intervention;
        }

    }
}
