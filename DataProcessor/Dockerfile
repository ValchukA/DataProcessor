FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Directory.Build.props DataProcessor/DataProcessor.csproj .
RUN dotnet restore
COPY DataProcessor .
RUN dotnet publish --no-restore -c release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "DataProcessor.dll"]
