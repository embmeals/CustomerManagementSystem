# Customer Management System

A Windows Forms application built with .NET 8.0 for managing customer information with a clean, user-friendly interface.

> **Note:** This project was created as part of a school assignment to demonstrate object-oriented programming concepts, generic collections, delegates, events, and Windows Forms development.

## Features

- ✅ **Add Customers** - Create new customer records with first name, last name, and email
- ✅ **Update Customers** - Modify existing customer information
- ✅ **Delete Customers** - Remove customers from the system with confirmation
- ✅ **View Customers** - Display all customers in a sortable list view
- ✅ **Email Validation** - Ensures valid email addresses are entered
- ✅ **Duplicate Prevention** - Prevents adding customers with duplicate email addresses
- ✅ **Event-Driven Architecture** - Uses delegates and events for customer operations
- ✅ **Generic Collections** - Custom generic collection implementation for type safety

## Technologies Used

- **.NET 8.0** - Target framework
- **Windows Forms** - UI framework
- **C# 12** - Programming language
- **Generic Collections** - Custom `CustomerCollection<T>` implementation
- **Delegates & Events** - Event-driven architecture
- **Interfaces** - `IManageable` interface for extensibility

## Project Structure

```
CustomerManagementSystem/
├── Models/
│   ├── Customer.cs           # Customer entity with validation
│   └── IManageable.cs        # Interface for manageable entities
├── Data/
│   ├── CustomerCollection.cs # Generic collection implementation
│   └── CustomerManager.cs    # Business logic and event handling
├── UI/
│   ├── Form1.cs              # Main form logic
│   └── Form1.Designer.cs     # Form designer code
└── Program.cs                # Application entry point
```

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Windows OS (Windows 7 or later)
- Visual Studio 2022 or JetBrains Rider (optional, for development)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/embmeals/CustomerManagementSystem.git
   cd CustomerManagementSystem
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

## Usage

### Adding a Customer

1. Enter the customer's first name, last name, and email address
2. Click the **Add Customer** button
3. The customer will appear in the list below

### Updating a Customer

1. Select a customer from the list
2. Modify the information in the input fields
3. Click the **Update Customer** button

### Deleting a Customer

1. Select a customer from the list
2. Click the **Delete Customer** button
3. Confirm the deletion in the dialog box

## Code Examples

### Creating a Customer

```csharp
var customer = new Customer("John", "Doe", "john.doe@example.com");
```

### Using the Customer Manager

```csharp
var collection = new CustomerCollection<Customer>();
var manager = new CustomerManager(collection);

// Subscribe to events
manager.CustomerAdded += (customer) =>
    Console.WriteLine($"Added: {customer.FullName}");

// Add a customer
manager.AddCustomer(customer);
```

## Architecture Highlights

### Generic Collection
The `CustomerCollection<T>` class provides a type-safe, enumerable collection with indexer support.

### Event-Driven Design
The `CustomerManager` class uses delegates and events to notify subscribers of customer operations:
- `CustomerAdded`
- `CustomerUpdated`
- `CustomerDeleted`

### Interface-Based Design
The `IManageable` interface ensures consistent behavior across manageable entities.

## CI/CD

This project uses GitHub Actions for continuous integration:
- Automatically builds on push to `main` branch
- Runs on Windows runners (required for Windows Forms)
- Validates code compilation

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is open source and available under the [MIT License](LICENSE).

## Author

**embmeals**
- GitHub: [@embmeals](https://github.com/embmeals)

## Learning Objectives

This project demonstrates the following programming concepts:
- **Object-Oriented Programming** - Classes, inheritance, interfaces, encapsulation
- **Generic Collections** - Custom generic collection implementation
- **Delegates & Events** - Event-driven architecture and custom event handlers
- **Windows Forms** - GUI development with .NET
- **Data Validation** - Input validation and error handling
- **SOLID Principles** - Single responsibility, interface segregation

## Acknowledgments

- Built with .NET 8.0 and Windows Forms
- Created as an educational project to demonstrate C# and .NET fundamentals
- Showcases generic collections and event-driven architecture
