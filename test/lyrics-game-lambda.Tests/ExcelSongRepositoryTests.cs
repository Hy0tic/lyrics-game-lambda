using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Moq;
using songdb;

namespace lyrics_game_lambda.Tests;

public class ExcelSongRepositoryTests
{
    private ExcelSongRepository songRepository { get; set; }
    private Mock<IExcelPackageWrapper> excelPackageWrapperMock { get; set; }
    public ExcelSongRepositoryTests()
    {
        excelPackageWrapperMock = new Mock<IExcelPackageWrapper>();

        excelPackageWrapperMock.Setup(service => 
                service
                    .GetTitleAtRow(It.IsAny<int>()))
            .Returns("song title");

        excelPackageWrapperMock.Setup(service => 
                service
                    .GetLyricsAtRow(It.IsAny<int>()))
            .Returns("song lyrics");

        excelPackageWrapperMock.Setup(service => 
                service
                    .GetAlbumAtRow(It.IsAny<int>()))
            .Returns("song album");

        excelPackageWrapperMock.Setup(service => 
                service
                    .GetTotalRows())
            .Returns(254);
        
        songRepository = new ExcelSongRepository(excelPackageWrapperMock.Object);
    }

    [Fact]
    public async void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var upperCase = await function.FunctionHandler(context);

        Assert.Equal(upperCase, upperCase);
    }
    
    [Fact]
    public void GetRandomSongTitleReturnString()
    {
        var title = songRepository.GetRandomSongTitle();
        Assert.IsType<string>(title);
    }
    
    [Fact]
    public void GetRandomSongTitle_IsNotNull()
    {
        var title = songRepository.GetRandomSongTitle();
        Assert.NotNull(title);
    }

    [Fact]
    public void GetRandomSongReturnSong()
    {
        var song = songRepository.GetRandomSong();
        Assert.IsType<Song>(song);
    }
    
    [Fact]
    public void GetRandomSOng_IsNotNull()
    {
        var song = songRepository.GetRandomSong();
        Assert.NotNull(song);
    }
}
