using Newtonsoft.Json;
using WorkspanCloner;
using WorkspanCloner.Models;
using Xunit;

namespace WorkspanClonerTester.UnitTests
{
    public class TwoLevelTest
    {
        [Fact]
        public void HasClonedThreeNodes()
        {
            var response = Runner.ParseAndRun(new[] { "Resources/2levelexample.json", "5" });
            var data = JsonConvert.DeserializeObject<EntitiesGraph>(response);

            Assert.NotNull(data);
            Assert.Equal(7, data.Entities.Count);
            Assert.Equal(7, data.Links.Count);
            Assert.Contains(data.Links, x => x.From == 3 && x.To == 12);
            Assert.Contains(data.Links, x => x.From == 12 && x.To == 13);
            Assert.Contains(data.Links, x => x.From == 13 && x.To == 14);
        }
    }
}
