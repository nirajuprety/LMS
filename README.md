# Library Management System

The Library Management System is a web application designed to efficiently manage the operations of a library. It provides a user-friendly interface for both staff members and students, allowing them to access library resources, issue and return books, and perform various administrative tasks.

## Table of Contents
1. [Introduction](#introduction)
2. [Features](#features)
3. [Getting Started](#getting-started)
4. [API Endpoint Implementation](#api-endpoint-implementation)
5. [Contributing](#contributing)
6. [License](#license)

## Introduction

The Library Management System aims to streamline the library's day-to-day activities by automating processes and providing real-time access to information. Staff members can manage books, students, and issued books, while students can check the availability of books and borrow them.

## Features

- Staff Login: Secure authentication for staff members to access the system.
- Add Book: Staff members can add new books to the library collection.
- Add Student: Staff members can add new students to the library system.
- Issue Book: Staff members can issue books to students.
- Get Issued Books: View a list of books issued to a specific student.
- Get Book Details: Retrieve details of a specific book using its ID.
- Get Student Details: Retrieve details of a specific student using their ID.

## Getting Started

To run the Library Management System locally on your machine, follow these steps:

1. Clone the repository from GitHub: `git clone https://github.com/nirajuprety/LMS.git`
2. Navigate to the project directory: `cd LMS`
3. Install the required dependencies: `build the project`
4. Set up the database with appropriate tables and seed initial data(appsettings.json).
5. Start the application: `dotnet run  --` 

## API Endpoint Implementation

The Library Management System's backend is implemented with ASP.NET Web API. The API endpoints are as follows:

- Staff Login, Get Book Details
- Add Book
- Add Student, Get Student Details
- Issue Book, Get Issued Books

Please refer to the [[API documentation](https://docs.google.com/document/d/1_wcqE6CqI9B-AYf4cGHMMNWfPVUbqMGoT3UeIpKf7P4/edit)] for detailed information about the API endpoints and their usage.

## Contributing

We welcome contributions to the Library Management System! If you find a bug or have a feature suggestion, please open an issue on GitHub. For code contributions, please follow the guidelines in [CONTRIBUTING.md](./CONTRIBUTING.md).

## License

The Library Management System is open-source software licensed under the [MIT License](./LICENSE). Feel free to use, modify, and distribute it as per the terms of the license.
