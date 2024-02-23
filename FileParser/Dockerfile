FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props .
COPY FileParser/FileParser.csproj FileParser/
COPY MessagingContracts/MessagingContracts.csproj MessagingContracts/
RUN dotnet restore FileParser/FileParser.csproj

COPY FileParser FileParser
COPY MessagingContracts MessagingContracts
RUN dotnet publish FileParser --no-restore -c release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "FileParser.dll"]
