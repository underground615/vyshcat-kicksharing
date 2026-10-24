using Microsoft.Extensions.DependencyInjection;
using VyshCat.Models;
using VyshCat.Services;
using Xunit;

namespace VyshCat.Tests;

public sealed class FleetServiceTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InspectionResult(bool decision) {
        var fake = new FakeServiceCenter(decision);
        var services = new ServiceCollection();
        services.AddSingleton<IServiceCenter>(fake);
        services.AddSingleton<FleetService>();
        using var provider = services.BuildServiceProvider();
        var fleet = provider.GetRequiredService<FleetService>();
        var vehicle = new ElectricScooter("S", "Scooter", 6, !decision, 2m);

        Assert.Equal(decision, fleet.AddVehicle(vehicle));
        Assert.Equal(decision ? 1 : 0, fleet.GetInventory().Count);
    }

    [Fact]
    public void TotalEnergy() {
        var fleet = CreateFleet();
        fleet.AddVehicle(new ElectricScooter("S", "Scooter", 6, true, 1.5m));
        fleet.AddVehicle(new ElectricBicycle("E", "E-bike", 8, true, 2m));
        fleet.AddVehicle(new Bicycle("B", "Bike", 5, true));
        fleet.AddEquipment(new Helmet("H", "Helmet"));
        fleet.AddEquipment(new ChargingCabinet("C", "Cabinet", 3m));

        Assert.Equal(6.5m, fleet.GetDailyEnergyConsumption());
        Assert.Equal(3, fleet.GetVehicles().Count);
        Assert.Equal(5, fleet.GetInventory().Count);
    }

    [Theory]
    [InlineData(5, false)]
    [InlineData(6, true)]
    public void BeginnerVehicles(int score, bool suitable) {
        var fleet = CreateFleet();
        fleet.AddVehicle(new Bicycle("B", "Bike", score, true));
        Assert.Equal(suitable ? 1 : 0, fleet.GetBeginnerVehicles().Count);
    }

    [Fact]
    public void DuplicateNumber() {
        var fleet = CreateFleet();
        fleet.AddVehicle(new Bicycle(" B-01 ", "Bike", 5, true));
        Assert.Throws<InvalidOperationException>(() => fleet.AddEquipment(new Helmet("b-01", "Helmet")));
        Assert.Throws<InvalidOperationException>(() => fleet.AddVehicle(new Bicycle("B-01", "Bike", 6, true)));
        Assert.Single(fleet.GetInventory());
    }

    private static FleetService CreateFleet() {
        return new FleetService(new ServiceCenter());
    }
}
