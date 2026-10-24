using System.Globalization;
using VyshCat.Models;
using VyshCat.Services;

namespace VyshCat;

public sealed class ConsoleMenu {
    private readonly FleetService fleet;

    public ConsoleMenu(FleetService fleet) {
        this.fleet = fleet;
    }

    public void Run() {
        Console.WriteLine("ВышКат. Учёт парка. Данные хранятся до выхода из программы.");
        while (true) {
            Console.WriteLine("\n1. Добавить транспорт\n2. Добавить вещь\n3. Отчёт" +
                "\n4. Транспорт для новичков\n5. Инвентаризация\n0. Выход");
            Console.Write("> ");
            var command = Console.ReadLine();
            if (command == null || command == "0") {
                return;
            }

            try {
                switch (command) {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        AddEquipment();
                        break;
                    case "3":
                        PrintReport();
                        break;
                    case "4":
                        PrintItems(fleet.GetBeginnerVehicles());
                        break;
                    case "5":
                        PrintItems(fleet.GetInventory());
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
            catch (EndOfStreamException) {
                return;
            }
            catch (ArgumentException exception) {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }
            catch (InvalidOperationException exception) {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }
        }
    }

    public void RunDemo() {
        Console.WriteLine("ВышКат: демонстрация сценариев");
        PrintReport();
        PrintItems(fleet.GetInventory());
        Admit(new ElectricScooter("S-01", "Самокат", 6, true, 1.5m));
        Admit(new Bicycle("B-01", "Велосипед", 5, true));
        Admit(new ElectricBicycle("E-01", "Электровелосипед", 8, true, 2m));
        Admit(new ElectricScooter("S-02", "Неисправный самокат", 9, false, 100m));
        fleet.AddEquipment(new Helmet("H-01", "Шлем"));
        fleet.AddEquipment(new ChargingCabinet("C-01", "Зарядный шкаф", 3m));
        Console.WriteLine("Шлем и зарядный шкаф поставлены на баланс.");
        PrintReport();
        Console.WriteLine("Транспорт для новичков:");
        PrintItems(fleet.GetBeginnerVehicles());
        Console.WriteLine("Всё имущество:");
        PrintItems(fleet.GetInventory());
    }

    private void AddVehicle() {
        var type = Read("Тип: 1 - электросамокат, 2 - велосипед, 3 - электровелосипед");
        if (type != "1" && type != "2" && type != "3") {
            throw new ArgumentException("Неизвестный тип транспорта.");
        }
        var number = Read("Инвентарный номер");
        var name = Read("Наименование");
        if (!int.TryParse(Read("Простота (1-10)"), out var score)) {
            throw new ArgumentException("Простота должна быть целым числом.");
        }
        var condition = Read("Исправен? 1 - да, 0 - нет");
        if (condition != "0" && condition != "1") {
            throw new ArgumentException("Введите 1 или 0.");
        }
        var operational = condition == "1";
        Vehicle vehicle;
        if (type == "1") {
            vehicle = new ElectricScooter(number, name, score, operational, ReadEnergy());
        }
        else if (type == "2") {
            vehicle = new Bicycle(number, name, score, operational);
        }
        else {
            vehicle = new ElectricBicycle(number, name, score, operational, ReadEnergy());
        }
        Admit(vehicle);
    }

    private void AddEquipment() {
        var type = Read("Тип: 1 - шлем, 2 - зарядный шкаф");
        if (type != "1" && type != "2") {
            throw new ArgumentException("Неизвестный тип вещи.");
        }
        var number = Read("Инвентарный номер");
        var name = Read("Наименование");
        Equipment equipment;
        if (type == "1") {
            equipment = new Helmet(number, name);
        }
        else {
            equipment = new ChargingCabinet(number, name, ReadEnergy());
        }
        fleet.AddEquipment(equipment);
        Console.WriteLine("Вещь поставлена на баланс.");
    }

    private void Admit(Vehicle vehicle) {
        Console.Write($"{vehicle.Name} ({vehicle.InventoryNumber}): ");
        if (fleet.AddVehicle(vehicle)) {
            Console.WriteLine("принят в парк.");
        }
        else {
            Console.WriteLine("отклонён техосмотром.");
        }
    }

    private void PrintReport() {
        var vehicles = fleet.GetVehicles().Count;
        Console.WriteLine($"Транспорт: {vehicles}; вещи: {fleet.GetInventory().Count - vehicles}; " +
            $"всего: {fleet.GetInventory().Count}.");
        Console.WriteLine($"Суточный расход энергии: {fleet.GetDailyEnergyConsumption()} кВт*ч.");
    }

    private static void PrintItems(IEnumerable<IInventoryItem> items) {
        var count = 0;
        foreach (var item in items) {
            Console.WriteLine($"{item.InventoryNumber}: {item.Name}");
            count++;
        }
        if (count == 0) {
            Console.WriteLine("Список пуст.");
        }
    }

    private static string Read(string prompt) {
        Console.Write($"{prompt}: ");
        var value = Console.ReadLine();
        if (value == null) {
            throw new EndOfStreamException();
        }
        return value.Trim();
    }

    private static decimal ReadEnergy() {
        var text = Read("Суточный расход энергии (кВт*ч)").Replace(',', '.');
        if (!decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out var energy)) {
            throw new ArgumentException("Введите расход числом, например 1,5.");
        }
        return energy;
    }
}
