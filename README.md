# ToDoApi

This demo showcases a few things:

- The Swagger UI that represents the backing set of API endpoints
- A Website that utilizes a few frontend components to leverage the API
- A solution structure that separates Data, Service, and Web API components
- A few unit tests to demonstrate how mocking can be used with the existing services

While running, the `DataAccess` project serves as the persistent datasource, but the
general designs could be swapped with an EFCore DbContext or other similar system

The frontend site utilizes a lesser known library (Knockout.js) as it's been awhile
having to do that work and that was the easiest to spinup something functional. It
uses jQuery and Bootstrap for additional functionality.

The unit tests are definitely not all encompassing or comprehensive.  The basic
idea is to show the general concepts of mocking the underlyng services and 
Fluent Assertions could be used.
