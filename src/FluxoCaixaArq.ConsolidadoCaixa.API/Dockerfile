FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FluxoCaixaArq.ConsolidadoCaixa.API/FluxoCaixaArq.ConsolidadoCaixa.API.csproj", "src/FluxoCaixaArq.ConsolidadoCaixa.API/"]
RUN dotnet restore "src/FluxoCaixaArq.ConsolidadoCaixa.API/FluxoCaixaArq.ConsolidadoCaixa.API.csproj"
COPY . .
WORKDIR "/src/src/FluxoCaixaArq.ConsolidadoCaixa.API"
RUN dotnet build "FluxoCaixaArq.ConsolidadoCaixa.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FluxoCaixaArq.ConsolidadoCaixa.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixaArq.ConsolidadoCaixa.API.dll"]
