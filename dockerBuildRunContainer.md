1. docker build -t webcoreapi-image .
2. docker run --name webcoreapi --volume .:/var/www/html/src -d -p 5080:8080 webcoreapi-image

# for swagger UI

# http://localhost:5201/swagger/index.html

# MSSQL Sever login - command in container

# /opt/mssql-tools18/bin/sqlcmd -S "127.0.0.1,1433" -U sa -P "Platinum01" -C

# https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-cmd

# docker exec -it csharp-api bash

# dotnet clean

# dotnet build

# dotnet entity framework global command tool

# dotnet tool install --global dotnet-ef

# export PATH="$PATH:/root/.dotnet/tools"

# dotnet ef migrations add InitialCreate

# dotnet ef migrations add InitialCreate --context OAuthCredentialsTokenDbContext

# dotnet ef migrations add AddExpiryDateToOauth --context OAuthCredentialsTokenDbContext

# dotnet ef database update 0 --context OAuthCredentialsTokenDbContext

# dotnet ef migrations remove --context OAuthCredentialsTokenDbContext

# dotnet ef database update

# dotnet ef database update --context UserDbContext

<!-- SELECT * FROM INFORMATION_SCHEMA.TABLES; GO -->
<!-- SELECT * FROM [webapi_db].INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' -->


<!-- Data Source=127.0.0.1,1433;User ID=sa;Password=password;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30 -->
