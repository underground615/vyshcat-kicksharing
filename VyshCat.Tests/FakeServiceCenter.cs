using VyshCat.Models;
using VyshCat.Services;

namespace VyshCat.Tests;

internal sealed class FakeServiceCenter : IServiceCenter {
    private readonly bool result;

    public FakeServiceCenter(bool result) {
        this.result = result;
    }

    public bool Inspect(Vehicle vehicle) {
        return result;
    }
}
