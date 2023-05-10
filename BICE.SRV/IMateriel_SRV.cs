using BICE.DTO;

namespace BICE.SRV
{
    public interface IMateriel_SRV
    {
        Materiel_DTO GetById(int id);
        IEnumerable<Materiel_DTO> GetAll();
        Materiel_DTO Ajouter(Materiel_DTO materiel);
        Materiel_DTO Modifier(Materiel_DTO materiel);


    }
}