# HexagonalDDDMicroserviceTemplate
A simple Microservice template in .net Core implementing DDD and Hexagonal Architecture

In this template you'll find:
  - .net core 2.0
  - API
  - Worker(windows service? linux daemon? ...your choice! It's .net core, after all :p)
  - Hexagonal architecture best practices;
  - DDD
  - Domain events with [MediatR](https://github.com/jbogard/MediatR)
  - Automatic domain validation using [FluentValidation](https://github.com/JeremySkinner/fluentvalidation)
  - API Model validation with [FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/) integrated in the pipeline
  - Fully injected dependencies with [Autofac](https://github.com/autofac/Autofac)
  - Mediator patttern with [MediatR](https://github.com/jbogard/MediatR)
  - Error handling with [pipeline middlewares](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?tabs=aspnetcore2x)
  - Messaging broker with rabbitMQ and [RabbitHole](https://github.com/alvesdm/RabbitHole)

License
----

MIT
