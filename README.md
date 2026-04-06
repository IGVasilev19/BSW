# 📦 Warehouse Management System (BSW) 

![C#](https://img.shields.io/badge/language-C%23-blue.svg) ![Next.js](https://img.shields.io/badge/framework-Next.js-black.svg) ![TailwindCSS](https://img.shields.io/badge/styling-TailwindCSS-blue.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

## 📝 Description
The Warehouse Management System (BSW) is a comprehensive C#-based enterprise solution designed to streamline and automate warehouse operations. It covers the end-to-end lifecycle of warehouse inventory, including product management, stock movement, category organization, order processing, and employee access control. The project features a robust service-oriented architecture, utilizing a repository pattern for data access and a clean separation of concerns.

## 📑 Table of Contents
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Installation](#-installation)
- [Usage](#-usage)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features
- 🏗️ **Inventory Management**: Full CRUD operations for warehouses, zones, products, and categories.
- 📦 **Order Processing**: Advanced order management system with product assignment logic.
- 🔐 **Authentication & Authorization**: Secure employee role-based access management with password hashing.
- 🧠 **Strategy Pattern**: Flexible pricing strategy implementation using a Factory pattern to handle different product pricing models.
- 🚀 **Dynamic UI**: Responsive interface utilizing TailwindCSS with custom JavaScript animations and interactive components.
- 🧪 **Test-Driven**: Includes a dedicated test suite for verifying core business logic.

## 🛠️ Tech Stack
- **Backend**: C# .NET 8.0 / ASP.NET Core
- **Frontend**: Razor Pages, JavaScript, TailwindCSS
- **Database**: SQL Server (implied via SQL queries)
- **Development Tools**: Csharpier (code formatting), NuGet

## ⚙️ Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/IGVasilev19/BSW
   ```
2. **Restore Dependencies**:
   Navigate to the `WarehouseManagementSystem` directory and run:
   ```bash
   dotnet restore
   ```
3. **Install Frontend Dependencies**:
   Ensure you have Node.js installed, then run:
   ```bash
   npm install
   ```
4. **Database Setup**:
   Configure your connection string in `appsettings.json` and execute the provided scripts in `Database/Warehouse queries.sql`.

## 💻 Usage
This system allows warehouse managers to:
- **Manage Stock**: Add stock to specific zones using the `InventoryController`.
- **Employee Onboarding**: Register new employees with specific roles using `SystemController`.
- **Pricing**: Apply different pricing strategies (e.g., `OnSalePricingStrategy`) dynamically via the `PricingStrategyFactory`.

## 📂 Project Structure
- `BLL/`: Business Logic Layer containing domain entities (Warehouse, Product, Order).
- `DAL/`: Data Access Layer containing repository interfaces and implementations.
- `Service/`: Orchestration layer providing business services and strategy patterns.
- `WarehouseManagementSystem/`: Main ASP.NET MVC project (Controllers, Views, Models).
- `Tests/`: Unit tests for verifying business services.

## 📬 Footer
**Project Name**: BSW
**Repository**: [https://github.com/IGVasilev19/BSW](https://github.com/IGVasilev19/BSW)
*Built with passion for efficient logistics. Feel free to star the repo if you found it useful!*
