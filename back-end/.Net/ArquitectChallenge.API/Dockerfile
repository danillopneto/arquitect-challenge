FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ArquitectChallenge.API/ArquitectChallenge.API.csproj", "ArquitectChallenge.API/"]
COPY ["ArquitectChallenge.API/ArquitectChallenge.API.xml", "ArquitectChallenge.API/"]
COPY ["ArquitectChallenge.Domain/ArquitectChallenge.Domain.csproj", "ArquitectChallenge.Domain/"]
COPY ["ArquitectChallenge.Configuration/ArquitectChallenge.Configuration.csproj", "ArquitectChallenge.Configuration/"]
COPY ["ArquitectChallenge.Interfaces/ArquitectChallenge.Interfaces.csproj", "ArquitectChallenge.Interfaces/"]
COPY ["ArquitectChallenge.Services/ArquitectChallenge.Services.csproj", "ArquitectChallenge.Services/"]
RUN dotnet restore "ArquitectChallenge.API/ArquitectChallenge.API.csproj"
COPY . .
WORKDIR "/src/ArquitectChallenge.API"
RUN dotnet build "ArquitectChallenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArquitectChallenge.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArquitectChallenge.API.dll"]