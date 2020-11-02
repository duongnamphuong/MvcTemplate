# How to use script

* Create a blank database in SQL Server, naming it "Authorize".
* Run whole script file to add necessary objects and data.

# How to generate script file from the database.

Right-click *Authorize* database, choose *Tasks* > *Generate Scripts*

*Select specific database objects*, check all *Tables*.

*Advanced Scripting Options*:

* *Types of data to script*: Schema and data
* *Script Collation*: True
* *Include Descriptive Headers*: False
* *...* (other options): leave it default. But if the content of the generated file differs from that of the current script file, you should check which options should be changed and modify them yourself.

*File to generate*: input the full directory of the script file to which you would like to export. For example: **D:&#92;Git&#92;MvcTemplate&#92;Databases&#92;Authorize&#92;schema-and-data.sql**

# How to change schema

Database-first.

* Firstly, you change the schema of the database.

* After that, you "update" the "Model" in the project.

* Finally, export the database to script file, overwriting the current file.
