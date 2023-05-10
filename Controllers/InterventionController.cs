using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterventionController : Controller
    {
        private IIntervention_SRV service;

        public InterventionController(IIntervention_SRV iIntervention_SRV)
        {
            service = iIntervention_SRV;
        }

        [HttpPost]
        [Route("/Intervention/Ajouter")]
        public Intervention_DTO Ajouter(Intervention_DTO intervention)
        {
            return service.Ajouter(intervention);
        }
    }
}
