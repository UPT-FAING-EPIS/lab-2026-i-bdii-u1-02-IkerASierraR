FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
# Cambiamos el 80 por 8080
EXPOSE 8080
# Forzamos a la app a usar el puerto 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ClienteAPI.csproj", "."]
RUN dotnet restore "./ClienteAPI.csproj"
COPY . .
RUN dotnet build "ClienteAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClienteAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]