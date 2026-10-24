namespace VyshCat.Models;

public sealed class ElectricBicycle : Vehicle, IEnergyConsumer {
    public decimal DailyEnergyConsumptionKwh {
        get;
    }

    public ElectricBicycle(string inventoryNumber, string name, int beginnerScore,
        bool isOperational, decimal dailyEnergyConsumptionKwh)
        : base(inventoryNumber, name, beginnerScore, isOperational) {
        if (dailyEnergyConsumptionKwh < 0) {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumptionKwh),
                "Расход энергии не может быть отрицательным.");
        }

        DailyEnergyConsumptionKwh = dailyEnergyConsumptionKwh;
    }
}
