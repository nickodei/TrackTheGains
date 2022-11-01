# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY ["TrackTheGains.WebApi/TrackTheGains.WebApi.csproj", "TrackTheGains.WebApi/"]
COPY ["TrackTheGains.Shared/TrackTheGains.Shared.csproj", "TrackTheGains.Shared/"]
RUN dotnet restore "TrackTheGains.WebApi/TrackTheGains.WebApi.csproj" 
    
# Copy everything else and build
COPY . .
WORKDIR "/app/TrackTheGains.WebApi"
RUN dotnet build "TrackTheGains.WebApi.csproj" -c Release -o /app/build

from build-env as publish
RUN dotnet publish "TrackTheGains.WebApi.csproj" -c Release -o /app/publish
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackTheGains.WebApi.dll"]