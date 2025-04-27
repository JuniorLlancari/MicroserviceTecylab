FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9090
 
FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Docentes/Docentes.Api/Docentes.Api.csproj", "Docentes/Docentes.Api/"]
RUN dotnet restore "Docentes/Docentes.Api/Docentes.Api.csproj"
COPY . .
WORKDIR "/src/Docentes/Docentes.Api"
RUN dotnet build "./Docentes.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./Docentes.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Docentes.Api.dll"]