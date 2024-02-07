using FluentValidation;
using Moq;
using MSP.Clients.API.Application.Services;
using MSP.Clients.API.Data.Repositories;
using MSP.Clients.API.Models;
using MSP.Clients.UnitTests.Fakers;
using MSP.Core.Models;
using MSP.Data.Core;
using MSP.WebAPI.Services;
using System.Linq.Expressions;
using MSP.Clients.API.Data;
using MSP.Clients.API.DTOs;
using MSP.Clients.API.Validations;
using MSP.Tests.Shared;

namespace MSP.Clients.UnitTests.Application;

public class ClientAppServiceTest : BaseTest
{
    private readonly ClientAppService _clientAppService;
    private readonly ClientRequestValidator _clientRequestValidator;

    public ClientAppServiceTest(MSPFixture fixture) : base(fixture)
    {
        _clientAppService = GetService<ClientAppService>();
        _clientRequestValidator = GetService<ClientRequestValidator>();
    }

    [Fact]
    public async Task ShouldCreateClientSuccessfullyAsync()
    {
        var request = new ClientRequestDTOFaker().Generate();
        var validation = await _clientRequestValidator.ValidateAsync(request);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IClientRepository>()
            .Setup(x => x.CreateAsync(It.IsAny<Client>()))
            .ReturnsAsync(new ClientFaker(request));
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(false);

        await _clientAppService.CreateAsync(request);

        GetMock<IClientRepository>().Verify(x => x.CreateAsync(It.IsAny<Client>()), Times.Once());
        GetMock<IClientsUnitOfWork>().Verify(x => x.CommitAsync(), Times.Once);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Never);
    }

    [Fact]
    public async Task ShouldNotCreateDuplicatedClientAsync()
    {
        var request = new ClientRequestDTOFaker().Generate();
        var validation = await _clientRequestValidator.ValidateAsync(request);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(true);

        await _clientAppService.CreateAsync(request);

        GetMock<IClientRepository>().Verify(x => x.CreateAsync(It.IsAny<Client>()), Times.Never);
        GetMock<IUnitOfWork>().Verify(x => x.CommitAsync(), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Once);
    }

    [Fact]
    public async Task ShouldNotCreateInvalidClientAsync()
    {
        var request = new ClientRequestDTO
        {
            DocumentNumber = "",
            Email = "",
            Name = ""
        };
        var validation = await _clientRequestValidator.ValidateAsync(request);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(false);

        await _clientAppService.CreateAsync(request);

        Assert.Equal(5, validation.Errors.Count);
        GetMock<IClientRepository>().Verify(x => x.CreateAsync(It.IsAny<Client>()), Times.Never);
        GetMock<IUnitOfWork>().Verify(x => x.CommitAsync(), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Once);
    }

    [Fact]
    public async Task ShouldUpdateClientSuccessfullyAsync()
    {
        var request = new ClientRequestDTOFaker().Generate();
        var entity = new ClientFaker().Generate();
        var validation = await _clientRequestValidator.ValidateAsync(request);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IClientRepository>()
            .Setup(x => x.Update(It.IsAny<Client>()))
            .Returns(new ClientFaker(request));
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsByIdAsync(entity.Id))
            .ReturnsAsync(true);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(false);
        GetMock<IClientRepository>()
            .Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<string>()))
            .ReturnsAsync(entity);

        await _clientAppService.UpdateAsync(entity.Id, request);

        GetMock<IClientRepository>().Verify(x => x.Update(It.IsAny<Client>()), Times.Once());
        GetMock<IClientsUnitOfWork>().Verify(x => x.CommitAsync(), Times.Once);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Never);
    }

    [Fact]
    public async Task ShouldNotUpdateDuplicatedClientAsync()
    {
        var request = new ClientRequestDTOFaker().Generate();
        var validation = await _clientRequestValidator.ValidateAsync(request);
        var entity = new ClientFaker().Generate();
        GetMock<IClientRepository>()
            .Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<string>()))
            .ReturnsAsync(entity);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(true);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsByIdAsync(entity.Id))
            .ReturnsAsync(true);

        await _clientAppService.UpdateAsync(entity.Id, request);

        GetMock<IClientRepository>().Verify(x => x.Update(It.IsAny<Client>()), Times.Never);
        GetMock<IUnitOfWork>().Verify(x => x.CommitAsync(), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Once);
    }

    [Fact]
    public async Task ShouldNotUpdateInvalidClientAsync()
    {
        var entity = new ClientFaker().Generate();
        var request = new ClientRequestDTO
        {
            DocumentNumber = "",
            Email = "",
            Name = ""
        };
        var validation = await _clientRequestValidator.ValidateAsync(request);
        GetMock<IValidator<ClientRequestDTO>>()
            .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validation);

        await _clientAppService.UpdateAsync(entity.Id, request);

        Assert.Equal(5, validation.Errors.Count);
        GetMock<IClientRepository>().Verify(x => x.Update(It.IsAny<Client>()), Times.Never);
        GetMock<IUnitOfWork>().Verify(x => x.CommitAsync(), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotifications(validation.Errors), Times.Once);
    }

    [Fact]
    public async Task ShouldDeleteClientSuccessfullyAsync()
    {
        var request = new ClientFaker().Generate();
        GetMock<IClientRepository>()
            .Setup(x => x.GetByIdAsync(request.Id, It.IsAny<string>()))
            .ReturnsAsync(request);
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(true);

        await _clientAppService.DeleteAsync(request.Id);

        GetMock<IClientRepository>().Verify(x => x.DeleteByIdAsync(request.Id), Times.Once());
        GetMock<IClientsUnitOfWork>().Verify(x => x.CommitAsync(), Times.Once);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Never);
    }

    [Fact]
    public async Task ShouldNotDeleteNotFoundClientAsync()
    {
        const int id = 1;
        GetMock<IClientRepository>()
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
            .ReturnsAsync(false);

        await _clientAppService.DeleteAsync(id);

        GetMock<IClientRepository>().Verify(x => x.DeleteByIdAsync(id), Times.Never);
        GetMock<IClientsUnitOfWork>().Verify(x => x.CommitAsync(), Times.Never);
        GetMock<INotificationCollector>().Verify(x => x.AddNotification(It.IsAny<ErrorResponse>()), Times.Once);
    }
}