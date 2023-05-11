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
        [Route("/VehiculeGetById")]
        public Vehicule_DTO GetByIdVehicule(int id)
        {
            return service.GetById(id);
        }

        [HttpGet]
        [Route("/VehiculeGetByNumero")]
        public Vehicule_DTO GetByNumeroVehicule(string numero)
        {
            return service.GetByNumero(numero);
        }

        [HttpGet]
        [Route("/VehiculeGetAll")]
        public List<Vehicule_DTO> GetAllVehicule()
        {
            return (List<Vehicule_DTO>)service.GetAll();
        }

        [HttpPost]
        [Route("/VehiculeAjouter")]
        public Vehicule_DTO AjouterVehicule(Vehicule_DTO vehicule)
        {
            return service.Ajouter(vehicule);
        }

        [HttpPost]
        [Route("/VehiculeModifier")]
        public Vehicule_DTO ModifierVehicule(Vehicule_DTO vehicule)
        {
            return service.Modifier(vehicule);
        }

        [HttpPost]
        [Route("/VehiculeDelete")]
        public void DeleteVehicule(Vehicule_DTO vehicule)
        {
            service.Delete(vehicule);
        }
    }
}
