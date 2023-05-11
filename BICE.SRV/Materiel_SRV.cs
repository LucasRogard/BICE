using BICE.BLL;
using BICE.DAL;
using BICE.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BICE.SRV
{
    public class Materiel_SRV : IMateriel_SRV

    {

        #region Champs
        protected IDepot_DAL<Materiel_DAL> depot_materiel;
        #endregion

        public Materiel_SRV(IDepot_DAL<Materiel_DAL> depot)
        {
            this.depot_materiel = depot;
        }
        public Materiel_SRV()
            : this(new Materiel_Depot_DAL())
        {

        }

        public Materiel_DTO Ajouter(Materiel_DTO materiel)
        {
            var materiel_DAL = new Materiel_DAL(
                materiel.Id,
                materiel.Denomination,
                materiel.Categorie,
                materiel.Numero,
                materiel.EstStocke,
                materiel.NbUtilisation,
                materiel.NbMaxUtilisation,
                materiel.DateExpiration,
                materiel.DateControle
                );
            depot_materiel.Insert(materiel_DAL);
            return materiel;
        }
        public IEnumerable<Materiel_DTO> GetAll()
        {
            //retourne une liste de materiel DTO
            return depot_materiel.GetAll().Select(materiel_DAL => new Materiel_DTO()
            {
                Id = materiel_DAL.Id,
                Denomination = materiel_DAL.Denomination,
                Categorie = materiel_DAL.Categorie,
                Numero = materiel_DAL.Numero,
                EstStocke = materiel_DAL.EstStocke,
                NbUtilisation = materiel_DAL.NbUtilisation,
                NbMaxUtilisation = materiel_DAL.NbMaxUtilisation,
                DateExpiration = materiel_DAL.DateExpiration,
                DateControle = materiel_DAL.DateControle
            });
        }
        public Materiel_DTO? GetById(int id)
        {
            var Materiel_DAL = depot_materiel.GetById(id);
            if (depot_materiel.GetById(id) == null) { return null; }

            return new Materiel_DTO()
            {
                Id = Materiel_DAL.Id,
                Denomination = Materiel_DAL.Denomination,
                Categorie = Materiel_DAL.Categorie,
                Numero = Materiel_DAL.Numero,
                EstStocke = Materiel_DAL.EstStocke,
                NbUtilisation = Materiel_DAL.NbUtilisation,
                NbMaxUtilisation = Materiel_DAL.NbMaxUtilisation,
                DateExpiration = Materiel_DAL.DateExpiration,
                DateControle = Materiel_DAL.DateControle
            };
        }
        public Materiel_DTO Modifier(Materiel_DTO materiel)
        {
            var materiel_DAL = new Materiel_DAL(
                materiel.Id,
                materiel.Denomination,
                materiel.Categorie,
                materiel.Numero,
                materiel.EstStocke,
                materiel.NbUtilisation,
                materiel.NbMaxUtilisation,
                materiel.DateExpiration,
                materiel.DateControle
                );
            depot_materiel.Update(materiel_DAL);

            return materiel;
        }

    }
}
