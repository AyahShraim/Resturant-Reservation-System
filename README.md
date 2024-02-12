# Restaurant Reservation RESTful API

## Overview

This project implements a RESTful API for managing various entities within a restaurant reservation system. It leverages .NET 7.0, Entity Framework Core (EF Core), and JWT for authentication.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete functionality is available for the following entities:
    - **Customers**
    - **Restaurants**
    - **Employees**
    - **Reservations**
    - **Orders**
    - **Menu Items**
    - **Order Items**
    - **Tables**
- **Additional API Endpoints**:
    - **List All Managers**: Retrieve a list of all managers.
    - **Retrieve Reservations by Customer ID**: Retrieve reservations associated with a specific customer.
    - **List Orders and Menu Items for a Reservation**: Retrieve orders and menu items associated with a specific reservation.
    - **List Ordered Menu Items for a Reservation**: Retrieve ordered menu items associated with a specific reservation.
    - **Calculate Average Order Amount for an Employee**: Calculate the average order amount for a specific employee.
 
# Authorization, Validation, and Error Handling

This section focuses on ensuring the security, integrity, and reliability of the RESTful API by implementing authorization, validation, and error handling mechanisms.

- **Secure APIs with JWT**: Utilize JWT or another authorization mechanism to secure the APIs, ensuring that only authenticated users have access to protected endpoints.

- **Input Validation**: Implement model validation filters to validate incoming data, ensuring that it meets the required criteria and maintaining data integrity throughout the application.

- **Exception Handling Middleware**: Utilize middleware for exception handling to intercept and gracefully handle any runtime exceptions that may occur during API execution. This ensures that users receive user-friendly error responses, enhancing the overall user experience and facilitating troubleshooting.

# API Documentation, Testing, and Versioning

In this section, ه cover the aspects related to documenting, testing, and versioning the RESTful API.

- **API Documentation with Swagger**: Integration of Swagger to automatically generate detailed API documentation. This documentation includes information about parameters, expected responses, and possible error codes, enhancing the usability and understanding of the API.

   - **XML Documentation Integration**:
  - Utilize XML documentation within the codebase to enhance code readability and maintainability.
  - Automatically integrate XML documentation into Swagger for enriched API documentation.

- **Postman Testing**:
  - Developed a comprehensive Postman collection for testing all API endpoints.
  - The Postman collection is available in the repository for easy access.
  - Implemented environment variables in the collection for seamless testing across different environments.
  - View the public Postman documentation [here](https://documenter.getpostman.com/view/26374109/2sA2r3am2d) for detailed API endpoint documentation and testing.

- **Versioning**: Implementation of API versioning to manage changes effectively and ensure backward compatibility. This allows for the introduction of new features or modifications without impacting existing clients. 


## Getting Started

1. Ensure that the `RestaurantReservationCore` database is set up in SQL Server.
2. Run the project and navigate to the specified endpoints using tools like Postman or Swagger UI.
3. Authenticate users using the `/api/authentication/authenticate` endpoint to generate a JWT token for secure access to other endpoints.

## Contribution

Contributions to improve and enhance the API are welcome. Fork the repository, make your changes, and submit a pull request.

