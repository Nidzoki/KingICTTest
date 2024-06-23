# KingICT test project

### Description
This is a middleware that I had created for KingICT test project. It includes authorization and product endpoints. This project uses https://dummyjson.com/ API for products and its own database for users.
### Running the database and server
For database I had used SQL Server Express 2022 Developer Edition that I downloaded from https://www.microsoft.com/en-us/sql-server/sql-server-downloads. 

Also, I had used SQL Server Management Studio (SSMS) from https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16, though it isn't necessary to run the app, but it gives more info about the current state of the database. 

In the project folder in appsettings.json, there is an object field `"DefaultConnection"`. It contains the conection data which is needed for project to run properly. You need to replace the MSQLSERVER01 part of the string with your own name of the SQL server(if it's different). The name of SQL server can be found in **Start -> SQL Server Configuration Manager -> SQL Server Services** or **Start -> Run -> MMC -> File -> SQLServerManager16**.
There will be at least one SQL server that has running state. In the brackets, there will be full name of the server written like this `SQL Server (<Name of server>)`.

For server to run you have to be in project directory (the one that contains `Program.cs`). Open new terminal in that directory and type in the `dotnet run` command. If everything is done correctly, there will be a Swagger html page running on http://localhost:5259/swagger/index.html where you can test the endpoints.

#### Notice
You have to be authorized for product endpoints, so you'll have to use the register endpoint to create new user, then log in with the same credentials. Login endpoint will return Bearer token string which you have to copy and paste in the value field in form that pops up when you click Authorize button on the top of the page. You'll have to add `Bearer` before the copied string for thist to work, so it'll look like this `Bearer <Bearer token string>`. Once you are authorized, you'll have access to the product endpoints. There is an easier way for achieving the same result. You can set useCookies to true in login endpoint and then the middleware will take care of the rest.
