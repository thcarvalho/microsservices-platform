using System.Linq.Expressions;
using System.Security.Claims;
using FluentValidation;
using Moq;
using MSP.Auth.API.Application.Services;
using MSP.Auth.API.Data.Repositories;
using MSP.Auth.API.DTOs;
using MSP.Auth.API.Models;
using MSP.Auth.API.Validations;
using MSP.Auth.UnitTests.Fakers;
using MSP.Core.Models;
using MSP.Tests.Shared;
using MSP.WebAPI.Services;

namespace MSP.Auth.UnitTests.Application;

public class AuthAppServiceTest : BaseTest
{
    private readonly AuthAppService _authAppService;
    private readonly RegisterRequestValidator _registerRequestValidator;
    private readonly LoginRequestValidator _loginRequestValidator;

    public AuthAppServiceTest(MSPFixture fixture) : base(fixture)
    {
        _authAppService = GetService<AuthAppService>();
        _registerRequestValidator = GetService<RegisterRequestValidator>();
        _loginRequestValidator = GetService<LoginRequestValidator>();
    }

    [Fact]
    public async Task ShouldLoginSuccessfullyAsync()
    {
        var request = new LoginRequestDTOFaker().Generate();
        var entity = new AuthUserFaker(email: request.Email, password: request.Password).Generate();
        var validation = await _loginRequestValidator.ValidateAsync(request);
        GetMock<IValidator<LoginRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IAuthUserRepository>()
            .Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                                      It.IsAny<string>(),
                                      It.IsAny<bool>()))
            .ReturnsAsync(entity);

        var login = await _authAppService.LoginAsync(request);

        Assert.NotNull(login);
        GetMock<IAuthUserRepository>()
            .Verify(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(), 
                                       It.IsAny<string>(), 
                                       It.IsAny<bool>()), Times.Exactly(2));
        GetMock<ITokenGeneratorService>().Verify(x => x.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Once);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Never);
    }

    [Fact]
    public async Task ShouldNotLoginFailInvalidDTOAsync()
    {
        var request = new LoginRequestDTO
        {
            Email = "",
            Password = ""
        };
        var validation = await _loginRequestValidator.ValidateAsync(request);
        GetMock<IValidator<LoginRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);

        var login = await _authAppService.LoginAsync(request);

        Assert.Null(login);
        Assert.Equal(3, validation.Errors.Count);
        GetMock<IAuthUserRepository>()
            .Verify(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()), Times.Never);
        GetMock<ITokenGeneratorService>().Verify(x => x.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Once);
    }

    [Fact]
    public async Task ShouldNotLoginFailWrongPasswordAsync()
    {
        var request = new LoginRequestDTOFaker().Generate();
        var validation = await _loginRequestValidator.ValidateAsync(request);
        var entity = new AuthUserFaker(email: request.Email).Generate();
        GetMock<IValidator<LoginRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IAuthUserRepository>()
            .Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
            .ReturnsAsync(entity);

        var login = await _authAppService.LoginAsync(request);

        Assert.Null(login);
        GetMock<IAuthUserRepository>()
            .Verify(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        GetMock<ITokenGeneratorService>().Verify(x => x.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Once);
    }

    [Fact]
    public async Task ShouldNotLoginFailNotFoundEmailAsync()
    {
        var request = new LoginRequestDTOFaker().Generate();
        var validation = await _loginRequestValidator.ValidateAsync(request);
        GetMock<IValidator<LoginRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IAuthUserRepository>()
            .Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
            .ReturnsAsync(value: null);

        var login = await _authAppService.LoginAsync(request);

        Assert.Null(login);
        GetMock<IAuthUserRepository>()
            .Verify(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        GetMock<ITokenGeneratorService>().Verify(x => x.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Once);
    }

    [Fact]
    public async Task ShouldRegisterUserSuccessfullyAsync()
    {
        var request = new RegisterRequestDTOFaker().Generate();
        var entity = new AuthUserFaker(email: request.Email, password: request.Password, name: request.Name).Generate();
        var validation = await _registerRequestValidator.ValidateAsync(request);
        GetMock<IValidator<RegisterRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IAuthUserRepository>()
            .Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
            .ReturnsAsync(entity);

        var login = await _authAppService.RegisterAsync(request);

        Assert.NotNull(login);
        GetMock<IAuthUserRepository>()
            .Verify(x => x.GetOneAsync(It.IsAny<Expression<Func<AuthUser, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()), Times.Exactly(2));
        GetMock<ITokenGeneratorService>().Verify(x => x.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Once);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Never);
    }
}