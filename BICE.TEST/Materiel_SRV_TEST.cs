using BICE.DAL;
using BICE.SRV;
using BICE.DTO;
using Moq;

public class Materiel_SRV_test
{
    [Fact]
    public void Materiel_SRV_GetById()
    {
        var mock = new Mock<IDepot_DAL<Materiel_DAL>>();
        mock.Setup(d => d.GetById(It.IsAny<int>())).Returns(new Materiel_DAL(0, "Corde", "FEU", "94321128", true, 5, 10, DateTime.Now, DateTime.Now));

        var srv = new Materiel_SRV(mock.Object);

        var result = srv.GetById(1);

        Assert.NotNull(result);
        Assert.IsType<Materiel_DTO>(result);

        //pas compris
        mock.Verify(depot => depot.GetById(It.IsAny<int>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Materiel_SRV_Delete()
    {
        var mock = new Mock<IDepot_DAL<Materiel_DAL>>();
        mock.Setup(d => d.Delete(It.IsAny<Materiel_DAL>()));

        var srv = new Materiel_SRV(mock.Object);

        srv.Delete(new Materiel_DTO());

        mock.Verify(depot => depot.Delete(It.IsAny<Materiel_DAL>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Materiel_SRV_Insert()
    {
        var mock = new Mock<IDepot_DAL<Materiel_DAL>>();
        mock.Setup(d => d.Insert(It.IsAny<Materiel_DAL>())).Returns(new Materiel_DAL(0, "Corde", "FEU", "94321128", true, 5, 10, DateTime.Now, DateTime.Now));

        var srv = new Materiel_SRV(mock.Object);

        var result = srv.Ajouter(new Materiel_DTO());

        mock.Verify(depot => depot.Insert(It.IsAny<Materiel_DAL>()), Times.AtLeastOnce);
    }
    [Fact]
    public void Materiel_SRV_Update()
    {
        var mock = new Mock<IDepot_DAL<Materiel_DAL>>();
        mock.Setup(d => d.Update(It.IsAny<Materiel_DAL>()));

        var srv = new Materiel_SRV(mock.Object);

        var result = srv.Modifier(new Materiel_DTO());

        mock.Verify(depot => depot.Update(It.IsAny<Materiel_DAL>()), Times.AtLeastOnce);
    }
}