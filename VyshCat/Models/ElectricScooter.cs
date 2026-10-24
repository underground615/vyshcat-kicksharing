namespace VyshCat.Models;

public sealed class ElectricScooter : Vehicle, IEnergyConsumer {
    public decimal DailyEnergyConsumptionKwh {
        get;
    }

    public ElectricScooter(string inventoryNumber, string name, int beginnerScore,
        bool isOperational, decimal dailyEnergyConsumptionKwh)
        : base(inventoryNumber, name, beginnerScore, isOperational) {
        if (dailyEnergyConsumptionKwh < 0) {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumptionKwh),
                "Расход энергии не может быть отрицательным.");
        }

        DailyEnergyConsumptionKwh = dailyEnergyConsumptionKwh;
    }
}
