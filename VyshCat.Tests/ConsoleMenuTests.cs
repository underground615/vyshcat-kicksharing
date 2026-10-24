using VyshCat.Services;
using Xunit;

namespace VyshCat.Tests;

public sealed class ConsoleMenuTests {
    [Fact]
    public void AddItemsAndShowReport() {
        var fleet = new FleetService(new ServiceCenter());
        var input = string.Join("\n", new[] {
            "1", "1", "S", "Scooter", "6", "1", "1,5",
            "1", "2", "B", "Bike", "5", "1",
            "1", "3", "E", "E-bike", "8", "1", "2.0",
            "1", "1", "R", "Rejected", "9", "0", "99",
            "2", "1", "H", "Helmet",
            "2", "2", "C", "Cabinet", "3",
            "3", "4", "5", "0"
        });
        var output = Capture(input, () => new ConsoleMenu(fleet).Run());
        Assert.Equal(5, fleet.GetInventory().Count);
        Assert.Equal(6.5m, fleet.GetDailyEnergyConsumption());
        Assert.Contains("Транспорт: 3; вещи: 2; всего: 5", output);
        Assert.Contains("S: Scooter", output);
        Assert.Contains("H: Helmet", output);
        Assert.Contains("C: Cabinet", output);
        Assert.Contains("кВт*ч", output);
        Assert.Contains("отклонён техосмотром", output);
    }

    private static string Capture(string input, Action action) {
        var previousInput = Console.In;
        var previousOutput = Console.Out;
        using var reader = new StringReader(input);
        using var writer = new StringWriter();
        try {
            Console.SetIn(reader);
            Console.SetOut(writer);
            action();
            return writer.ToString();
        }
        finally {
            Console.SetIn(previousInput);
            Console.SetOut(previousOutput);
        }
    }
}
