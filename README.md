# Phone Book Technical Test

REST API for managing contacts in a phone book, developed as part of a technical assessment for an FMS fleet management platform.

## Author

**Valentina Cabas**

## Technologies

* **C# / ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **Npgsql**
* **Swagger / OpenAPI**
* **Git / GitHub**

## Architecture

The project uses a layered architecture to separate responsibilities:

```text
C:.

│   .gitignore
│   appsettings.Development.json
│   appsettings.json
│   backend.csproj
│   backend.http
│   Program.cs
│
├───Controllers
│       ContactsController.cs
│
├───Data
│       AppDbContext.cs
│
├───DTOs
│       CreateContactDto.cs
│       UpdateContactDto.cs
│
├───Enums
│       ContactStatus.cs
│       ContactType.cs
│
├───Migrations
│
├───Models
│       Contact.cs
│
├───Properties
│       launchSettings.json
│
├───Repositories
│   │   ContactRepository.cs
│   │
│   └───Interfaces
│           IContactRepository.cs
│
└───Services
    │   ContactService.cs
    │
    └───Interfaces
            IContactService.cs
```

### Application Flow

```text
HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository
     ↓
Entity Framework Core
     ↓
PostgreSQL
```

Each layer has a specific responsibility:

* **Controller:** receives HTTP requests and returns responses.
* **Service:** contains the business logic.
* **Repository:** handles database access.
* **DTOs:** define the data received when creating and updating contacts.
* **Models:** represent the application's entities.
* **Data:** contains the Entity Framework Core configuration.

## Features

The API currently supports:

* Retrieve all active contacts.
* Retrieve a contact by ID.
* Create contacts.
* Update contacts.
* Delete contacts using **Soft Delete**.
* Manage contact types using enums.
* Manage `Active` and `Inactive` statuses.
* Document and test endpoints using Swagger.

### Contact Types

The API supports three contact types:

```text
Person
PublicOrganization
PrivateOrganization
```

### Contact Status

```text
Active
Inactive
```

Deleted contacts are not physically removed from the database. Instead, their status is changed from `Active` to `Inactive`.

Inactive contacts are not returned in normal queries.

## Endpoints

### Get Contacts

```http
GET /api/contact
```

Returns all active contacts.

### Get Contact by ID

```http
GET /api/contact/{id}
```

Returns an active contact by its ID.

If the contact does not exist or is inactive, the API returns `404 Not Found`.

### Create Contact

```http
POST /api/contact
```

### Update Contact

```http
PUT /api/contact/{id}
```

Updates the information of an active contact.

### Delete Contact

```http 
DELETE /api/contact/{id}
```

Performs a **Soft Delete**, changing the contact status to `Inactive`.

![alt text](image-1.png)

## Database

The project uses PostgreSQL together with Entity Framework Core.

The main entity is:

```text
Contacts
```

Some of its main fields are:

```text
Id
ContactType
Name
LastName
PhoneNumber
Comments
Email
GovernmentLevel
Industry
Status
CreatedAt
UpdatedAt
```

The `ContactType` and `ContactStatus` enums are stored as text in PostgreSQL to keep their values readable.

![alt text](image.png)

## Project Setup and Execution

### 1. Clone the Repository

Clone the repository from GitHub:

```powershell
git clone https://github.com/valncabs/phone-book-technical-test.git
```

Navigate to the project folder:

```powershell
cd phone-book-technical-test
```

### 2. Navigate to the Backend

```powershell
cd backend
```

### 3. Verify the Requirements

Make sure the .NET SDK is installed:

```powershell
dotnet --version
```

PostgreSQL must also be installed and running.

### 4. Create the Database

Using PostgreSQL or pgAdmin, create a database named:

```text
phonebook
```

### 5. Configure the PostgreSQL Connection

Open:

```text
backend/appsettings.json
```

Configure the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=phonebook;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Replace `YOUR_PASSWORD` with the password configured for your PostgreSQL user.

> For security reasons, real passwords should not be committed to the repository.

### 6. Restore Dependencies

From the `backend` folder, run:

```powershell
dotnet restore
```

This command downloads the project's required dependencies.

### 7. Apply Database Migrations

Run:

```powershell
dotnet ef database update
```

This creates or updates the database tables using Entity Framework Core migrations.

### 8. Build the Project

Verify that the project builds successfully:

```powershell
dotnet build
```

Expected result:

```text
Build succeeded.
```

### 9. Run the API

Start the server:

```powershell
dotnet run
```

The API will be available at:

```text
http://localhost:5260
```

Do not close the terminal while the API is running.

### 10. Open Swagger

With the API running, open the following URL in your browser:

```text
http://localhost:5260/swagger
```

Swagger allows you to view and test the available API endpoints.

### 11. Test the Endpoints

The following operations can be tested through Swagger:

```text
GET     /api/Contact
GET     /api/Contact/{id}
POST    /api/Contact
PUT     /api/Contact/{id}
DELETE  /api/Contact/{id}
```

### Complete Setup Flow

For an already configured environment, the basic backend execution flow is:

```powershell
cd phone-book-technical-test
cd backend
dotnet restore
dotnet ef database update
dotnet build
dotnet run
```

Then open:

```text
http://localhost:5260/swagger
```


