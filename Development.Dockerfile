FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# Install debugger
# RUN apt-get update \
#     && apt-get install -y curl unzip \
#     && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg 
    
WORKDIR /src/csharp_core_web_api
COPY ["csharp_core_web_api.csproj", "."]
RUN dotnet restore "./csharp_core_web_api.csproj"


    
COPY . .

CMD ["dotnet", "watch", "run", "--urls=http://0.0.0.0:8080"]

#### after change run this command docker-compose up --build

## dotnet new xunit -n MyApp.Tests