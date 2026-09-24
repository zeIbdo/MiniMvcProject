# MiniMvcProject

An e-commerce web application built with **ASP.NET Core MVC**, featuring product management, user authentication, an administrative panel, and database-driven content management.

## Technologies

* **C# / .NET**
* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **HTML / CSS / JavaScript**
* **Bootstrap**

## Features

* Product management
* Category and tag management
* User registration and authentication
* Role-based authorization
* Administrative panel
* User management
* Database-driven content management
* Entity Framework Core relationships and migrations
* Responsive web interface

## Architecture

The solution contains separate applications for the main website and administrative panel:

* **MiniMvcProject** — Main e-commerce website
* **MiniMvcProject.ADMIN** — Administrative panel

The application uses ASP.NET Core MVC with Entity Framework Core for database access and ASP.NET Core Identity for authentication and authorization.

## Database

**SQL Server** is used as the relational database, with **Entity Framework Core** handling:

* Entity relationships
* Database migrations
* Data access
* LINQ queries
* CRUD operations

## Authentication & Authorization

User authentication and authorization are implemented using **ASP.NET Core Identity**.

Role-based authorization is used to control access to administrative functionality.

## Repository

GitHub: https://github.com/zeIbdo/MiniMvcProject
