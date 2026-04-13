FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install debugger
# RUN apt-get update \
#     && apt-get install -y curl unzip \
#     && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg 
    
ARG BUILD_CONFIGURATION=Release
ARG ENVIRONMENT=Development
WORKDIR /src/api
COPY ["csharp_core_web_api.csproj", "."]
RUN dotnet restore "./csharp_core_web_api.csproj"


    
COPY . /src/api
# WORKDIR "/src/csharp_core_web_api"
RUN dotnet build "./csharp_core_web_api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./csharp_core_web_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN dotnet build -c Debug -o /app/build
RUN dotnet publish -c Debug -o /app/publish

FROM build AS final
WORKDIR /src/api
EXPOSE 8080
COPY --from=build /app/build .
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT
ENTRYPOINT ["dotnet", "csharp_core_web_api.dll"]

#### after change run this command docker-compose up --build

## dotnet new xunit -n MyApp.Tests