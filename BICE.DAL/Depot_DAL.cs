using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public abstract class Depot_DAL<Type_DAL> : IDepot_DAL<Type_DAL>
    {
        public string ChaineDeConnexion { get; set; }
        protected SqlConnection Connexion { get; set; }
        protected SqlCommand Commande { get; set; }

        public Depot_DAL()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile("appsettings.json", false, true).Build();
            ChaineDeConnexion = config.GetSection("ConnectionStrings:default").Value;
        }

        protected void InitialiserLaConnexionEtLaCommande()
        {
            Connexion = new SqlConnection(ChaineDeConnexion);
            Commande = Connexion.CreateCommand();
            Connexion.Open();
        }
        protected void FermerEtDisposerLaConnexionEtLaCommande()
        {
            if (Connexion != null)
            {
                Connexion.Close();
                Connexion.Dispose();
            }
            if (Commande != null)
                Commande.Dispose();
        }


        public abstract void Delete(Type_DAL p);

        public abstract IEnumerable<Type_DAL> GetAll();

        public abstract Type_DAL GetById(int id);

        public abstract Type_DAL Insert(Type_DAL p);

        public abstract Type_DAL Update(Type_DAL p);
    }
}
