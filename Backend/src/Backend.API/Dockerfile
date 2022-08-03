#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./src/Backend.API/", "./Backend.API/"]
COPY ["./src/Backend.API/Backend.Api.csproj", "./Backend.API/Backend.Api.csproj"]
COPY ["./src/Backend.Service/", "./Backend.Service/"]
COPY ["./src/Backend.Service/Backend.Service.csproj", "./Backend.Service/Backend.Service.csproj"]
COPY ["./src/Backend.Infra.Data/", "./Backend.Infra.Data/"]
COPY ["./src/Backend.Infra.Data/Backend.Infra.Data.csproj", "./Backend.Infra.Data/Backend.Infra.Data.csproj"]
COPY ["./src/Backend.Repository/", "./Backend.Repository/"]
COPY ["./src/Backend.Repository/Backend.Repository.csproj", "./Backend.Repository/Backend.Repository.csproj"]
RUN dotnet restore "./Backend.API/Backend.Api.csproj"
COPY . .
WORKDIR "/src/Backend.API"
RUN dotnet build "./Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "./app/Backend.Api.dll"]
