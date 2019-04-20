using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WorkspanCloner;
using Xunit;

namespace WorkspanClonerTester.UnitTests
{
    public class ValidationTest
    {
        [Fact]
        public void InvalidArgument()
        {
            Assert.Throws<ArgumentException>(() => 
                Runner.ParseAndRun(new[] { "Resources/2levelexample.json", "" }));
        }

        [Fact]
        public void JsonNotFound()
        {
            Assert.Throws<FileNotFoundException>(() =>
                Runner.ParseAndRun(new[] { "Resources/100000levelexample.json", "5" }));
        }

        [Fact]
        public void BadJson()
        {
            Assert.Throws<JsonSerializationException>(() =>
                Runner.ParseAndRun(new[] { "Resources/badexample.json", "5" }));
        }

        [Fact]
        public void NoEntities()
        {
            Assert.Throws<MissingMemberException>(() =>
                Runner.ParseAndRun(new[] { "Resources/noentitiesexample.json", "5" }));
        }

        [Fact]
        public void EntityNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() =>
                Runner.ParseAndRun(new[] { "Resources/2levelexample.json", "1000" }));
        }

        [Fact]
        public void DuplicateEntity()
        {
            Assert.Throws<DuplicateWaitObjectException>(() =>
                Runner.ParseAndRun(new[] { "Resources/duplicateexample.json", "5" }));
        }
    }
}
