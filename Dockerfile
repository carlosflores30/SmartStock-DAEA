# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solución y restaurar
COPY . .
RUN dotnet restore ./Smartstock/Smartstock.csproj
RUN dotnet publish ./Smartstock/Smartstock.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Smartstock.dll"]
