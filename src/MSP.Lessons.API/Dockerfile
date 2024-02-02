#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MSP.Lessons.API/MSP.Lessons.API.csproj", "src/MSP.Lessons.API/"]
RUN dotnet restore "src/MSP.Lessons.API/MSP.Lessons.API.csproj"
COPY . .
WORKDIR "/src/src/MSP.Lessons.API"
RUN dotnet build "MSP.Lessons.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSP.Lessons.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSP.Lessons.API.dll"]