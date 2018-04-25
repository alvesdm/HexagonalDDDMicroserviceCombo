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
  - Bearer Token Authentication & Authorization with X509SecurityKey as IssuerSigningKey
  - Service dependencies' healthcheck(including Background worker), with [FluentCheck](https://github.com/alvesdm/FluentCheck)
  - No Service discovery implemented since we are very enthusiatic about docker orhcestrators like Docker Swarm and Kubertenes and both provide really nice SD feature out of the box. Now in case you want to have it, you might wanna check out this package[FluentDicovery](https://github.com/alvesdm/FluentDicovery)

License
----

MIT
