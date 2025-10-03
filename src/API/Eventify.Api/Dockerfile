# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "."]
COPY ["src/API/Eventify.Api/Eventify.Api.csproj", "src/API/Eventify.Api/"]
# Shared projects
COPY ["src/Shared/Eventify.Shared.Application/Eventify.Shared.Application.csproj", "src/Shared/Eventify.Shared.Application/"]
COPY ["src/Shared/Eventify.Shared.Domain/Eventify.Shared.Domain.csproj", "src/Shared/Eventify.Shared.Domain/"]
COPY ["src/Shared/Eventify.Shared.Infrastructure/Eventify.Shared.Infrastructure.csproj", "src/Shared/Eventify.Shared.Infrastructure/"]
COPY ["src/Shared/Eventify.Shared.Presentation/Eventify.Shared.Presentation.csproj", "src/Shared/Eventify.Shared.Presentation/"]

# Events module
COPY ["src/Modules/Events/Eventify.Modules.Events.Application/Eventify.Modules.Events.Application.csproj", "src/Modules/Events/Eventify.Modules.Events.Application/"]
COPY ["src/Modules/Events/Eventify.Modules.Events.Domain/Eventify.Modules.Events.Domain.csproj", "src/Modules/Events/Eventify.Modules.Events.Domain/"]
COPY ["src/Modules/Events/Eventify.Modules.Events.Infrastructure/Eventify.Modules.Events.Infrastructure.csproj", "src/Modules/Events/Eventify.Modules.Events.Infrastructure/"]
COPY ["src/Modules/Events/Eventify.Modules.Events.IntegrationEvents/Eventify.Modules.Events.IntegrationEvents.csproj", "src/Modules/Events/Eventify.Modules.Events.IntegrationEvents/"]
COPY ["src/Modules/Events/Eventify.Modules.Events.Presentation/Eventify.Modules.Events.Presentation.csproj", "src/Modules/Events/Eventify.Modules.Events.Presentation/"]

# Users module
COPY ["src/Modules/Users/Eventify.Modules.Users.Application/Eventify.Modules.Users.Application.csproj", "src/Modules/Users/Eventify.Modules.Users.Application/"]
COPY ["src/Modules/Users/Eventify.Modules.Users.Domain/Eventify.Modules.Users.Domain.csproj", "src/Modules/Users/Eventify.Modules.Users.Domain/"]
COPY ["src/Modules/Users/Eventify.Modules.Users.Infrastructure/Eventify.Modules.Users.Infrastructure.csproj", "src/Modules/Users/Eventify.Modules.Users.Infrastructure/"]
COPY ["src/Modules/Users/Eventify.Modules.Users.IntegrationEvents/Eventify.Modules.Users.IntegrationEvents.csproj", "src/Modules/Users/Eventify.Modules.Users.IntegrationEvents/"]
COPY ["src/Modules/Users/Eventify.Modules.Users.Presentation/Eventify.Modules.Users.Presentation.csproj", "src/Modules/Users/Eventify.Modules.Users.Presentation/"]

# Ticketing module
COPY ["src/Modules/Ticketing/Eventify.Modules.Ticketing.Application/Eventify.Modules.Ticketing.Application.csproj", "src/Modules/Ticketing/Eventify.Modules.Ticketing.Application/"]
COPY ["src/Modules/Ticketing/Eventify.Modules.Ticketing.Domain/Eventify.Modules.Ticketing.Domain.csproj", "src/Modules/Ticketing/Eventify.Modules.Ticketing.Domain/"]
COPY ["src/Modules/Ticketing/Eventify.Modules.Ticketing.Infrastructure/Eventify.Modules.Ticketing.Infrastructure.csproj", "src/Modules/Ticketing/Eventify.Modules.Ticketing.Infrastructure/"]
COPY ["src/Modules/Ticketing/Eventify.Modules.Ticketing.IntegrationEvents/Eventify.Modules.Ticketing.IntegrationEvents.csproj", "src/Modules/Ticketing/Eventify.Modules.Ticketing.IntegrationEvents/"]
COPY ["src/Modules/Ticketing/Eventify.Modules.Ticketing.Presentation/Eventify.Modules.Ticketing.Presentation.csproj", "src/Modules/Ticketing/Eventify.Modules.Ticketing.Presentation/"]

RUN dotnet restore "./src/API/Eventify.Api/Eventify.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Eventify.Api"
RUN dotnet build "./Eventify.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eventify.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eventify.Api.dll"]