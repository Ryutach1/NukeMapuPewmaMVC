# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia el contenido del proyecto
COPY . .

# Publica el proyecto
RUN dotnet publish "NukeMapuPewmaVSC.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copia el resultado de la compilación
COPY --from=build /app/publish .

# Expone el puerto (Render escucha en el 10000)
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando de inicio
ENTRYPOINT ["dotnet", "NukeMapuPewmaVSC.dll"]
