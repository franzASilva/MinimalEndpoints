# Minimal API with JWT Auth, Swagger, Api Versioning and HealthChecks.
**For testing purposes only, pre-created Roles Admin and User and user admin with password 123**
- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview);
- [JWT](https://jwt.io/)

<br />
<b>Swagger and Api Versioning with JWT Auth</b><br />
<img src="https://github.com/user-attachments/assets/e27ef42f-56fa-4661-b2c8-de2786e02218" width="500"><br />
<img src="https://github.com/user-attachments/assets/107c50d7-3506-45c8-8925-5691fb0df936" width="500"><br />

<br />
<b>Using Swagger and JWT Auth</b><br />
When trying to use an api that requires auth without a token, you receive the 401 Unauthorized error<br />

<img src="https://github.com/user-attachments/assets/abf5f7f9-8ecc-44f2-b0c3-1c16da35f578" width="500"><br />

User admin and password 123 to get a token<br />

<img src="https://github.com/user-attachments/assets/46ab4b8a-c2e2-408f-8224-c8258270615b" width="500"><br />

Use token to authorize<br />

<img src="https://github.com/user-attachments/assets/913129d9-5757-4252-9993-1fe1a1fb102a" width="500"><br />

Now... 200 OK<br />

<img src="https://github.com/user-attachments/assets/cc9a55a4-7c51-4d1e-a425-d5e9881f7dcc" width="500"><br />

Use the user API to create new users and update then with a role.<br />
We can only use DummyEndpoint with Admin or User roles, while in UserEndpoint we only need a valid token<br />

<img src="https://github.com/user-attachments/assets/72c39cce-f055-4453-bcc0-b9ce5ba8f4ff" width="500"><br />

<img src="https://github.com/user-attachments/assets/1e2ec399-b5c1-4ac7-b0ce-763099e99a66" width="500"><br />

<br />
<b>Healthcheck with some details</b><br />
<a href="https://localhost:7101/healthcheck">https://localhost:7101/healthcheck</a><br />

<img src="https://github.com/user-attachments/assets/019a8079-f7e7-4236-914f-701cd2e94eae" width="500">
