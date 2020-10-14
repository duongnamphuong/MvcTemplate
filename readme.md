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

# Steps to implement SCSS into an MVC project

* Install-Package BundleTransformer.SassAndScss
* Install-Package LibSassHost.Native.win-x64
* Install-Package LibSassHost.Native.win-x86
* Add **Site.scss** file somewhere. For example, place it in **Content** folder
* In **BundleConfig.cs**, add **"~/Content/site.scss"** into the parameter list of **Include** method

# Future implementations

* ADO.NET
* Quartz (either in-memory or database)
