# Project summary

* Project Template: Class Library (.NET Framework)
* Framework: .NET Framework 4.5.2
* Use ADO.NET template with Entity Framework 6.1.3 to work with database.
* Database first

# Usage

This project serves as a Data Access Layer between the solution and the Database that holds credential information.

# How to use

All other projects that refer this project must **Install-Package EntityFramework -Version 6.1.3** (same version as this one). Besides, the run app (the web app, any executable program that directly or indirectly refers to this project) must have **&#60;connectionStrings&#62;...&#60;/connectionStrings&#62;** in their *Web.config*/*App.config* file.

# Understand the connection string

| Item  | Meaning |
| ------------- | ------------- |
| *data source*  | The SQL Server server name. This is what you type in *Server name* textbox in SSMS |
| *initial catalog*  | The name of the database you want to use  |
| *integrated security*  | If **True**, then this connection string will use *Windows Authentication*, if **False**, then it turns to *SQL Server Authentication*. In case of **False**, you need to add *user id* and *password* into the connection string. For example, *user id=sa;password=your_sql_server_password;*.
| *user id*<br/>and<br/>*password* | only required if *integrated security=False* |
