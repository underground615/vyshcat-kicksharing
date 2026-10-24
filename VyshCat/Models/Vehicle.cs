namespace VyshCat.Models;

public abstract class Vehicle : InventoryItem {
    public int BeginnerScore {
        get;
    }
    public bool IsOperational {
        get;
    }

    protected Vehicle(string inventoryNumber, string name, int beginnerScore, bool isOperational)
        : base(inventoryNumber, name) {
        if (beginnerScore < 1 || beginnerScore > 10) {
            throw new ArgumentOutOfRangeException(nameof(beginnerScore), "Простота должна быть от 1 до 10.");
        }

        BeginnerScore = beginnerScore;
        IsOperational = isOperational;
    }
}
