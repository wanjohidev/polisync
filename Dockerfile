# Stage 1 - Build
# Full SDK Image - has compilers/tools needed to build, but won't ship this
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file & Restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source
COPY . .

# Publish App
RUN dotnet publish -c Release -o /app/publish

#Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Render exposes PORT as an environment variable
#ASP.NET Core should listen on that port
ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

# The command that runs when the container starts
ENTRYPOINT [ "dotnet", "polisync.dll" ]