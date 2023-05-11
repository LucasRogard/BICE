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
    public class Vehicule_SRV : IVehicule_SRV
          
    {

        #region Champs
        protected IDepot_DAL<Vehicule_DAL> depot_vehicule;
        #endregion

        public Vehicule_SRV(IDepot_DAL<Vehicule_DAL> depot)
        {
            this.depot_vehicule = depot;
        }
        public Vehicule_SRV()
            : this(new Vehicule_Depot_DAL())
        {

        }

        public Vehicule_DTO Ajouter(Vehicule_DTO vehicule)
        {
            var vehicule_DAL = new Vehicule_DAL(
                vehicule.Id,
                vehicule.Denomination,
                vehicule.Immatriculation,
                vehicule.Numero
                );
            depot_vehicule.Insert(vehicule_DAL);
            return vehicule;
        }
        public IEnumerable<Vehicule_DTO> GetAll()
        {
            //retourne une liste de materiel DTO
            return depot_vehicule.GetAll().Select(vehicule_DAL => new Vehicule_DTO()
            {
                Id = vehicule_DAL.Id,
                Denomination = vehicule_DAL.Denomination,
                Immatriculation = vehicule_DAL.Immatriculation,
                Numero = vehicule_DAL.Numero
            });
        }
        public Vehicule_DTO? GetById(int id)
        {
            var Vehicule_DAL = depot_vehicule.GetById(id);
            if (depot_vehicule.GetById(id) == null) 
            { 
                return null; 
            }else
            {
                return new Vehicule_DTO()
                {
                    Id = Vehicule_DAL.Id,
                    Denomination = Vehicule_DAL.Denomination,
                    Immatriculation = Vehicule_DAL.Immatriculation,
                    Numero = Vehicule_DAL.Numero
                };
            }
        }
        public Vehicule_DTO? GetByNumero(string numero)
        {
            var Vehicule_DAL = depot_vehicule.GetByNumero(numero);
            if (depot_vehicule.GetByNumero(numero) == null)
            {
                return null;
            }
            else
            {
                return new Vehicule_DTO()
                {
                    Id = Vehicule_DAL.Id,
                    Denomination = Vehicule_DAL.Denomination,
                    Immatriculation = Vehicule_DAL.Immatriculation,
                    Numero = Vehicule_DAL.Numero
                };
            }
        }

        public Vehicule_DTO Modifier(Vehicule_DTO vehicule)
        {
            var vehicule_DAL = new Vehicule_DAL(
                vehicule.Id,
                vehicule.Denomination,
                vehicule.Immatriculation,
                vehicule.Numero
                );
            depot_vehicule.Update(vehicule_DAL);

            return vehicule;
        }

        public void Delete(Vehicule_DTO vehicule)
        {
            var vehicule_DAL = new Vehicule_DAL(
                vehicule.Id,
                vehicule.Denomination,
                vehicule.Immatriculation,
                vehicule.Numero
                );
            depot_vehicule.Delete(vehicule_DAL);
        }

    }
}
