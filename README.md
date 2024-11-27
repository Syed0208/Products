# Products
A .NET 8.0 Web API for product management using Clean Architecture and CQRS.

## Project Structure

- `API`: This is the main API project, which is the application's entry point.
- `Application`: This layer contains the application logic and is responsible for the application's behaviour.
- `Domain`:  This contains domain models and business logic.
- `Infrastructure`: This contains infrastructure-related code, such as database interactions.
- `tests/Application.Tests`: This contains unit tests.

## Packages and Libraries

- Entity Framework Core for database access.
- AutoMapper for object-to-object mapping.
- MediatR for implementing CQRS and handler logic.
- FluentValidation for input validation.
- xUnit for unit testing.

## Getting Started

### Prerequisites

- .NET 8.0
- Visual Studio 2022 or later

### Building

To build the project, open the `Products.sln` file in Visual Studio and build the solution.

### Migrations

To apply the migrations:
- Open Package Manager Console.
- Choose Infrastructure project.
- Run Update-Database

### Running

To run the project, set `API` as the startup project in Visual Studio and start the application.

## API Endpoints

1. `GET /api/products`

2. `POST /api/products`
   - Body: JSON object with properties `Name`, `Category`, `Description`, `Price`, `StockAvailable`

3. `GET /api/products/{id}`
   - Parameters: `id`

4. `PUT /api/products/{id}`
   - Parameters: `id`
   - Body: JSON object with properties `Id` `Name`, `Category`, `Description`, `Price`

5. `DELETE /api/products/{id}`
   - Parameters: `id`

6. `PUT /api/products/add-to-stock/{id}/{quantity}`
   - Parameters: `id`, `quantity`

7. `PUT /api/products/decrement-stock/{id}/{quantity}`
   - Parameters: `id`, `quantity`

## Tests

The tests/* directory contains the tests.

 
