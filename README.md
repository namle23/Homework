# Homework

Description:
-Solution for If Insurance's developer position.

Brief:
-Contains 2 projects. Homework.Api is the actual API and Homework.Api.Tests includes unit tests for Homework.Api.
-Homework.Api is the prerequisite and need to be at running state, ready to accept requests from front-end.
-Homework.Api contains 1 single GET endpoint, /api/products.

How to run:
-Easiest is to open the solution file Homework.sln with Visual Studio and run from there, OR

Homework.Api
-Navigate to Homework.Api directory, run command in terminal: dotnet run
The following similar info should be shown:
info: Microsoft.Hosting.Lifetime[14]
Now listening on: http://localhost:5156

, copy the address including the port to use in front-end.

Homework.Api.Test
-Navigate to root solution directory, run command in terminal: dotnet test
