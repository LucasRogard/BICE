using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public class Vehicule_Depot_DAL : Depot_DAL<Vehicule_DAL>
    {
        public override void Delete(Vehicule_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"DELETE FROM [dbo].[vehicules]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", v.Id));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();
        }

        public override IEnumerable<Vehicule_DAL> GetAll()
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,denomination,immatriculation,numero
                                    FROM [dbo].[vehicules]";


            var reader = Commande.ExecuteReader();

            var liste = new List<Vehicule_DAL>();

            while (reader.Read())
            {
                liste.Add(new Vehicule_DAL(reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetSqlString(2).IsNull ? null : reader.GetString(2),
                                    reader.GetString(3)
                ));
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return liste;
        }

        public override Vehicule_DAL GetById(int id)
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,denomination,immatriculation,numero
                                    FROM [dbo].[vehicules]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", id));

            var reader = Commande.ExecuteReader();

            Vehicule_DAL v = null;

            if (reader.Read()) //j'ai trouvé une ligne
            {
                v = new Vehicule_DAL(reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetSqlString(2).IsNull ? null : reader.GetString(2),
                                    reader.GetString(3));
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }

        public override Vehicule_DAL GetByNumero(string numero)
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,denomination,immatriculation,numero
                                    FROM [dbo].[vehicules]
                                     WHERE numero=@numero";

            Commande.Parameters.Add(new SqlParameter("@numero", numero));

            var reader = Commande.ExecuteReader();

            Vehicule_DAL v = null;

            if (reader.Read()) //j'ai trouvé une ligne
            {
                v = new Vehicule_DAL(reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetSqlString(2).IsNull ? null : reader.GetString(2),
                                    reader.GetString(3));
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }

        public override Vehicule_DAL Insert(Vehicule_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"INSERT INTO [dbo].[vehicules]
                                           ([denomination]
                                           ,[immatriculation]
                                           ,[numero])
                                     VALUES
                                           (@denomination
                                           ,@immatriculation
                                           ,@numero); select scope_identity()";

            Commande.Parameters.Add(new SqlParameter("@denomination", v.Denomination));
            Commande.Parameters.Add(new SqlParameter("@immatriculation", v.Immatriculation));
            Commande.Parameters.Add(new SqlParameter("@numero", v.Numero));

            v.Id = Convert.ToInt32((decimal)Commande.ExecuteScalar());

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }


        public override Vehicule_DAL Update(Vehicule_DAL v)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"UPDATE [dbo].[vehicules] set [denomination] = @denomination
                                        ,[immatriculation] = @immatriculation
                                        ,[numero] = @numero
                                    WHERE
                                        (id = @id)";

            Commande.Parameters.Add(new SqlParameter("@id", v.Id));
            Commande.Parameters.Add(new SqlParameter("@denomination", v.Denomination));
            Commande.Parameters.Add(new SqlParameter("@immatriculation", v.Immatriculation));
            Commande.Parameters.Add(new SqlParameter("@numero", v.Numero));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();

            return v;
        }
    }
}
