# Introduction

This is a project of MVC - Web API on .NET framework.

I am implementing typical features of a Web Application:

* Authentication: JWT
* Database connecting: SQL Server - ADO.NET
* Scheduling: Quartz
* Writing logs: log4net
* Singleton

You can use this as a template for other web application.

# Environment

* Visual Studio 2017
* .NET Framework 4.5.2
* My preferred line break style: Windows-style (\r\n).
* SQL Server 2017: 14.0.2027.
* SQL Server Management Studio (SSMS) v17.9
* Memurai

# Implemented items

* Login form submission, checking the submitted information with that in *Authorize* database and return a JWT in the response's cookie.
* A Quartz scheduler (in-memory) that cleanups expired tokens in *TokenIssued* table. Please note that: the feature that adding data to that table was not implemented yet. I'll do it later (please check **Future implementations**).
* A customized Authorize Attribute
* JSON Web Token (JWT)
* SCSS (Sassy CSS)
* ADO.NET
* Storing passwords in hashes. Current supported hash types:
  * MD5
  * RIPEMD160
  * SHA1
  * SHA256
  * SHA384
  * SHA512
  * HMACMD5
  * HMACRIPEMD160
  * HMACSHA1
  * HMACSHA256
  * HMACSHA384
  * HMACSHA512

* Register account with your preferred password hash type
* Fetching setting from the database once each time the web app starts and store those setting in a singleton class. [What is singleton pattern?](https://en.wikipedia.org/wiki/Singleton_pattern)
* Multiple language with App_GlobalResources.

# Build CSS from SCSS

* Download **Dart Sass**: [https://github.com/sass/dart-sass/releases](https://github.com/sass/dart-sass/releases) and extract it somewhere. You will have **dart-sass** folder after that. Add the directory of dart-sass (i.e: **D:&#92;dart-sass&#92;**&#41; into *PATH* environment parameter. At the time I wrote this readme, I used the version **1.27.0** of Dart Sass.
* Build CSS with **sass** command. For example, **sass Site.scss Site.css** command will build **Site.css** and **Site.css.map** from **Site.scss**. For easy use in this repo, I created a batch file **Sass build.bat** at the root directory.
* The command in that batch file has a **--watch** parameter. Therefore, the console will continuously watch the source file and compiles each time it detects any change. You only need to run the batch file once and keep the console open during your work with the solution.

Source control ruling states that we should not commit files that were built from source. However, we still have to commit **.scss**, **.css**, and **.css.map** files even though **.css**, and **.css.map** were generated from **.scss** because the actual file that the browser uses is **.css** file.

If you would like to contribute, please use the same version of Dart Sass so that the compiled CSS won't be in a mess.

# Memurai

This is a alternative Redis for Windows. Its command syntax is almost the same as Redis'.

Future plan: I intend to use Memurai to store data which is frequently accessed. We can lease the burden of the main database.

## Install Memurai

Download Memurai from: [https://www.memurai.com/](https://www.memurai.com/)

You can use **memurai --help** and **memurai-cli --help** to study their syntax.

# Future implementations

* Highcharts
* Implement logic so that the web only accepted tokens issued by itself. Tips: use *TokenIssued* table in *Authorize* database.
* Google Maps API
* Memurai
* Paypal sandbox API
