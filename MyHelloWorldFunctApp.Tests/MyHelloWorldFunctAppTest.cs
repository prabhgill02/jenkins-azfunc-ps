using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHelloWorldFuncApp;
using Xunit;
using Moq;

public class MyHttpFunctionTests
{
    [Fact]
    public void Run_ShouldReturn_OkObjectResult_WithExpectedMessage()
    {
        // Arrange
        var mockHttpRequest = new Mock<HttpRequest>();

        // Act
        var result = MyHttpFunction.Run(mockHttpRequest.Object) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Welcome to Azure Functions", result.Value);
    }
}
