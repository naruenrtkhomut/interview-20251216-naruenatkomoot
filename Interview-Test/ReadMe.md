# Question
- Implement method `InvokeAsync` in [AuthenMiddleware.cs](#question) file to validate the `x-api-key` 
from request header. So your must be hashed the `x-api-key` by using SHA512 algorithm before you compare to the `hashedKey` in [AuthenMiddleware.cs](#question) file as below:
```csharp
private const string xApiKey = "<your hashed sha512 x-api-key>";
```
and return `401 Unauthorized` if the `x-api-key` is invalid.

- Implement design database schema by using `Code-First and automated migration` approach in [InterviewTestDbContext.cs](#question) file to your database for the following requirement:
  - User can have multiple roles.
  - Role can have multiple permissions.

**Important Note:** Please use `SQL Server` for your database schema design.

- Implement method `CreateUser` in [UserRepository.cs](#question) file and return affected row when execute `SaveChanges()` 
to create a new user by using `Entity Framework` and to create the user please use the data from `Data.cs` file.


- Implement method `GetUserById` in [UserRepository.cs](#question) file by using `Linq` to return the model similar to the `ExpectResult1.json` and `ExpectResult2.json`
as following:


  `ExpectResult1.json`
```json
{
  "id": "02CE43A4-A378-4B30-B52E-227EFA6B696E",
  "userId": "user01",
  "username": "John.D.Smith",
  "firstName": "John",
  "lastName": "Smith",
  "age": null,
  "roles": [
    {
      "roleId": 1,
      "roleName": "pick operation"
    },
    {
      "roleId": 2,
      "roleName": "pack operation"
    },
    {
      "roleId": 3,
      "roleName": "document operation"
    }
  ],
  "permissions": [
    "1-01-picking-info",
    "1-02-picking-start",
    "1-03-picking-confirm",
    "1-04-picking-report",
    "2-01-packing-info",
    "2-02-packing-start",
    "2-03-packing-confirm",
    "2-04-packing-report",
    "3-01-printing-label"
  ]
}
```

`ExpectResult2.json`
```json
{
  "id": "F90810B6-E017-431A-9DAE-A4BA7F9BC865",
  "userId": "user02",
  "username": "Bob.M.Jackson",
  "firstName": "Bob",
  "lastName": "Jackson",
  "age": 28,
  "roles": [
    {
      "roleId": 3,
      "roleName": "document operation"
    }
  ],
  "permissions": [
    "1-04-picking-report",
    "2-04-packing-report",
    "3-01-printing-label"
  ]
}
```

- Implement API by using `dependency injection` for interface `IUserRepositoy` file to return the model similar to the `ExpectResult1.json` and `ExpectResult2.json` as following:
  - `GET /api/user/GetUserById/{id}` in `UserController` file. as below:

```csharp
[HttpGet("GetUserById/{id}")]
public ActionResult GetUserById(string id)
{
    //Todo: Implement this method
    return Ok();
}
```

- Implement API by using `dependency injection` for interface `IUserRepositoy` file to create user from `Data.cs` file as following:
  - `Post /api/user/CreateUser` in `UserController` file. as below:

```csharp
[HttpPost("CreateUser")]
public ActionResult CreateUser(UserModel user)
{
    //Todo: Implement this method
    return Ok();
}
```

- Implement gateway configuration in `configurationOcelot.json` file by using library `Ocelot` 
to  forward the client request to `Interview-Test.Api(https://localhost:44307)` with using the domain `https://localhost:44375/gateway`


# Interview-Test.Client (Frontend Angular)

- Implement screen Users List

![Example Users List screen](../Interview-Test/Interview-Test.Client/src/assets/list.png)

- Implement screen Users Detail 

![Example Users Detail screen](../Interview-Test/Interview-Test.Client/src/assets/detail.png)

- Connect data from API Gateway Ocelot.