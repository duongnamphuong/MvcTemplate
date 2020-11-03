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

# Implemented items

* A simple Login template. Currently, it is pseudo because it checks user and password hard-coded. 
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

# Build CSS from SCSS

* Download **Dart Sass**: [https://github.com/sass/dart-sass/releases](https://github.com/sass/dart-sass/releases) and extract it somewhere. You will have **dart-sass** folder after that. Add the directory of dart-sass (i.e: **D:&#92;dart-sass&#92;**&#41; into *PATH* environment parameter. At the time I wrote this readme, I used the version **1.27.0** of Dart Sass.
* Build CSS with **sass** command. For example, **sass Site.scss Site.css** command will build **Site.css** and **Site.css.map** from **Site.scss**. For easy use in this repo, I created a batch file **Sass build.bat** at the root directory. If you are about to change CSS, please do it in SCSS file, then run the **Sass build.bat** file.

There is a rule telling that we should not commit files that were built from source. However, we still have to commit **.scss**, **.css**, and **.css.map** files even though **.css**, and **.css.map** were generated from **.scss** because the actual file that the browser uses is **.css** file.

To make sure the built files are not changed, please make sure you use the same version of Dart Sass as I do.   

# Future implementations

* Highcharts
* Implement login form submission, checking the submitted information with that in *Authorize* database and return a JWT in the response's cookie.
* Implement logic so that the web only accepted tokens issued by itself. Tips: use *TokenIssued* table in *Authorize* database.
* A Quartz scheduler (either in-memory or database) that cleanup expired tokens in *TokenIssued* table.
