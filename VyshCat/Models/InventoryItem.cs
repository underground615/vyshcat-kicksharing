namespace VyshCat.Models;

public abstract class InventoryItem : IInventoryItem {
    public string InventoryNumber {
        get;
    }
    public string Name {
        get;
    }

    protected InventoryItem(string inventoryNumber, string name) {
        if (string.IsNullOrWhiteSpace(inventoryNumber)) {
            throw new ArgumentException("Инвентарный номер обязателен.", nameof(inventoryNumber));
        }
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentException("Наименование обязательно.", nameof(name));
        }

        InventoryNumber = inventoryNumber.Trim();
        Name = name.Trim();
    }
}
