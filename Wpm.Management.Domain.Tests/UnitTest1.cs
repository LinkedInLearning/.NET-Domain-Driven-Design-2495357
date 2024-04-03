using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Services;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var id = Guid.NewGuid();
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);

        var pet1 = new Pet(id, "Nina", 10, "Tri-color", SexOfPet.Female, breedId);
        var pet2 = new Pet(id, "Gianni", 13, "Tri-color", SexOfPet.Male, breedId);

        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void Peso_debe_ser_igual()
    {
        var w1 = new Weight(20.5m);
        var w2 = new Weight(20.5m);
        Assert.True(w1 == w2);
    }

    [Fact]
    public void RangoPeso_debe_ser_igual()
    {
        var wr1 = new WeightRange(10.5m, 20.5m);
        var wr2 = new WeightRange(10.5m, 20.5m);
        Assert.True(wr1 == wr2);
    }

    [Fact]
    public void BreedId_debe_ser_valido()
    {
        var breedService = new FakeBreedService();
        var id = breedService.breeds[0].Id;
        var breedId = new BreedId(id, breedService);
        Assert.NotNull(breedId);
    }

    [Fact]
    public void BreedId_no_debe_ser_valido()
    {
        var breedService = new FakeBreedService();
        var id = Guid.NewGuid();
        Assert.Throws<ArgumentException>(() =>
        {
            var breedId = new BreedId(id, breedService);
        });
    }

    [Fact]
    public void WeightClass_debe_ser_underweight()
    {
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Test", 10, "Color", SexOfPet.Male, breedId);
        pet.SetWeight(8.5m, breedService);
        Assert.True(pet.WeightClass == WeightClass.Underweight);
    }

    [Fact]
    public void WeightClass_debe_ser_overweight()
    {
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Test", 10, "Color", SexOfPet.Male, breedId);
        pet.SetWeight(28.5m, breedService);
        Assert.True(pet.WeightClass == WeightClass.Overweight);
    }

    [Fact]
    public void WeightClass_debe_ser_ideal()
    {
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Test", 10, "Color", SexOfPet.Male, breedId);
        pet.SetWeight(12.5m, breedService);
        Assert.True(pet.WeightClass == WeightClass.Ideal);
    }
}