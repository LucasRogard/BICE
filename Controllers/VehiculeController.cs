using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiculeController : Controller
    {
        private IVehicule_SRV service;

        public VehiculeController(IVehicule_SRV iVehicule_SRV)
        {
            service = iVehicule_SRV;
        }

        [HttpGet]
        [Route("/Vehicule/One")]
        public Vehicule_DTO GetById(int id)
        {
            return service.GetById(id);
        }

        [HttpGet]
        [Route("/Vehicule/All")]
        public List<Vehicule_DTO> GetAll()
        {
            return (List<Vehicule_DTO>)service.GetAll();
        }

        [HttpPost]
        [Route("/Vehicule/Ajouter")]
        public Vehicule_DTO Ajouter(Vehicule_DTO vehicule)
        {
            return service.Ajouter(vehicule);
        }
    }
}
