using Newtonsoft.Json;
using WorkspanCloner;
using WorkspanCloner.Models;
using Xunit;

namespace WorkspanClonerTester.UnitTests
{
    public class EmptyLinksTest
    {
        [Fact]
        public void OneNodeCopied()
        {
            var response = Runner.ParseAndRun(new[] { "Resources/emptylinksexample.json", "5" });
            var data = JsonConvert.DeserializeObject<EntitiesGraph>(response);

            Assert.NotNull(data);
            Assert.Equal(5, data.Entities.Count);
            Assert.Empty(data.Links);
        }
    }
}
