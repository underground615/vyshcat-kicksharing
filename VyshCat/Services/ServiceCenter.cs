using VyshCat.Models;

namespace VyshCat.Services;

public sealed class ServiceCenter : IServiceCenter {
    public bool Inspect(Vehicle vehicle) {
        ArgumentNullException.ThrowIfNull(vehicle);
        return vehicle.IsOperational;
    }
}
