# RiverBook

Welcome to the **RiverBook** repository! RiverBook is an innovative project aimed at providing a seamless and interactive platform for users to explore, share, and manage information about rivers around the world. Built with scalability and user experience in mind, this project leverages modern technologies to deliver an engaging and functional application.

## Features

- **River Information Database**: A comprehensive database of rivers, including their history, geography, and ecology.
- **User Contributions**: Allow users to contribute information, photos, and experiences about specific rivers.
- **Interactive Maps**: Visualize river locations and associated data using interactive mapping tools.
- **Community Engagement**: Create discussions, share updates, and connect with fellow enthusiasts.
- **Search and Filter**: Easily find rivers based on name, region, or specific attributes.

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
