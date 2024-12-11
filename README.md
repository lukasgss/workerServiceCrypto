# Functioning
The software uses CoinLore's API to retrieve cryptocurrency coins and exchanges, and displays it in a filterable grid using Vue.js with TypeScript in the front end. It does upsert functionality using a Hangfire job that executes every hour in a worker service.

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
Offers the implementations to external services like database access, external API calls, etc.

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

# Observations
The configuration files that have sensitive data where added to the repository on purpose to facilitate the database creation and testing the application in general.
