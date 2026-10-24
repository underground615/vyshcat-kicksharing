using VyshCat.Models;
using VyshCat.Services;
using Xunit;

namespace VyshCat.Tests;

public sealed class ServiceCenterTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CheckCondition(bool operational) {
        Assert.Equal(operational, new ServiceCenter().Inspect(new Bicycle("B", "Bike", 6, operational)));
    }

}
