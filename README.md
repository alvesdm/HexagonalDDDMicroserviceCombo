[![Build status](https://ci.appveyor.com/api/projects/status/5c0t3d9lx0ug0ier?svg=true)](https://ci.appveyor.com/project/alvesdm/hexagonaldddmicroservicetemplate)

# Hexagonal(DDD) Microservice Template
A simple Microservice template with asp.net Core 2.0 implementing Hexagonal(DDD) Architecture

In this template you'll find:
  - asp.net core 2.0;
  - An API so that other microservices can communicate with it;
  - Worker(windows service? linux daemon? ...your choice! It's .net core, after all :p) with [PeterKottas DotNetCore.WindowsService](https://github.com/PeterKottas/DotNetCore.WindowsService) package that offers most(if not all) features you would find in TopShelf which unfirtunately is not .net core friendly yet;
  - Hexagonal architecture best practices;
  - DDD;
  - Domain events with [MediatR](https://github.com/jbogard/MediatR);
  - Automatic domain validation using [FluentValidation](https://github.com/JeremySkinner/fluentvalidation);
  - API Model validation with [FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/) integrated in the pipeline;
  - Fully injected dependencies with [Autofac](https://github.com/autofac/Autofac);
  - Mediator patttern with [MediatR](https://github.com/jbogard/MediatR) to provide a nice, clean and non-'fat controllers';
  - Error handling with [pipeline middlewares](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?tabs=aspnetcore2x);
  - Messaging broker with rabbitMQ and [RabbitHole](https://github.com/alvesdm/RabbitHole). Masstransit is awesome but not .net core friendly yet;
  - JWT Bearer Token Authentication & Authorization with X509SecurityKey as IssuerSigningKey;
  - Service dependencies' healthcheck(including Background worker), with [FluentCheck](https://github.com/alvesdm/FluentCheck);
  - No Service discovery implemented since I'm very enthusiatic about docker orhcestrators like Docker Swarm and Kubertenes and both provide really nice SD feature out of the box. Now in case you want to have it, you might wanna check out this package [FluentDicovery](https://github.com/alvesdm/FluentDicovery).
  
Alright mate, hope you enjoy it. 
If you happen to use it and it's working like a charm in you production environment, let me know about your happy story.

License
----

MIT
