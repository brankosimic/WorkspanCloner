using Newtonsoft.Json;
using WorkspanCloner;
using WorkspanCloner.Models;
using Xunit;

namespace WorkspanClonerTester.UnitTests
{
    public class CircularReferenceTest
    {
        [Fact]
        public void HasTwoParentsAndThreeDescdendents()
        {
            var response = Runner.ParseAndRun(new[] { "Resources/circularexample.json", "5" });
            var data = JsonConvert.DeserializeObject<EntitiesGraph>(response);

            Assert.NotNull(data);
            Assert.Equal(7, data.Entities.Count);
            Assert.Equal(10, data.Links.Count);
            Assert.Contains(data.Links, x => x.From == 3 && x.To == 12); // parent of clone
            Assert.Contains(data.Links, x => x.From == 11 && x.To == 12); // parent of clone
            Assert.Contains(data.Links, x => x.From == 12 && x.To == 13); // direct child of clone
            Assert.Contains(data.Links, x => x.From == 13 && x.To == 14); // indirect child of node
            Assert.Contains(data.Links, x => x.From == 14 && x.To == 12); // indirect child of node
        }
    }
}
