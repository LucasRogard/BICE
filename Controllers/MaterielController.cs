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
        [Route("/Materiel/One")]
        public Materiel_DTO GetById(int id)
        {
            return service.GetById(id);
        }

        [HttpPost]
        [Route("/Materiel/Ajouter")]
        public Materiel_DTO Ajouter(Materiel_DTO materiel)
        {
            return service.Ajouter(materiel);
        }
    }
}
