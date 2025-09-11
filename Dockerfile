# Use the .NET 6 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy solution and project files
COPY csharp/Platform.Data.Doublets.Gql.sln ./
COPY csharp/Platform.Data.Doublets.Gql.Schema/Platform.Data.Doublets.Gql.Schema.csproj ./Platform.Data.Doublets.Gql.Schema/
COPY csharp/Platform.Data.Doublets.Gql.Server/Platform.Data.Doublets.Gql.Server.csproj ./Platform.Data.Doublets.Gql.Server/
COPY csharp/Platform.Data.Doublets.Gql.Tests/Platform.Data.Doublets.Gql.Tests.csproj ./Platform.Data.Doublets.Gql.Tests/

# Restore packages
RUN dotnet restore

# Copy source code
COPY csharp/ ./

# Build and publish the application
RUN dotnet publish Platform.Data.Doublets.Gql.Server/Platform.Data.Doublets.Gql.Server.csproj -c Release -o out --no-restore

# Use the .NET 6 runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published application
COPY --from=build /app/out .

# Create directory for database files
RUN mkdir -p /app/data

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port 80
EXPOSE 80

# Run the application with the database path in the data volume
ENTRYPOINT ["dotnet", "Platform.Data.Doublets.Gql.Server.dll", "/app/data/db.links"]