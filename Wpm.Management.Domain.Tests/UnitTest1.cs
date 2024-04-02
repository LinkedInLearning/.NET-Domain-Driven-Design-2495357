namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var id = Guid.NewGuid();

        var pet1 = new Pet(id, "Nina", 10, "Tri-color", new Weight(20.5m), SexOfPet.Female);
        var pet2 = new Pet(id, "Gianni", 13, "Tri-color", new Weight(16.5m), SexOfPet.Male);

        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void Peso_debe_ser_igual()
    {
        var w1 = new Weight(20.5m);
        var w2 = new Weight(20.5m);
        Assert.True(w1 == w2);
    }
}