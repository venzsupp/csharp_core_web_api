1. docker build -t webcoreapi-image .
2. docker run --name webcoreapi --volume .:/var/www/html/src -d -p 5080:8080 webcoreapi-image

# for swagger UI

# http://localhost:5201/swagger/index.html

# MSSQL Sever login - command in container

# /opt/mssql-tools18/bin/sqlcmd -S "127.0.0.1,1433" -U sa -P "Platinum01" -C

# https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-cmd

# dotnet entity framework global command tool

# dotnet tool install --global dotnet-ef

# export PATH="$PATH:/root/.dotnet/tools"

# dotnet ef migrations add InitialCreate

# dotnet ef database update

<!-- SELECT * FROM INFORMATION_SCHEMA.TABLES; GO -->
<!-- SELECT * FROM [webapi_db].INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' -->
