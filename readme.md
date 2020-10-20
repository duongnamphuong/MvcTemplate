# Introduction

This is a project of MVC - Web API on .NET framework.

I am going to implement typical features of a Web Application:

* Authentication: JWT
* Database connecting: SQL Server - ADO.NET
* Scheduling: Quartz
* Writing logs: log4net

You can use this as a template for other web application.

# Environment

* Visual Studio 2017
* .NET Framework 4.5.2

# Implemented items

* A simple Login template
* A customized Authorize Attribute
* JSON Web Token (JWT)
* SCSS (Sassy CSS)

# Build CSS from SCSS

* Download Dart Sass: [https://github.com/sass/dart-sass/releases](https://github.com/sass/dart-sass/releases) and extract it somewhere. You will have **dart-sass** folder after that. Add the directory of dart-sass (i.e: **D:&#92;dart-sass&#92;**&#41; into *PATH* environment parameter. At the time I wrote this readme, I used **Dart Sass 1.27.0**.
* Build CSS with **sass** command. For example, **sass Site.scss Site.css** command will build **Site.css** and **Site.css.map** from **Site.scss**. For easy use in this repo, I created a batch file **Sass build.bat** at the root directory.

# Future implementations

* ADO.NET
* Quartz (either in-memory or database)
