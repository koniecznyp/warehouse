## Product warehouse
[![Build Status](https://travis-ci.org/koniecznyp/warehouse.svg?branch=master)](https://travis-ci.org/koniecznyp/warehouse)

A small project application written in .NET Core 3.1 for simplified products management.

## How to run?
Service can be started locally via `dotnet run` command executed in the `/src/Warehouse.Api` directory. You can also start application using the docker. For this you will need a docker file available [here](https://github.com/koniecznyp/warehouse/blob/master/compose/docker-compose.yml). In order to start it, execute:

`docker-compose up -d` (`-d` will run containers in the background).

After starting the application it will be available under [http://localhost:5001](http://localhost:5001)

