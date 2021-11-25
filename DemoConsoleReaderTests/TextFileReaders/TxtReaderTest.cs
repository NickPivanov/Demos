using Xunit;
using Moq;
using DemoConsoleReader;
using DemoConsoleReader.TextFileReaders;
using System.Linq;
using System.Collections.Generic;

namespace DemoConsoleReaderTests
{
    public class TxtReaderTest
    {

        [Fact]
        public void CanReadFile()
        {
            //arrange
            var data = new[] { "line1", "line2" };

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.ReadLine(It.IsAny<string>())).Returns(data);

            var expected = 2;

            //act
            var txtReader = new TxtReader(mockStream.Object);
            var result = txtReader.ReadFile<string>(It.IsAny<string>()).Count();
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanWriteToFile()
        {
            //arrange
            var data = new List<string>();

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.WriteLine(It.IsAny<string>(), It.IsAny<string>(), true))
                .Callback((string p, string l, bool a) => { data.Add(l); }).Verifiable();

            var expected = 2;

            //act
            var txtReader = new TxtReader(mockStream.Object);
            txtReader.AddDataToFile(It.IsAny<string>(), "line 1");
            txtReader.AddDataToFile(It.IsAny<string>(), "line 2");
            //assert
            Assert.Equal(expected, data.Count);
        }

        [Fact]
        public void CanCreateFile()
        {
            //arrange
            var data = new[] { "line1", "line2" }; ;
            var path = "test.txt";

            var mockStream = new Mock<IStreamWrap>();
            mockStream.Setup(s => s.WriteLine(path, It.IsAny<string>(), false)).Verifiable();
            //act
            var txtReader = new TxtReader(mockStream.Object);
            txtReader.CreateFile(path, data);
            //assert
            mockStream.Verify(x => x.WriteLine(path, It.IsAny<string>(), false), Times.Exactly(2));
        }
    }
}