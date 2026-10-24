using VyshCat.Models;

namespace VyshCat.Services;

public sealed class FleetService {
    private readonly IServiceCenter serviceCenter;
    private readonly List<InventoryItem> inventory = new();

    public FleetService(IServiceCenter serviceCenter) {
        ArgumentNullException.ThrowIfNull(serviceCenter);
        this.serviceCenter = serviceCenter;
    }

    public bool AddVehicle(Vehicle vehicle) {
        ArgumentNullException.ThrowIfNull(vehicle);
        EnsureUniqueNumber(vehicle);
        if (!serviceCenter.Inspect(vehicle)) {
            return false;
        }

        inventory.Add(vehicle);
        return true;
    }

    public void AddEquipment(Equipment equipment) {
        ArgumentNullException.ThrowIfNull(equipment);
        EnsureUniqueNumber(equipment);
        inventory.Add(equipment);
    }

    public List<Vehicle> GetVehicles() {
        var result = new List<Vehicle>();
        foreach (var item in inventory) {
            if (item is Vehicle vehicle) {
                result.Add(vehicle);
            }
        }
        return result;
    }

    public List<IInventoryItem> GetInventory() {
        var result = new List<IInventoryItem>();
        foreach (var item in inventory) {
            result.Add(item);
        }
        return result;
    }

    public List<Vehicle> GetBeginnerVehicles() {
        var result = new List<Vehicle>();
        foreach (var vehicle in GetVehicles()) {
            if (vehicle.BeginnerScore >= 6) {
                result.Add(vehicle);
            }
        }
        return result;
    }

    public decimal GetDailyEnergyConsumption() {
        decimal total = 0;
        foreach (var item in inventory) {
            if (item is IEnergyConsumer consumer) {
                total += consumer.DailyEnergyConsumptionKwh;
            }
        }
        return total;
    }

    private void EnsureUniqueNumber(IInventoryItem item) {
        foreach (var existing in inventory) {
            if (string.Equals(existing.InventoryNumber, item.InventoryNumber,
                    StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidOperationException($"Номер '{item.InventoryNumber}' уже занят.");
            }
        }
    }
}
