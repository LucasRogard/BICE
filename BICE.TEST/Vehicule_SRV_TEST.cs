using BICE.DAL;
using BICE.SRV;
using BICE.DTO;
using Moq;

public class Vehicule_SRV_test
{
    [Fact]
    public void Vehicule_SRV_GetById()
    {
        var mock = new Mock<IDepot_DAL<Vehicule_DAL>>();
        mock.Setup(d => d.GetById(It.IsAny<int>())).Returns(new Vehicule_DAL(0, "fiat panda", "9CV5F36", "94321128", true));

        var srv = new Vehicule_SRV(mock.Object);

        var result = srv.GetById(1);

        Assert.NotNull(result);
        Assert.IsType<Vehicule_DTO>(result);

        //pas compris
        mock.Verify(depot => depot.GetById(It.IsAny<int>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Vehicule_SRV_Delete()
    {
        var mock = new Mock<IDepot_DAL<Vehicule_DAL>>();
        mock.Setup(d => d.Delete(It.IsAny<Vehicule_DAL>()));

        var srv = new Vehicule_SRV(mock.Object);

        srv.Delete(new Vehicule_DTO());

        mock.Verify(depot => depot.Delete(It.IsAny<Vehicule_DAL>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Vehicule_SRV_Insert()
    {
        var mock = new Mock<IDepot_DAL<Vehicule_DAL>>();
        mock.Setup(d => d.Insert(It.IsAny<Vehicule_DAL>())).Returns(new Vehicule_DAL(0, "fiat panda", "9CV5F36", "94321128", true));

        var srv = new Vehicule_SRV(mock.Object);

        var result = srv.Ajouter(new Vehicule_DTO());

        mock.Verify(depot => depot.Insert(It.IsAny<Vehicule_DAL>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Vehicule_SRV_Update()
    {
        var mock = new Mock<IDepot_DAL<Vehicule_DAL>>();
        mock.Setup(d => d.Update(It.IsAny<Vehicule_DAL>()));

        var srv = new Vehicule_SRV(mock.Object);

        var result = srv.Modifier(new Vehicule_DTO());

        mock.Verify(depot => depot.Update(It.IsAny<Vehicule_DAL>()), Times.AtLeastOnce);
    }
}