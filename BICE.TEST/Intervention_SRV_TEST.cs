using BICE.DAL;
using BICE.SRV;
using BICE.DTO;
using Moq;

public class Intervention_SRV_test
{
    [Fact]
    public void Intervention_SRV_Insert()
    {
        var mock = new Mock<IDepot_DAL<Intervention_DAL>>();
        mock.Setup(d => d.Insert(It.IsAny<Intervention_DAL>())).Returns(new Intervention_DAL(0, DateTime.Now, "feu dans une maison", "FEU"));

        var srv = new Intervention_SRV(mock.Object);

        var result = srv.Ajouter(new Intervention_DTO());

        mock.Verify(depot => depot.Insert(It.IsAny<Intervention_DAL>()), Times.AtLeastOnce);
    }
}