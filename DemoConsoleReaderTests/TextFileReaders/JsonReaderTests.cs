using DemoConsoleReader;
using DemoConsoleReader.Models;
using DemoConsoleReader.TextFileReaders;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DemoConsoleReaderTests.TextFileReaders
{
    public class JsonReaderTests
    {
        [Fact]
        public void CanReadJson()
        {
            //arrange
            var data = new List<Engine> {
              new Engine { Name = "Engine1", SerialNumber = 101, Operator = new() { Name = "Matt Fraser" }, Measurements = new List<int> {21, 15, 9 } },
              new Engine { Name = "Engine2", SerialNumber = 201, Operator = new() { Name = "Rich Froning" }, Measurements = new List<int> { 9, 15, 21 } }
        };

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.ReadLineFromJson(It.IsAny<string>())).Returns(data);

            var expected = 2;

            //act
            var jsonReader = new JsonReader(mockStream.Object);
            var result = jsonReader.ReadFile<Engine>(It.IsAny<string>()).Count();
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanWriteToJson()
        {
            //arrange
            var file = new List<string>();

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.WriteLine(It.IsAny<string>(), It.IsAny<string>(), true))
                .Callback((string p, string l, bool a) => { file.Add(l); }).Verifiable();

            var expected = 2;

            //act
            var jsonReader = new JsonReader(mockStream.Object);
            jsonReader.AddDataToFile(It.IsAny<string>(), new Engine { Name = "Engine1" });
            jsonReader.AddDataToFile(It.IsAny<string>(), new Engine { Name = "Engine2" });
            //assert
            Assert.Equal(expected, file.Count);
        }

        [Fact]
        public void CanCreateJson()
        {
            //arrange
            var data = new[] { new Engine { Name = "Engine1" }, new Engine { Name = "Engine2" } }; 

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.WriteLine(It.IsAny<string>(), It.IsAny<string>(), false)).Verifiable();
            //act
            var jsonReader = new JsonReader(mockStream.Object);
            jsonReader.CreateFile(It.IsAny<string>(), data);
            //assert
            mockStream.Verify(x => x.WriteLine(It.IsAny<string>(), It.IsAny<string>(), false), Times.Exactly(2));
        }
    }
}
