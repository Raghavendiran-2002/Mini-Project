
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PharmacyManagementSystem/PharmacyManagementSystem.csproj", "PharmacyManagementSystem/"]
RUN dotnet restore "./PharmacyManagementSystem/PharmacyManagementSystem.csproj"
COPY . .
WORKDIR "/src/PharmacyManagementSystem"
RUN dotnet build "./PharmacyManagementSystem.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PharmacyManagementSystem.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY log4net.config /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmacyManagementSystem.dll"]