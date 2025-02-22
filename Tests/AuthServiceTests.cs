using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Big.Services;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly Mock<ILogger<AuthService>> _loggerMock;

    public AuthServiceTests()
    {
        _loggerMock = new Mock<ILogger<AuthService>>();
        _authService = new AuthService(_loggerMock.Object);
    }

    [Fact]
    public async Task LoginAsync_Deve_Retornar_Token_Se_Usuario_Existe()
    {
        var token = await _authService.LoginAsync("email@email.com", "senha123");
        Assert.NotNull(token);
    }
}