# 
Do kol:
logowanie do sqlserwera(wybierz tabelke)
pakiety nuget:
Microsoft.AspNetCore.OpenApi
Microsoft.Data.SqlClient
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Swashbuckle.AspNetCore

ConnectionString: Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True; TrustServerCertificate=True
narzedzia -> nugetPM console
Pm-> Scaffold-DbContext "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -Output DAL
