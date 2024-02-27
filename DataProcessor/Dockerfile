FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props .
COPY DataProcessor/DataProcessor.csproj DataProcessor/
COPY MessagingContracts/MessagingContracts.csproj MessagingContracts/
RUN dotnet restore DataProcessor/DataProcessor.csproj

COPY DataProcessor DataProcessor
COPY MessagingContracts MessagingContracts
RUN dotnet publish DataProcessor --no-restore -c release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "DataProcessor.dll"]
