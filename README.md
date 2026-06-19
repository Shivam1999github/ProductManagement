# Product API Assessment

A RESTful Backend API built with .NET 8 following Clean Architecture principles, implementing CRUD operations for Products and Items with JWT Authentication, Repository Pattern, Unit of Work, FluentValidation, Swagger documentation, Docker support, and automated testing.

---

# Technology Stack

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication & Refresh Tokens
* FluentValidation
* AutoMapper
* Serilog
* Swagger / OpenAPI
* Docker & Docker Compose
* xUnit
* Moq

---

# Architecture

The solution follows Clean Architecture principles and is organized into the following layers:

```text
Solution
│
├── src
│   ├── API
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
├── tests
│   ├── API.Tests
│   ├── Application.Tests
│   └── Infrastructure.Tests
│
└── docker-compose.yml
```

## Layer Responsibilities

### API Layer

* Controllers
* Middleware
* Filters
* Dependency Injection Configuration
* Swagger Configuration
* Authentication Setup

### Application Layer

* DTOs
* Services
* Interfaces
* Validators
* Mapping Profiles

### Domain Layer

* Entities
* Enums
* Exceptions
* Domain Models

### Infrastructure Layer

* Entity Framework Core
* Repositories
* Unit Of Work
* Authentication Services
* Logging

---

# Database Schema

## Product

| Column      | Type          |
| ----------- | ------------- |
| Id          | int           |
| ProductName | nvarchar(255) |
| CreatedBy   | nvarchar(100) |
| CreatedOn   | datetime      |
| ModifiedBy  | nvarchar(100) |
| ModifiedOn  | datetime      |

## Item

| Column    | Type |
| --------- | ---- |
| Id        | int  |
| ProductId | int  |
| Quantity  | int  |

---

# Features

## Product Management

* Create Product
* Get Product By Id
* Get All Products
* Update Product
* Delete Product

## Item Management

* Add Item
* Get Item
* Update Item
* Delete Item

## Authentication

* JWT Access Token
* Refresh Token
* Token Rotation
* Role-Based Authorization

## Validation

* FluentValidation
* Request Validation
* Consistent Error Responses

## Logging

* Structured Logging using Serilog

## Documentation

* Swagger UI
* OpenAPI Specification

---

# API Versioning

Current API Version:

```text
v1
```

Example Route:

```http
GET /api/v1/products
```

---

# Authentication Flow

1. User submits credentials.
2. API validates credentials.
3. JWT Access Token is issued.
4. Refresh Token is issued.
5. Access Token is used for protected APIs.
6. When expired, Refresh Token generates a new Access Token.
7. Old Refresh Token becomes invalid.

---

# API Endpoints

## Authentication

### Login

```http
POST /api/v1/auth/login
```

Request

```json
{
  "username": "admin",
  "password": "Password@123"
}
```

### Refresh Token

```http
POST /api/v1/auth/refresh-token
```

Request

```json
{
  "accessToken": "token",
  "refreshToken": "refresh-token"
}
```

---

## Products

### Get All Products

```http
GET /api/v1/products
```

### Get Product By Id

```http
GET /api/v1/products/{id}
```

### Create Product

```http
POST /api/v1/products
```

Request

```json
{
  "productName": "Laptop"
}
```

### Update Product

```http
PUT /api/v1/products/{id}
```

Request

```json
{
  "productName": "Updated Laptop"
}
```

### Delete Product

```http
DELETE /api/v1/products/{id}
```

---

# Error Response Format

```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Product name is required."
  ]
}
```

---

# Running Locally

## Prerequisites

* .NET 8 SDK
* SQL Server
* Docker Desktop (Optional)

---

## Clone Repository

```bash
git clone <repository-url>
cd ProductApiAssessment
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Apply Database Migrations

```bash
dotnet ef database update \
-p src/Infrastructure \
-s src/API
```

---

## Run Application

```bash
dotnet run --project src/API
```

Application URL:

```text
https://localhost:5001
```

Swagger URL:

```text
https://localhost:5001/swagger
```

---

# Running Tests

Run all tests:

```bash
dotnet test
```

Run specific test project:

```bash
dotnet test tests/Application.Tests
```

---

# Docker Support

## Build Image

```bash
docker build -t product-api .
```

## Run Container

```bash
docker run -p 5000:8080 product-api
```

---

# Docker Compose

Start all services:

```bash
docker-compose up -d
```

Stop services:

```bash
docker-compose down
```

---

# Performance Considerations

* AsNoTracking for read-only queries
* Async/Await throughout application
* Repository Pattern
* Pagination support
* Optimized SQL queries
* Proper indexing strategy

---

# Security Measures

* JWT Authentication
* Refresh Token Rotation
* HTTPS Enforcement
* Security Headers
* CORS Configuration
* FluentValidation
* Role-Based Authorization

---

# Logging

Serilog is used for structured logging.

Logs include:

* Request Tracking
* Error Tracking
* Authentication Events
* Application Events

---

# Future Enhancements

* Redis Caching
* CQRS Pattern
* MediatR Integration
* Event-Driven Architecture
* Distributed Tracing
* Azure Deployment

---

# Author

Shivam Tiwari

Technical Assessment Submission
