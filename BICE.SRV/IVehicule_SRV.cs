﻿using BICE.DTO;

namespace BICE.SRV
{
    public interface IVehicule_SRV
    {
        Vehicule_DTO GetById(int id);
        IEnumerable<Vehicule_DTO> GetAll();
        Vehicule_DTO Ajouter(Vehicule_DTO forme);
        Vehicule_DTO Modifier(Vehicule_DTO forme);


    }
}