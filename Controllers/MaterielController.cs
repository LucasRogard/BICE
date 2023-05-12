using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterielController : Controller
    {
        private IMateriel_SRV service;

        public MaterielController(IMateriel_SRV iMateriel_SRV)
        {
            service = iMateriel_SRV;
        }

        [HttpGet]
        [Route("/MaterielGetById")]
        public Materiel_DTO GetByIdMateriel(int id)
        {
            return service.GetById(id);
        }

        [HttpGet]
        [Route("/MaterielGetAll")]
        public List<Materiel_DTO> GetAll()
        {
            return service.GetAll().ToList();
        }

        [HttpGet]
        [Route("/MaterielGetByNumero")]
        public Materiel_DTO GetByNumeroMateriel(string numero)
        {
            return service.GetByNumero(numero);
        }

        [HttpPost]
        [Route("/MaterielAjouter")]
        public Materiel_DTO AjouterMateriel(Materiel_DTO materiel)
        {
            return service.Ajouter(materiel);
        }

        [HttpPost]
        [Route("/MaterielModifier")]
        public Materiel_DTO ModifierMateriel(Materiel_DTO materiel)
        {
            return service.Modifier(materiel);
        }

        [HttpPost]
        [Route("/MaterielDelete")]
        public void DeleteMateriel(Materiel_DTO materiel)
        {
            service.Delete(materiel);
        }
    }
}
