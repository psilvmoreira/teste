FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/NovaHub.Backend/NovaHub.Backend.csproj", "src/NovaHub.Backend/"]
RUN dotnet restore "src/NovaHub.Backend/NovaHub.Backend.csproj"
COPY . .
WORKDIR "/src/src/NovaHub.Backend"
RUN dotnet build "./NovaHub.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NovaHub.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NovaHub.Backend.dll"]
