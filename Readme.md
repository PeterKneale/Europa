# Europa Overview
A sample dotnet core 2.0 application demonstrating messaging, CQRS and microservices. 

## Architecture
- Web: ASP.NET Core MVC 
- Micro-services: CQRS Style commands and queries
  
### Write Service
- Writes are made to a Postgres database 
- Dapper is used for sql execution
- In a master-slave arrangement these changes would be written to the master

### Query Service
- Reads are made against postgres database views
- Dapper is used for sql execution
- In a master-slave arrangement these reads would be made against the slave

### Search Service
- In response to events occuring, a Solr search index is updated.
- In response to queries, Solr is searched and its results returned.
  
### Feed Service
- Handles podcast feed retrieval / refreshes.

## Messaging Infrastructure
- Messages are defined as 
  - IEvent: Raised to commicate that an event has occurred within the domain.
  - ICommand: Send to change somthing in the domain.
  - IQuery: Requested to retrieve information about the domain

- Dispatchers dispatch messages to the rabbit bus 
  - ICommandDispatcher, IQueryDispatcher and IEventDispatcher

- Executors execute a message that has been received.
  - Starting a new scope
  - Resolving the appropriate handler
  - Executing the handler by passing in the message to be handled.

## Other Infrastructure
- Database maintenance performed using FluentMigrator
- Message validation performed using FluentValidation
- Unit of Work implementation to perform multiple Dapper Sql operations in the context of a transaction.
- Unit testing using xUnit, Fluent Assertions and Moq.

## Nuget Packages Used
- Messaging
  - EasyNetQ
  - RabbitMQ
- Infrastructure
  - Autofac
  - Autofac.Extensions.DependencyInjection
  - Dapper
  - Dapper.SimpleCRUD.NetCore
  - FluentMigrator
  - FluentMigrator.Runner
  - Npgsql
- Testing
  - FubarDev.NDbUnit.Core
  - FubarDev.NDbUnit.Postgresql
  - FluentAssertions
  - FluentAssertions.AspNetCore.Mvc
  - Moq                        
- Web
  - AutoMapper
  - FluentValidation.AspNetCore

# Instructions for running

## Run Application via docker compose
```
docker-compose up --build
```

## Run Tests via docker compose
```
docker-compose -f docker-compose.test.yml up --build --abort-on-container-exit
```

### Stop and remove all containers
```
docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
```

# Related Links
- Download .NET Core 2 https://www.microsoft.com/net/download/core
- DockerHub images 
  - https://hub.docker.com/r/microsoft/dotnet/
  - https://hub.docker.com/r/microsoft/aspnetcore/
- Related Blog Posts
  - http://udidahan.com/2009/12/09/clarified-cqrs/
  - https://martinfowler.com/bliki/CQRS.html
