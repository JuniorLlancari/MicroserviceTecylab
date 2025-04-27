FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9080
 
FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Cursos/Cursos.Api/Cursos.Api.csproj", "Cursos/Cursos.Api/"]
RUN dotnet restore "Cursos/Cursos.Api/Cursos.Api.csproj"
COPY . .
WORKDIR "/src/Cursos/Cursos.Api"
RUN dotnet build "./Cursos.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./Cursos.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Cursos.Api.dll"]