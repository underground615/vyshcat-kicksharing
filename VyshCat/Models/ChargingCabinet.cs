namespace VyshCat.Models;

public sealed class ChargingCabinet : Equipment, IEnergyConsumer {
    public decimal DailyEnergyConsumptionKwh {
        get;
    }

    public ChargingCabinet(string inventoryNumber, string name, decimal dailyEnergyConsumptionKwh)
        : base(inventoryNumber, name) {
        if (dailyEnergyConsumptionKwh < 0) {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumptionKwh),
                "Расход энергии не может быть отрицательным.");
        }

        DailyEnergyConsumptionKwh = dailyEnergyConsumptionKwh;
    }
}
