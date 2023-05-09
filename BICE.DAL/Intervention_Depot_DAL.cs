using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.DAL
{
    public class Intervention_Depot_DAL : Depot_DAL<Intervention_DAL>
    {
        public override void Delete(Intervention_DAL i)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"DELETE FROM [dbo].[intervention]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", i.Id));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();
        }

        public override IEnumerable<Intervention_DAL> GetAll()
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,date,description,denomination
                                    FROM [dbo].[intervention]";


            var reader = Commande.ExecuteReader();

            var liste = new List<Intervention_DAL>();

            while (reader.Read())
            {
                liste.Add(new Intervention_DAL(reader.GetInt32(0),
                                    reader.GetSqlDateTime(1),
                                    reader.GetString(2).IsNull ? null : reader.GetString(2),
                                    reader.GetString(3).IsNull ? null : reader.GetString(3)
                ));
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return liste;
        }

        public override Intervention_DAL GetById(int id)
        {
            InitialiserLaConnexionEtLaCommande();

            Commande.CommandText = @"SELECT id,date,description,denomination
                                    FROM [dbo].[intervention]
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", id));

            var reader = Commande.ExecuteReader();

            Intervention_DAL i = null;

            if (reader.Read())
            {
                i = new Intervention_DAL(reader.GetInt32(0),
                                    reader.GetSqlDateTime(1),
                                    reader.GetString(2).IsNull ? null : reader.GetString(2),
                                    reader.GetString(3).IsNull ? null : reader.GetString(3)
                );
            }

            FermerEtDisposerLaConnexionEtLaCommande();

            return i;
        }

        public override Intervention_DAL Insert(Intervention_DAL i)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"INSERT INTO [dbo].[intervention]
                                           ([date]
                                           ,[description]
                                           ,[denomination])
                                     VALUES
                                           (@date
                                           ,@description
                                           ,@denomination";

            Commande.Parameters.Add(new SqlParameter("@date", i.Date));
            Commande.Parameters.Add(new SqlParameter("@description", i.Description));
            Commande.Parameters.Add(new SqlParameter("@denomination", i.Denomination));

            i.Id = Convert.ToInt32((decimal)Commande.ExecuteScalar());

            FermerEtDisposerLaConnexionEtLaCommande();

            return i;
        }


        public override Intervention_DAL Update(Intervention_DAL i)
        {
            InitialiserLaConnexionEtLaCommande();
            Commande.CommandText = @"UPDATE [dbo].[Points]
                                           set [date] = @date
                                           ,[description] = @description
                                           ,[denomination] = @denomination
                                     WHERE id=@id";

            Commande.Parameters.Add(new SqlParameter("@id", i.Id));
            Commande.Parameters.Add(new SqlParameter("@date", i.Date));
            Commande.Parameters.Add(new SqlParameter("@description", i.Description));
            Commande.Parameters.Add(new SqlParameter("@denomination", i.Denomination));
            Commande.ExecuteNonQuery();

            FermerEtDisposerLaConnexionEtLaCommande();

            return i;
        }
    }
}
