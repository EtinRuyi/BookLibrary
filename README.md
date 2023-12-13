#Book Library Web Application
Overview
This is an online Book Library web  application is built with a focus on providing a user-friendly interface for managing and accessing a digital book collection.

Technologies Used
C#: Programming language used for backend development.
ASP.NET Core API: Framework for building APIs.
ASP.NET Core MVC: Framework for building web applications.
MSQL Server: Relational database management system.
Swagger Documentation: API documentation tool for easy understanding and testing.
Entity Framework Core: Object-Relational Mapping (ORM) framework for database interactions.
JWT Authentication: JSON Web Token for secure user authentication.
Cloudinary: Cloud-based image and video management service for handling media assets.
HTML and Bootstrap: Frontend technologies for designing and styling the user interface.
Git and GitHub: Version control system for collaboration and code management.
Project Structure
The project follows a backend-first approach, where a robust API is developed before building the frontend using MVC. The separation of concerns ensures modularity and maintainability.

Backend (API)

Controllers: Handle incoming HTTP requests and manage the flow of data.
Models: Define the structure of the data.
Services: Contain business logic and interact with the database.
Authentication: JWT authentication for secure access.
Frontend (MVC)

Views: Define the user interface using HTML and Bootstrap.
Controllers: Handle user input and interact with the backend API.
Models: Represent the data to be displayed in the views.
Setup
Database Configuration: Set up the connection string for the SQL Server database in appsettings.json.
Cloudinary Configuration: Configure Cloudinary credentials for media asset management.
API and MVC Configuration: Update API base URLs in MVC application for endpoint consumption.

API Documentation
API endpoints were tested using Swagger documentation.
