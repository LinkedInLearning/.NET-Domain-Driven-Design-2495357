namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var id = Guid.NewGuid();

        var pet1 = new Pet(id) { Name = "Nina", Age = 10 };
        var pet2 = new Pet(id) { Name = "Gianni", Age = 13 };

        Assert.True(pet1 == pet2);
    }
}