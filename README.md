# Car Adverts API Service

The service based on REST and provide an interface for car adverts data.

## API

Car Adverts REST API: `/api/carAdvert`

SwaggerUI Documentation: `/swagger`

Swagger Specification: `/swagger/v1/swagger.json`

## Security aspects

CORS protocol is enabled for the API in order to support cross-origin requests.

There is not authentication/authorisation for the API.

## Architecture

The application consists of 2 layers: Controller and Repository. No service layer needed as
long as the API implement basic CRUD operations and does not have a bissness logic. 

## Environments

Currently there is only development environment with In-Memory Database. The integration tests are also using In-Memory database.
