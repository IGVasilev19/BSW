USE dbi549842_warehousem

CREATE TABLE [Address](
AddressId INT PRIMARY KEY IDENTITY(1,1),
Country VARCHAR(50) NOT NULL,
City VARCHAR(50) NOT NULL,
StreetName VARCHAR(50) NOT NULL,
StreetNumber INT NOT NULL,
Zip INT NOT NULL
);

CREATE TABLE Warehouse(
WarehouseId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES [Address](AddressId)
);

SELECT * FROM Warehouse
DROP TABLE Warehouse
INSERT INTO Warehouse ([Name]) VALUES
('IvoWarehouse')


CREATE TABLE Employee(
EmployeeId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL,
Email VARCHAR(100) NOT NULL,
[Password] VARCHAR(255) NOT NULL,
PhoneNumber VARCHAR(20) NOT NULL,
IsActive BIT DEFAULT 0,
WarehouseId INT FOREIGN KEY REFERENCES Warehouse(WarehouseId),
[Role] INT NOT NULL
);

ALTER TABLE Employee
ALTER COLUMN WarehouseId INT NULL;

EXEC sp_rename 'Employee.Role', '[Role]', 'COLUMN';

CREATE TABLE [Order](
OrderId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL,
OrderDate DATETIME NOT NULL,
OrderStatus INT NOT NULL,
CreateById INT FOREIGN KEY REFERENCES Employee(EmployeeId),
AssignedToId INT FOREIGN KEY REFERENCES Employee(EmployeeId)
);

CREATE TABLE [Zone](
ZoneId INT PRIMARY KEY IDENTITY (1,1),
[Name] VARCHAR(50) NOT NULL,
Capacity DECIMAL NOT NULL,
WarehouseId INT FOREIGN KEY REFERENCES Warehouse(WarehouseId)
);

CREATE TABLE Inventory(
InventoryId INT PRIMARY KEY IDENTITY(1,1),
Quantity INT NOT NULL,
LastUpdate DATETIME NOT NULL,
ProductId INT FOREIGN KEY REFERENCES [Product](ProductId),
ZoneId INT FOREIGN KEY REFERENCES [Zone](ZoneId)
);

CREATE TABLE Category(
CategoryId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL
);

CREATE TABLE [Product](
ProductId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL,
Price DECIMAL NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId)
);

CREATE TABLE UnitType(
UnitTypeId INT PRIMARY KEY IDENTITY(1,1),
[Name] VARCHAR(50) NOT NULL
);

CREATE TABLE OrderProduct(
OrderProductId INT PRIMARY KEY IDENTITY(1,1),
Quantity INT NOT NULL,
TotalPrice DECIMAL NOT NULL,
OrderId INT FOREIGN KEY REFERENCES [Order](OrderId),
ProductId INT FOREIGN KEY REFERENCES [Product](ProductId),
UnitType INT NOT NULL
);

ALTER TABLE [OrderProduct] DROP CONSTRAINT FK__OrderProd__UnitT__4AB81AF0;
ALTER TABLE [Order] DROP CONSTRAINT FK__Order__OrderStat__33D4B598;
ALTER TABLE Employee DROP CONSTRAINT FK__Employee__Employ__2D27B809;
ALTER TABLE Employee DROP CONSTRAINT FK__Employee__RoleId__2F10007B;

DROP TABLE UnitType;
DROP TABLE OrderStatus;
DROP TABLE EmployeeStatus;
DROP TABLE [Role];

ALTER TABLE OrderProduct DROP COLUMN UnitTypeId;
ALTER TABLE [Order] DROP COLUMN OrderStatusId;
ALTER TABLE Employee DROP COLUMN EmployeeStatusId;
ALTER TABLE Employee DROP COLUMN RoleId;

ALTER TABLE OrderProduct ADD UnitType INT NOT NULL DEFAULT 0;
ALTER TABLE [Order] ADD OrderStatus INT NOT NULL DEFAULT 0;
ALTER TABLE Employee ADD EmployeeStatus INT NOT NULL DEFAULT 0;
ALTER TABLE Employee ADD Role INT NOT NULL DEFAULT 0;

SELECT * FROM Employee
SELECT * FROM Warehouse
SELECT * FROM [Address]

EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";
EXEC sp_MSforeachtable "DROP TABLE ?";

-- Disable all foreign key constraints temporarily
EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL"

-- Delete data from all tables
EXEC sp_msforeachtable "DELETE FROM ?"

-- Reset identity (auto-increment) values
EXEC sp_msforeachtable "IF OBJECTPROPERTY(OBJECT_ID('?'), 'TableHasIdentity') = 1 DBCC CHECKIDENT ('?', RESEED, 0)"

-- Re-enable all foreign key constraints
EXEC sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL"