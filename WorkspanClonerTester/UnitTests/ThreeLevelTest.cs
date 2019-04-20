using Newtonsoft.Json;
using WorkspanCloner;
using WorkspanCloner.Models;
using Xunit;

namespace WorkspanClonerTester.UnitTests
{
    public class ThreeLevelTest
    {
        [Fact]
        public void HasClonedFourNodes()
        {
            var response = Runner.ParseAndRun(new[] { "Resources/3levelexample.json", "5" });
            var data = JsonConvert.DeserializeObject<EntitiesGraph>(response);

            Assert.NotNull(data);
            Assert.Equal(9, data.Entities.Count);
            Assert.Equal(11, data.Links.Count);
            Assert.Contains(data.Links, x => x.From == 3 && x.To == 13);
            Assert.Contains(data.Links, x => x.From == 13 && x.To == 14);
            Assert.Contains(data.Links, x => x.From == 14 && x.To == 15);
            Assert.Contains(data.Links, x => x.From == 15 && x.To == 16);
            Assert.Contains(data.Links, x => x.From == 14 && x.To == 16);
        }
    }
}
