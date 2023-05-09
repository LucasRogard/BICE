using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public class Materiel_Depot_DAL : Depot_DAL<Materiel_DAL>
    {
        public override void Delete(Materiel_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"DELETE FROM [dbo].[materiel]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", v.Id));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();
        }

        public override IEnumerable<Materiel_DAL> GetAll()
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,denomination,categorie,numero,est_stocke,vehicule_id,nb_utilisations,nb_max_utilisations,date_expiration,date_prochain_controle
                                    FROM [dbo].[materiel]";


            var reader = Commande.ExecuteReader();

            var liste = new List<Materiel_DAL>();

            while (reader.Read())
            {
                liste.Add(new Materiel_DAL(reader.GetInt32(0), //Id
                                        reader.GetString(1), //Denomination
                                        reader.GetString(2), //Categorie
                                        reader.GetString(3), //Numero
                                        reader.GetBoolean(4), //EstStocke
                                        reader.GetInt32(5).IsNull ? null : reader.GetInt32(5), //VehculeId
                                        reader.GetInt32(6).IsNull ? null : reader.GetInt32(6), //NbUtilisations
                                        reader.GetInt32(7).IsNull ? null : reader.GetInt32(7), //NbMaxUtilisations
                                        reader.GetSqlDateTime(8).IsNull ? null : reader.GetDateTime(8), //DateExpiraion
                                        reader.GetSqlDateTime(9).IsNull ? null : reader.GetDateTime(9) //DateProchainControle
                ));
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return liste;
        }

        public override Materiel_DAL GetById(int id)
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,denomination,categorie,numero,est_stocke,vehicule_id,nb_utilisations,nb_max_utilisations,date_expiration,date_prochain_controle
                                    FROM [dbo].[materiel]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", id));

            var reader = Commande.ExecuteReader();

            Materiel_DAL v = null;

            if (reader.Read()) //j'ai trouvé une ligne
            {
                v = new Materiel_DAL(reader.GetInt32(0), //Id
                                    reader.GetString(1), //Denomination
                                    reader.GetString(2), //Categorie
                                    reader.GetString(3), //Numero
                                    reader.GetBoolean(4), //EstStocke
                                    reader.GetInt32(5).IsNull ? null : reader.GetInt32(5), //VehculeId
                                    reader.GetInt32(6).IsNull ? null : reader.GetInt32(6), //NbUtilisations
                                    reader.GetInt32(7).IsNull ? null : reader.GetInt32(7), //NbMaxUtilisations
                                    reader.GetSqlDateTime(8).IsNull ? null : reader.GetDateTime(8), //DateExpiraion
                                    reader.GetSqlDateTime(9).IsNull ? null : reader.GetDateTime(9) //DateProchainControle
                );
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }

        public override Materiel_DAL Insert(Materiel_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"INSERT INTO [dbo].[materiel]
                                           ([denomination]
                                           ,[categorie]
                                           ,[numero]
                                           ,[est_stocke]
                                           ,[vehicule_id]
                                           ,[nb_utilisations]
                                           ,[nb_max_utilisations]
                                           ,[date_expiration]
                                           ,[date_prochain_controle])
                                     VALUES
                                           (@denomination
                                           ,@categorie
                                           ,@numero
                                           ,@estStocke
                                           ,@vehiculeId
                                           ,@nbUtilisation
                                           ,@nbMaxUtilisation
                                           ,@dateExpiration
                                           ,@dateControle";

            Commande.Parameters.Add(new SqlParameter("@denomination", v.Denomination));
            Commande.Parameters.Add(new SqlParameter("@categorie", v.Categorie));
            Commande.Parameters.Add(new SqlParameter("@numero", v.Numero));
            Commande.Parameters.Add(new SqlParameter("@estStocke", v.EstStocke));
            Commande.Parameters.Add(new SqlParameter("@vehiculeId", v.VehiculeId));
            Commande.Parameters.Add(new SqlParameter("@nbUtilisation", v.NbUtilisation));
            Commande.Parameters.Add(new SqlParameter("nbMaxUtilisation", v.NbMaxUtilisation));
            Commande.Parameters.Add(new SqlParameter("dateExpiration", v.DateExpiration));
            Commande.Parameters.Add(new SqlParameter("dateControle", v.DateControle));

            v.Id = Convert.ToInt32((decimal)Commande.ExecuteScalar());

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }


        public override Materiel_DAL Update(Materiel_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"UPDATE [dbo].[materiel]
                                           set [denomination] = @denomination
                                           ,[categorie] = @categorie
                                           ,[numero] = @numero
                                           ,[est_stocke] = @estStocke
                                           ,[vehicule_id] = @vehiculeId
                                           ,[nb_utilisations] = @nbUtilisation
                                           ,[nb_max_utilisations] = @nbMaxUtilisation
                                           ,[date_expiration] = @dateExpiration
                                           ,[date_prochain_controle] = @dateControle
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", v.Id));
            Commande.Parameters.Add(new SqlParameter("@denomination", v.Denomination));
            Commande.Parameters.Add(new SqlParameter("@categorie", v.Categorie));
            Commande.Parameters.Add(new SqlParameter("@numero", v.Numero));
            Commande.Parameters.Add(new SqlParameter("@estStocke", v.EstStocke));
            Commande.Parameters.Add(new SqlParameter("@vehiculeId", v.VehiculeId));
            Commande.Parameters.Add(new SqlParameter("@nbUtilisation", v.NbUtilisation));
            Commande.Parameters.Add(new SqlParameter("nbMaxUtilisation", v.NbMaxUtilisation));
            Commande.Parameters.Add(new SqlParameter("dateExpiration", v.DateExpiration));
            Commande.Parameters.Add(new SqlParameter("dateControle", v.DateControle));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }
    }
}
