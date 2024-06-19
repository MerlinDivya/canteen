
 Canteen Management System

 Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

 Introduction

The Canteen Management System is a web application designed to manage the operations of a canteen. It includes functionalities for managing customers, issues, and receipts, making it easier to handle day-to-day operations efficiently.

 Features

- Add, edit, delete, and view customers.
- Add, edit, delete, and view issues.
- Add, edit, delete, and view receipts.
- Display all records in a user-friendly format.
- Error handling and validation.

Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- HTML/CSS
- JavaScript

 Getting Started

 Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET Core SDK installed on your machine.
- SQL Server or any other supported database.
- Visual Studio or any other code editor.

 Installation

1. Clone the repository:

```bash
git clone https://github.com/your-username/canteen-management-system.git
```

2. Navigate to the project directory:

```bash
cd canteen-management-system
```

3. Restore the dependencies:

```bash
dotnet restore
```

4. Update the database connection string:

Open `appsettings.json` and update the connection string to point to your SQL Server instance.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  }
}
```

5. Apply database migrations:

```bash
dotnet ef database update
```

6. Run the application:

```bash
dotnet run
```

 Usage

- Navigate to `https://localhost:5001` in your web browser.
- Use the navigation menu to access different sections of the application.

Project Structure

```plaintext
.
├── canteen.UI/              # ASP.NET Core MVC application
│   ├── Controllers/         # Controllers for handling requests
│   ├── Models/              # Domain models
│   ├── Views/               # Razor views for UI
│   ├── wwwroot/             # Static files
│   └── appsettings.json     # Configuration file
├── canteen.Data/            # Data access layer
│   ├── DataAccess/          # Data access classes
│   ├── Models/              # Entity models
│   └── Repository/          # Repository classes
├── canteen.sln              # Solution file
└── README.md                # Project documentation
```

 API Endpoints

 CustomerController

- `GET /Customer/Add` - View to add a new customer.
- `POST /Customer/Add` - Add a new customer.
- `GET /Customer/Edit/{id}` - View to edit an existing customer.
- `POST /Customer/Edit` - Update an existing customer.
- `GET /Customer/DisplayAll` - Display all customers.
- `POST /Customer/Delete/{id}` - Delete a customer.

 IssueController

- `GET /Issue/Add` - View to add a new issue.
- `POST /Issue/Add` - Add a new issue.
- `GET /Issue/Edit/{id}` - View to edit an existing issue.
- `POST /Issue/Edit` - Update an existing issue.
- `GET /Issue/DisplayTable` - Display all issues.
- `POST /Issue/Delete/{id}` - Delete an issue.

ReceiptsController

- `GET /Receipts/Add` - View to add a new receipt.
- `POST /Receipts/Add` - Add a new receipt.
- `GET /Receipts/Edit/{id}` - View to edit an existing receipt.
- `POST /Receipts/Edit` - Update an existing receipt.
- `GET /Receipts/DisplayAll` - Display all receipts.
- `POST /Receipts/Delete/{id}` - Delete a receipt.

 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature/your-feature-name`).
6. Create a Pull Request.

 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

 Acknowledgements

- Thanks to all contributors and users for their support.
- Special thanks to the developers and maintainers of the libraries and tools used in this project.

Screenshots
![Screenshot (309)](https://github.com/MerlinDivya/canteen/assets/91864335/67d3d9d4-1da2-42d1-8843-809c59481834)

![Screenshot (304)](https://github.com/MerlinDivya/canteen/assets/91864335/777a7300-fa86-40f3-add7-a5fdda7c6826)

![Screenshot (305)](https://github.com/MerlinDivya/canteen/assets/91864335/a2aca3a2-273f-4bab-ab83-7ec77ef1e27d)

![Screenshot (308)](https://github.com/MerlinDivya/canteen/assets/91864335/64501aba-4e97-4628-85f4-e15fad60c759)


![Screenshot (306)](https://github.com/MerlinDivya/canteen/assets/91864335/704520d7-f806-4268-afd1-1fbcbf9bd6af)

![Screenshot (307)](https://github.com/MerlinDivya/canteen/assets/91864335/f9e52776-7ab1-476c-8fd4-09af69b72b92)


