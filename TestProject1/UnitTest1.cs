using System.Text;
using System.Xml;
using webapi;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void MassageXml()
    {
        _testOutputHelper.WriteLine($"Current dir: {Directory.GetCurrentDirectory()}");
        string inputFile = "../../../../webapi/bin/Debug/net8.0/webapi.xml";
        var result = ProgramHelper.InsertSummaryTags(inputFile);
        _testOutputHelper.WriteLine(result.CreateNavigator().OuterXml);
    }
}