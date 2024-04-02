namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var id = Guid.NewGuid();

        var pet1 = new Pet() { Id = id };
        var pet2 = new Pet() { Id = id };

        Assert.True(pet1 == pet2);
    }
}