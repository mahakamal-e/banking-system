# Banking System API

This repository contains a simple .NET Core Web API for managing bank accounts. 

## Features

* **Deposit:** Deposit funds into an account.
* **Withdraw:** Withdraw funds from an account.
* **Transfer:** Transfer funds between accounts.
* **Get Balance:** Retrieve the current balance of an account.


**Prerequisites:**

* **.NET SDK:** Install the latest version from the official website
* **MySQL Server:** Install and configure a MySQL server locally or use a cloud-based service.
* **MySQL Connector/NET:** Install the necessary NuGet package:
    ```bash
    dotnet add package MySql.Data.EntityFrameworkCore
    ```

**Getting Started:**

1. **Clone the repository:**
   ```bash
   git clone <repository_url>
2. **Navigate to the project directory:**
   ```Bash
   cd BankingSystem
3. **Install dependencies:**
   ```Bash
   dotnet restore
4. **Apply database migrations:**
   ```Bash
   dotnet ef database update
5. **Run the application:**
   ```Bash
   dotnet run
## API Endpoints

* **Deposit:**
    * `POST /api/accounts/deposit`
    * Request Body:
        * `AccountId`: (int) The ID of the account to deposit to.
        * `Amount`: (decimal) The amount to deposit.
    * Example Request:
        ```json
        {
            "AccountId": 1,
            "Amount": 100.00
        }
        ```
    * Example Response:
        ```json
        {
            "message": "Deposit successful",
            "balance": 150.00 
        }
        ```

* **Withdraw:**
    * `POST /api/accounts/withdraw`
    * Request Body:
        * `AccountId`: (int) The ID of the account to withdraw from.
        * `Amount`: (decimal) The amount to withdraw.
    * Example Request:
        ```json
        {
            "AccountId": 1,
            "Amount": 50.00
        }
        ```
    * Example Response:
        ```json
        {
            "message": "Withdrawal successful",
            "balance": 50.00 
        }
        ```

* **Transfer:**
    * `POST /api/accounts/transfer`
    * Request Body:
        * `SourceAccountId`: (int) The ID of the account to transfer from.
        * `TargetAccountId`: (int) The ID of the account to transfer to.
        * `Amount`: (decimal) The amount to transfer.
    * Example Request:
        ```json
        {
            "SourceAccountId": 1,
            "TargetAccountId": 2,
            "Amount": 25.00
        }
        ```
    * Example Response:
        ```json
        {
            "message": "Transfer successful",
            "sourceBalance": 25.00,
            "targetBalance": 75.00
        }
        ```

* **Get Balance:**
    * `GET /api/accounts/{id}/balance`
    * Path Parameters:
        * `{id}`: (int) The ID of the account.
    * Example Request:
        ```
        GET /api/accounts/1/balance
        ```
    * Example Response:
        ```json
        {
            "balance": 100.00
        }
        ```
### Testing with Swagger:

After running the application, you can access the Swagger UI at http://127.0.0.1:5128/swagger/ (or the actual port your application is running on).
Swagger UI provides an interactive interface to explore and test the API endpoints.
You can easily make requests to each endpoint, view the expected request and response schemas, and see the actual responses from the API.

## Database Setup

**Configure Database:**

***Update appsettings.json:***
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_address;Database=db_name;User Id=your_username;Password=your_password;" 
  }
}

