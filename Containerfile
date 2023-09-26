# Use Red Hat UBI 8 for runtime
FROM registry.access.redhat.com/ubi8/dotnet-70-runtime AS base
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Use Red Hat UBI 8 SDK for build
FROM registry.access.redhat.com/ubi8/dotnet-70 AS build
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

USER root
WORKDIR /src
COPY . .

# Build the application
FROM build AS restore
RUN dotnet restore "RefKafkaApp.ProducerService.sln"

RUN dotnet test "RefKafkaApp.ProducerService.sln" -c Release -o /app/build

# Publish the application
FROM restore AS publish
RUN dotnet publish "RefKafkaApp.ProducerService.sln" -c Release -o /app/publish

# Create the final image
FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.Api.dll"]