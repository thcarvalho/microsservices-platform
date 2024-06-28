#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MSP.Clients.API/MSP.Clients.API.csproj", "src/MSP.Clients.API/"]
COPY ["src/MSP.Data.Core/MSP.Data.Core.csproj", "src/MSP.Data.Core/"]
COPY ["src/MSP.Core/MSP.Core.csproj", "src/MSP.Core/"]
COPY ["src/MSP.MessageBus/MSP.MessageBus.csproj", "src/MSP.MessageBus/"]
COPY ["src/MSP.WebAPI/MSP.WebAPI.csproj", "src/MSP.WebAPI/"]
RUN dotnet restore "src/MSP.Clients.API/MSP.Clients.API.csproj"
COPY . .
WORKDIR "/src/src/MSP.Clients.API"
RUN dotnet build "MSP.Clients.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSP.Clients.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSP.Clients.API.dll"]