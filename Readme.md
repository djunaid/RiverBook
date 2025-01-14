# RiverBook

Welcome to the **RiverBook** repository! RiverBook is an innovative project aimed at providing a seamless and interactive platform for users to explore, share, and manage information about books. Built with scalability and user experience in mind, this project leverages modern technologies to deliver an engaging and functional application.

## Features

- **Book Database**: A comprehensive database of books, including their details, reviews, and categories.
- **User Contributions**: Allow users to contribute reviews, ratings, and suggestions about specific books.
- **Book Recommendations**: Personalized recommendations based on user preferences and reading history.
- **Community Engagement**: Create discussions, share updates, and connect with fellow book enthusiasts.
- **Search and Filter**: Easily find books based on title, author, genre, or specific attributes.
- **Checkout Feature**: A streamlined process for users to add books to their cart, manage their selections, and complete purchases securely.

## Technology Stack

- **Backend**: .Net Core 8 Web API
- **Frontend**: Angular
- **Database**: Postgres, MongoDB
- **Architecture**: Modular Monolith design for scalability and maintainability

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/djunaid/RiverBook.git
   ```
2. Navigate to the project directory:
   ```bash
   cd RiverBook
   ```
3. Install dependencies:
   ```bash
   # For backend
   

   # For frontend
   npm install 
   
4. Start the application:
   ```bash
   # For backend
   dotnet run

   # For frontend
   npm run
   ```
5. Open your browser and visit:
   ```
   http://localhost:PORT
   ```
   Replace `PORT` with the actual port number the application is running on.

## Project Structure

- **Backend**: Handles APIs, data management, and business logic.
- **Frontend**: Provides the user interface and client-side functionality.
- **Database**: Stores river data, user contributions, and other application information.
- **Mapping Integration**: Provides geographical visualizations and river location tracking.

## Commands

- ConnectionString = Server=127.0.0.1;Port=5432;Database=myDataBase;Integrated Security=true;

- migration command

- dotnet ef migrations add InitialUsers -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ./RiverBooks.Web.csproj -o Data/Migrations 

- dotnet ef database update -c UserDBContext

- dotnet ef database update -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj

- dotnet ef migrations add AddCartItems -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj

- dotnet ef migrations add AddInitialOrder -c OrderDBContext -p ../RiverBooks.OrderProcessing/RiverBooks.OrderProcessing.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj
