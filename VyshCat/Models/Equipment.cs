namespace VyshCat.Models;

public abstract class Equipment : InventoryItem {
    protected Equipment(string inventoryNumber, string name) : base(inventoryNumber, name) {
    }
}
