Instructions:

1.  Update the appsettings.json file so the application can connect to your database.
2.  Run the command: dotnet ef database update
3.  Start the program you should see the Swagger UI
4.  Create an account using the Register endpoint
5.  Log in at the Login endpoint with your email and password
6.  Copy the token from the login response
7.  In the top-right corner of the page, click the green Authorize
    button and paste the token

You can now access all protected endpoints. Only the GET endpoints under
the Articles section are available without a token.
