using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using WalletApi.Application.UnitOfWork;
using WalletApi.Domain.Interface;
using WalletApi.Domain.Service;
using WalletApi.Infraestructure.Models;

public class WalletServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<IWalletService>> _loggerMock;
    private readonly WalletService _walletService;

    public WalletServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<IWalletService>>();
        _walletService = new WalletService(_unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateWalletAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        // Arrange
        var walletDto = new WalletDTO { Name = "" };

        // Act
        var result = await _walletService.CreateWalletAsync(walletDto);

        // Assert
        Assert.NotNull(result.Errors);
        Assert.Equal("El nombre de la billetera no puede estar vacío.", result.Errors.First().Message);
        Assert.Equal(400, result.Errors.First().StatusCode);
    }

    [Fact]
    public async Task CreateWalletAsync_ShouldReturnError_WhenWalletAlreadyExists()
    {
        // Arrange
        var walletDto = new WalletDTO { Name = "Wallet1", Id = 1 };

        _unitOfWorkMock.Setup(u => u.Repository<Wallet>()
            .ExistsAsync(It.IsAny<Expression<Func<Wallet, bool>>>()))
            .ReturnsAsync(true);

        // Act
        var result = await _walletService.CreateWalletAsync(walletDto);

        // Assert
        Assert.NotNull(result.Errors);
        Assert.Equal("Ya existe una billetera con este ID.", result.Errors.First().Message);
        Assert.Equal(409, result.Errors.First().StatusCode);
    }

    [Fact]
    public async Task CreateWalletAsync_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        var walletDto = new WalletDTO { Name = "Wallet1", Id = 1 };
        var wallet = new Wallet { Id = 1, Name = "Wallet1" };

        _unitOfWorkMock.Setup(u => u.Repository<Wallet>()
            .ExistsAsync(It.IsAny<Expression<Func<Wallet, bool>>>()))
            .ReturnsAsync(false);

        _mapperMock.Setup(m => m.Map<Wallet>(walletDto)).Returns(wallet);
        _mapperMock.Setup(m => m.Map<WalletDTO>(wallet)).Returns(walletDto);

        // Act
        var result = await _walletService.CreateWalletAsync(walletDto);

        // Assert
        Assert.NotNull(result.Body);
        Assert.Equal("Wallet1", result.Body.Name);
        Assert.Equal(1, result.Body.Id);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }



}