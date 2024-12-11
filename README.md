# Functioning
The software uses CoinLore's API to retrieve cryptocurrency coins and exchanges, and displays it in a filterable grid or in an individual page for the coin, everything made using Vue.js with TypeScript. It does upsert functionality using a Hangfire job that executes every hour in a worker service.

The documentation of CoinLore's API used to retrive the data can be found [here](https://www.coinlore.com/cryptocurrency-data-api).

# Architecture

The software was based on Clean Architecture following DDD principles and was divided in 4 layers, being API, Application, Domain and Infrastructure.

## API
This layer is responsible for exposing all the application functionalities to the users. It routes requests, serializes and deserializes data and interacts with the Application layer calling its services.

## Application
It's responsible for defining the jobs needed to be done to accomplish a certain application task, calling the domain layer for business rules and Infrastructure layer for external services.

## Domain
It's the core of the application, where all business rules are defined. It cointains the domain entities and business rules that operate on these entities. Since this is a very simple project, there aren't many business rules in the model, but it gets increasingly more important as the domain complexity increases.

## Infrastructure
Offers the implementations to external services like external API calls, database access (SQL Server), etc.

Given these layers, this is how the project dependencies look like:

\
![image](https://github.com/user-attachments/assets/f9c903a8-5a59-4d0d-9482-4c14187451e6)


# Running the project
For simplicity reasons, this project does not include docker and docker compose files to configure the environment, but it would a very good option to initialize all the services needed for the application to function properly.

Before running the project, it's necessary to execute the migrations. To do that, go into the Infrastructure layer and type the following command:
```bash
dotnet ef --startup-project ../Api/Api.csproj database update
```

After that, you can run the project's API and the worker service.

# Unit tests
There are a few unit tests to the domain entities, making sure they all guarantee their [invariancies](https://ddd-practitioners.com/home/glossary/business-invariant/), are always in a valid state and executes their business logic correctly. The tests were implemented using xUnit, and you can execute them by simply running:
```bash
dotnet test
```

# Worker Service
The worker service project retrieves the data from CoinLore's API, processes them to the format the API I built expects and send them on their respective endpoint every hour, with the schedule done by Hangfire. The endpoints it calls to upsert the data are: 

```POST /api/coins```
and 
```POST /api/exchanges```

# Observations
- The configuration files that have sensitive data were added to the repository on purpose to facilitate the database creation and testing the application in general;
- For simplicity reasons, it wasn't added any kind of authorization to the upsert endpoints, potentially allowing other unauthorized users to insert data in the application. That would be something to kep in mind if this application was ever going to production.
