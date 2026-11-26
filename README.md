\# Employee Management System – ASP.NET Core Web API



A RESTful Employee Management System API built using ASP.NET Core Web API, Entity Framework Core, SQL Server, and ASP.NET Core Identity. It provides secure authentication and role-based authorization using JSON Web Tokens (JWT).



\## 🚀 Technologies Used



\- C#

\- ASP.NET Core Web API

\- Entity Framework Core

\- Microsoft SQL Server

\- ASP.NET Core Identity

\- JWT Authentication

\- LINQ

\- Swagger / OpenAPI



\## ✨ Features



\- User Registration

\- User Login and Authentication

\- JWT Token Generation and Validation

\- Role-based Authorization (Admin and Employee)

\- Employee Management API

\- User Profile Management

\- Change Password

\- Entity Framework Core Database Integration

\- SQL Server Integration

\- Swagger API Documentation



\## 🔐 Authentication



The API uses JWT Bearer Authentication.



1\. Users authenticate using their username and password.

2\. The API validates the credentials.

3\. A JWT token is generated after successful authentication.

4\. The client sends the token in the Authorization header for protected endpoints.



Example:



```http

Authorization: Bearer YOUR\_JWT\_TOKEN

```



\## ⚙️ Prerequisites



\- .NET SDK

\- Microsoft SQL Server

\- Visual Studio or Visual Studio Code

\- SQL Server Management Studio (optional)



\## 📥 Installation



1\. Clone the repository:



&#x20;  ```bash

&#x20;  git clone https://github.com/tusharsharma898/Employee-Management-System-API.git

&#x20;  ```



2\. Navigate to the project directory:



&#x20;  ```bash

&#x20;  cd Employee-Management-System-API

&#x20;  ```



3\. Configure your database connection string and JWT secret using .NET User Secrets or environment variables.



4\. Apply Entity Framework Core migrations if required:



&#x20;  ```bash

&#x20;  dotnet ef database update

&#x20;  ```



5\. Run the application:



&#x20;  ```bash

&#x20;  dotnet run

&#x20;  ```



6\. Open Swagger in your browser using the URL shown in the application output.



\## 🔑 Configuration



Sensitive configuration values are not included in this repository.



Configure these values locally:



\- `ConnectionStrings:conStr` – SQL Server connection string

\- `AppSettings:Secret` – JWT signing secret



Do not commit passwords, API keys, connection-string credentials, or JWT secrets to source control.



\## 🌐 Frontend Integration



This API is designed to work with an Angular frontend.



\*\*Frontend Repository:\*\*  

https://github.com/tusharsharma898/Employee-Management-System-Angular



\## 👨‍💻 Author



\*\*Tushar Sharma\*\*



GitHub: https://github.com/tusharsharma898

