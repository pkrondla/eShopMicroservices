# eShop Microservices

A modern, cloud-native e-commerce microservices solution built with .NET 8, demonstrating best practices in microservices architecture, CQRS, event-driven communication, and containerization.

## 🏗️ Architecture Overview

This solution implements a microservices architecture for an e-commerce platform with the following key components:

- **API Gateway** - YARP reverse proxy for routing and rate limiting
- **Microservices** - Catalog, Basket, Ordering, and Discount services
- **Web Application** - Razor Pages frontend
- **Message Broker** - RabbitMQ for asynchronous communication
- **Databases** - PostgreSQL, SQL Server, SQLite, and Redis

## 📦 Services

### 1. **Catalog.API**
Product catalog management service
- **Technology**: ASP.NET Core 8, Marten (PostgreSQL), Carter
- **Database**: PostgreSQL
- **Features**:
  - Product CRUD operations
  - Product search and filtering
  - CQRS pattern with MediatR
  - Health checks

### 2. **Basket.API**
Shopping cart management service
- **Technology**: ASP.NET Core 8, Marten, Redis, gRPC, MassTransit
- **Databases**: PostgreSQL (persistence), Redis (caching)
- **Features**:
  - Shopping cart operations
  - Redis caching for performance
  - gRPC communication with Discount service
  - Basket checkout event publishing
  - Health checks

### 3. **Ordering.API**
Order management service with Clean Architecture
- **Technology**: ASP.NET Core 8, Entity Framework Core, MassTransit
- **Database**: SQL Server
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API layers)
- **Features**:
  - Order CRUD operations
  - Domain-driven design (DDD)
  - Domain events
  - Integration event handling (Basket checkout)
  - Feature flags support
  - Health checks

### 4. **Discount.Grpc**
Discount calculation service using gRPC
- **Technology**: ASP.NET Core 8, gRPC, Entity Framework Core
- **Database**: SQLite
- **Features**:
  - High-performance discount calculations
  - gRPC for inter-service communication

### 5. **YarpApiGateway**
API Gateway using YARP (Yet Another Reverse Proxy)
- **Technology**: ASP.NET Core 8, YARP
- **Features**:
  - Request routing to backend services
  - Rate limiting
  - Single entry point for all services

### 6. **Shopping.Web**
Frontend web application
- **Technology**: ASP.NET Core Razor Pages, Refit
- **Features**:
  - Product browsing
  - Shopping cart management
  - Order placement
  - Integration with backend services via API Gateway

## 🛠️ Technology Stack

### Core Technologies
- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core** - Web framework
- **Docker** - Containerization
- **Docker Compose** - Multi-container orchestration

### Design Patterns & Libraries
- **CQRS** - Command Query Responsibility Segregation (MediatR)
- **DDD** - Domain-Driven Design (Ordering service)
- **MediatR** - Mediator pattern implementation
- **Carter** - Minimal API framework
- **FluentValidation** - Input validation
- **Mapster** - Object mapping

### Data Access
- **Marten** - PostgreSQL document database for .NET
- **Entity Framework Core** - ORM for SQL Server and SQLite
- **Redis** - Distributed caching
- **PostgreSQL** - Relational database
- **SQL Server** - Relational database
- **SQLite** - Lightweight database

### Communication
- **gRPC** - High-performance RPC framework
- **MassTransit** - Message bus abstraction
- **RabbitMQ** - Message broker
- **YARP** - Reverse proxy and load balancer

### Cross-Cutting Concerns
- **Health Checks** - Service health monitoring
- **Exception Handling** - Global exception handling
- **Logging** - Structured logging
- **Validation** - Request validation pipeline

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for Windows/Mac) or Docker Engine (for Linux)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Running the Solution

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/eShopMicroservices.git
   cd eShopMicroservices
   ```

2. **Navigate to the source directory**
   ```bash
   cd src
   ```

3. **Start all services using Docker Compose**
   ```bash
   docker-compose up -d
   ```

   This will start:
   - All database containers (PostgreSQL, SQL Server, Redis, SQLite)
   - RabbitMQ message broker
   - All microservices
   - API Gateway
   - Web application

4. **Access the application**
   - **Web Application**: http://localhost:6005
   - **API Gateway**: http://localhost:6004
   - **RabbitMQ Management**: http://localhost:15672 (guest/guest)
   - **Catalog API**: http://localhost:6000
   - **Basket API**: http://localhost:6001
   - **Ordering API**: http://localhost:6003
   - **Discount gRPC**: http://localhost:6002

### Running Individual Services

To run services individually for development:

```bash
# Catalog Service
cd Services/Catalog/Catalog.API
dotnet run

# Basket Service
cd Services/Basket/Basket.API
dotnet run

# Ordering Service
cd Services/Ordering/Ordering.API
dotnet run

# Discount gRPC Service
cd Services/Discount/Discount.Grpc
dotnet run

# API Gateway
cd ApiGateways/YarpApiGateway
dotnet run

# Web Application
cd WebApps/Shopping.Web
dotnet run
```

## 📡 API Endpoints

### Catalog Service
- `GET /catalog-service/products` - Get all products
- `GET /catalog-service/products/{id}` - Get product by ID
- `GET /catalog-service/products/category/{category}` - Get products by category
- `POST /catalog-service/products` - Create product
- `PUT /catalog-service/products` - Update product
- `DELETE /catalog-service/products/{id}` - Delete product

### Basket Service
- `GET /basket-service/basket/{userName}` - Get shopping cart
- `POST /basket-service/basket` - Store/update shopping cart
- `DELETE /basket-service/basket/{userName}` - Delete shopping cart
- `POST /basket-service/basket/checkout` - Checkout basket (publishes event)

### Ordering Service
- `GET /ordering-service/orders` - Get all orders
- `GET /ordering-service/orders/customer/{customerId}` - Get orders by customer
- `GET /ordering-service/orders/name/{orderName}` - Get orders by name
- `POST /ordering-service/orders` - Create order
- `PUT /ordering-service/orders` - Update order
- `DELETE /ordering-service/orders/{id}` - Delete order

### Health Checks
All services expose health check endpoints at `/health`

## 🏛️ Architecture Patterns

### CQRS (Command Query Responsibility Segregation)
- Commands and Queries are separated
- MediatR handles command/query routing
- Validation and logging behaviors applied via pipeline

### Event-Driven Architecture
- **Integration Events**: Cross-service communication via RabbitMQ
  - `BasketCheckoutEvent` - Published when basket is checked out
- **Domain Events**: Within-service domain logic
  - `OrderCreatedEvent` - Published when order is created
  - `OrderUpdatedEvent` - Published when order is updated

### Clean Architecture (Ordering Service)
- **Domain Layer**: Core business logic, entities, value objects, domain events
- **Application Layer**: Use cases, commands, queries, DTOs
- **Infrastructure Layer**: Data access, external services
- **API Layer**: Controllers, endpoints, dependency injection

### Repository Pattern
- Abstracted data access
- Cached repository decorator pattern (Basket service)

## 🗄️ Database Configuration

### PostgreSQL
- **Catalog DB**: Port 5432
  - Database: `CatalogDb`
  - User: `postgres`
  - Password: `postgres`
- **Basket DB**: Port 5433
  - Database: `BasketDb`
  - User: `postgres`
  - Password: `postgres`

### SQL Server
- **Order DB**: Port 1433
  - Database: `OrderDb`
  - User: `sa`
  - Password: `SwN12345678`

### Redis
- Port: 6379
- Used for distributed caching in Basket service

### SQLite
- Used by Discount.Grpc service
- File-based database

## 🔧 Configuration

### Environment Variables
Services can be configured via environment variables or `appsettings.json` files:

- `ConnectionStrings:Database` - Database connection string
- `ConnectionStrings:Redis` - Redis connection string
- `GrpcSettings:DiscountUrl` - Discount gRPC service URL
- `MessageBroker:Host` - RabbitMQ connection string
- `MessageBroker:UserName` - RabbitMQ username
- `MessageBroker:Password` - RabbitMQ password
- `ApiSettings:GatewayAddress` - API Gateway URL (Web app)

### Feature Flags
Ordering service supports feature flags:
- `FeatureManagement:OrderFullfilment` - Enable/disable order fulfillment feature

## 🧪 Building Blocks

The solution includes shared building blocks:

### BuildingBlocks
- **CQRS Interfaces**: `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`
- **Behaviors**: `ValidationBehavior`, `LoggingBehavior`
- **Exceptions**: Custom exception types
- **Pagination**: Pagination support

### BuildingBlocks.Messaging
- **Integration Events**: Base integration event class
- **MassTransit Extensions**: Message broker configuration helpers

## 🐳 Docker

All services include Dockerfiles and are configured for containerization. The `docker-compose.yml` file orchestrates all services and dependencies.

### Building Images
```bash
docker-compose build
```

### Viewing Logs
```bash
docker-compose logs -f [service-name]
```

### Stopping Services
```bash
docker-compose down
```

## 📝 Project Structure

```
eShopMicroservices/
├── src/
│   ├── ApiGateways/
│   │   └── YarpApiGateway/          # API Gateway
│   ├── BuildingBlocks/
│   │   ├── BuildingBlocks/          # Shared CQRS, behaviors, exceptions
│   │   └── BuildingBlocks.Messaging/ # Messaging infrastructure
│   ├── Services/
│   │   ├── Basket/
│   │   │   └── Basket.API/          # Shopping cart service
│   │   ├── Catalog/
│   │   │   └── Catalog.API/         # Product catalog service
│   │   ├── Discount/
│   │   │   └── Discount.Grpc/       # Discount gRPC service
│   │   └── Ordering/
│   │       ├── Ordering.API/        # Order API layer
│   │       ├── Ordering.Application/ # Order application layer
│   │       ├── Ordering.Domain/     # Order domain layer
│   │       └── Ordering.Infrastructure/ # Order infrastructure layer
│   ├── WebApps/
│   │   └── Shopping.Web/            # Frontend web application
│   ├── docker-compose.yml           # Docker Compose configuration
│   └── eshop-microservices.sln      # Solution file
└── README.md
```

## 🔐 Security Considerations

- HTTPS is configured for all services
- Rate limiting implemented on Ordering service via API Gateway
- Input validation using FluentValidation
- Exception handling to prevent information leakage

## 🚧 Future Enhancements

Potential improvements and features:
- Authentication and authorization (JWT, OAuth2)
- API versioning
- Distributed tracing (OpenTelemetry)
- Service mesh integration
- Kubernetes deployment manifests
- Unit and integration tests
- CI/CD pipeline configuration
- Monitoring and observability (Prometheus, Grafana)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**pkrondla**

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check the issues page.

## 🙏 Acknowledgments

- Built with modern .NET technologies
- Inspired by microservices best practices
- Uses open-source libraries and frameworks

---

⭐ If you find this project helpful, please consider giving it a star!
