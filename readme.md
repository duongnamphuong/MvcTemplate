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
* log4net: **LogUtil** project folder is a clone of [https://github.com/duongnamphuong/log4netDemo/tree/master/solution/mylogger/LogUtil](https://github.com/duongnamphuong/log4netDemo/tree/master/solution/mylogger/LogUtil). Please go to that repo for the script that structures the log database.
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

Generated files **Site.css** and **Site.css.map** are not include in Git repo, but their existence is still defined in project (such as **WebApplication1.csproj** and **BundleConfig.cs**). We need that setting so that those files are deployed in server. Those two are ignored by **.gitignore**. Please don't re-add them while keeping using SCSS.

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

# FAQ

**Question**: Why do we store settings of this program to a Singleton class instance, instead of calling `ConfigurationManager.AppSettings["key"]`?

**Answer**:  By using Singleton, we can make use of "Find All References" feature in Visual Studio IDE. It is not so good to search for the text of `key` in whole solution folder.
These getter and setter: `{ get; private set; }` along with `Init...` methods that set value for properties in that Singleton, help to manage the setter well. As we know, in normal circumstances, settings are set only once when the program starts. When doing "Find All References" of those `Init...` methods, we expect that each method has only one reference.

---

**Question**: What is the current version of Quartz that this project uses?

**Answer**: This program uses Quartz version 2.6.1. The latest one before version 3x is 2.6.2. I will consider updating to 2.6.2, but not yet update to version 3, because since version 3x, the setup code changed drastically.
However, I think I  shall go to version 3x after studying its syntax.

---

**Question**: I see that there are branch(es) in which you wrote passwords of your SQL Server into *App.config*/*Web.config*. Is that OK?

**Answer**: Those password was generated randomly with [https://passwordsgenerator.net/](https://passwordsgenerator.net/) and I don't use a shared password in different services. Don't worry. If you clone this project and want to use SQL Server account to connect to database, please modify the user id and password in those connection strings to match those of yours.

---

**Question**: Both **LogUtil** and **WebApplication1** projects have *log4net.config* file and those files are set "Copy to Output Directory" to "Copy always". So when the program is built, which file will be applied?

**Answer**: Those *log4net.config* files will be copied to the Output directory in the same order as building projects (the dependencies project are built before). With this, **LogUtil**' *log4net.config* is copied first, then **WebApplication1**'s *log4net.config* is copied later, overwriting the duplicated file.
