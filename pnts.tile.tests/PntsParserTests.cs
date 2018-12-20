using NUnit.Framework;
using System.IO;
using System.Reflection;
using Pnts.Tile;

namespace Tests
{
    public class PntsParserTests
    {
        Stream pntsfile;

        [SetUp]
        public void Setup()
        {
            const string testfile = "Pnts.Tile.Tests.testfixtures.1-0-1-1.pnts";
            pntsfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(pntsfile != null);
        }

        [Test]
        public void ParsePntsTest()
        {
            // arrange
            var expectedMagicHeader = "pnts";
            var expectedVersionHeader = 1;

            // act
            var pnts = PntsParser.ParsePnts(pntsfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == pnts.Magic);
            Assert.IsTrue(expectedVersionHeader == pnts.Version);

        }
    }
}