# 🛒 Ecommerce Web API

A modern, cleanly structured **Ecommerce Web API** built with **ASP.NET Core**, **Entity Framework Core**, **JWT authentication**, and **SQL Server**. It provides a secure backend for managing products, categories, users, orders, and more.


```📌 Project Structure```

🔹 Ecommerce.Api

    Contains controllers, middleware, service registrations, authentication setup, and Swagger configuration. This is the entry point for HTTP requests.
🔹 Ecommerce.BLL

    Contains core business services and application rules. Services here process data, enforce logic, and interact with the data layer through interfaces.
🔹 Ecommerce.DAL

    Contains the Entity Framework Core DbContext, entity configurations, repository implementations, and data-related logic. It directly interacts with the SQL Server database.
🔹 Ecommerce.Shared

    contains common data transfer objects (DTOs), request/response models, utility classes, and other types shared across all projects (like ProductDto, OrderDto, etc.).



## Migration Strategy and Execution Plan

1️⃣ Folder Structure Overview

📂 DAL/Migrations/

    In your DAL (Data Access Layer), migrations are stored under:
    20250512153909_ChangeOrderStatusToString.cs
    
    ProductsDbContextModelSnapshot.cs → Snapshot of the latest migration state.

## Apply Migration to the Database
    dotnet ef database update


After the command is executed, the database will be created Or use the sql generated script

    📌 Make sure that DbContext UseSql Server is using the correct ConnectionString
    
![image](https://github.com/user-attachments/assets/c0ca3626-0e75-43af-970e-c23c7c940497)

