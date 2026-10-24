namespace VyshCat.Models;

public sealed class Bicycle : Vehicle {
    public Bicycle(string inventoryNumber, string name, int beginnerScore, bool isOperational)
     : base(inventoryNumber, name, beginnerScore, isOperational) {}
}
