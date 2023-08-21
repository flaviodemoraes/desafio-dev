#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/DesafioDev.WebApi/DesafioDev.WebApi.csproj", "src/DesafioDev.WebApi/"]
COPY ["src/DesafioDev.Operacoes.Applications/DesafioDev.Operacoes.Applications.csproj", "src/DesafioDev.Operacoes.Applications/"]
COPY ["src/DesafioDev.Core/DesafioDev.Core.csproj", "src/DesafioDev.Core/"]
COPY ["src/DesafioDev.Operacoes.Domain/DesafioDev.Operacoes.Domain.csproj", "src/DesafioDev.Operacoes.Domain/"]
COPY ["src/DesafioDev.Operacoes.Infra/DesafioDev.Operacoes.Infra.csproj", "src/DesafioDev.Operacoes.Infra/"]
RUN dotnet restore "src/DesafioDev.WebApi/DesafioDev.WebApi.csproj"
COPY . .
WORKDIR "/src/src/DesafioDev.WebApi"
RUN dotnet build "DesafioDev.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioDev.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioDev.WebApi.dll"]