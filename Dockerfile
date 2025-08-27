# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar primero los archivos de proyecto (.csproj) para aprovechar la cache
COPY ["BusinessCommunication/BusinessCommunication/BusinessCommunication.csproj", "BusinessCommunication/BusinessCommunication/"]
COPY ["BusinessCommunication/Application/Application.csproj", "BusinessCommunication/Application/"]
COPY ["BusinessCommunication/Domain/Domain.csproj", "BusinessCommunication/Domain/"]
COPY ["BusinessCommunication/Adapter/Adapter.csproj", "BusinessCommunication/Adapter/"]

# Restaurar dependencias
RUN dotnet restore "BusinessCommunication/BusinessCommunication/BusinessCommunication.csproj"

# Copiar todo el código
COPY . .

# Build
WORKDIR "/src/BusinessCommunication/BusinessCommunication"
RUN dotnet build "BusinessCommunication.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "BusinessCommunication.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_EnableDiagnostics=0

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessCommunication.dll"]

