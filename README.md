# Minimal API with JWT Auth, Swagger, Api Versioning and HealthChecks with UI.
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
<a href="https://localhost:7101/healthcheck">https://localhost:7101/ready</a><br />

<img src="https://github.com/user-attachments/assets/925ef2b6-e913-4386-b2a5-41e96505ad63" width="500"><br />

<br />
<b>Healthcheck live for use in containers</b><br />

<img src="https://github.com/user-attachments/assets/6c2ef6d4-f4cf-499e-8007-e0c9edb4c038" width="500"><br />

<br />
<b>Healthcheck UI</b><br />

<img src="https://github.com/user-attachments/assets/c89553ef-d7e5-4eba-b9b8-0b3e25e85424" width="500"><br />

<br />
<b>Healthcheck UI - webhooks</b><br />

<img src="https://github.com/user-attachments/assets/14c40144-5404-4fda-b005-39bb99c8cf9c" width="500"><br />

<br />
<b>Healthcheck UI - webhooks reporting</b><br />

<img src="https://github.com/user-attachments/assets/7bdcff9d-fa20-4ee6-bad6-852055148364" width="500"><br />
