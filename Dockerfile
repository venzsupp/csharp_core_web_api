FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install debugger
RUN apt-get update \
    && apt-get install -y curl unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg 
    
ARG BUILD_CONFIGURATION=Release
WORKDIR /var/www/html
COPY ["csharp_core_web_api.csproj", "."]
RUN dotnet restore "./csharp_core_web_api.csproj"


    
COPY . .
WORKDIR "/var/www/html"
RUN dotnet build "./csharp_core_web_api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./csharp_core_web_api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /var/www/html
# EXPOSE 8080
COPY --from=build /app/build .
COPY --from=publish /app/publish .
ARG ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT
ENTRYPOINT ["dotnet", "csharp_core_web_api.dll"]

#### after change run this command docker-compose up --build