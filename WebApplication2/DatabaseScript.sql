-- Create the RestaurantDB database
CREATE DATABASE RestaurantDB;
GO

USE RestaurantDB;
GO

-- Create MenuItems table
CREATE TABLE MenuItems (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

-- Create Orders table
CREATE TABLE Orders (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL
);

-- Create Tables table
CREATE TABLE Tables (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TableNumber NVARCHAR(10) NOT NULL,
    Capacity INT NOT NULL
);

-- Create Staff table
CREATE TABLE Staff (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

-- Create Users table for authentication
CREATE TABLE Users (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

-- Insert a default user for testing
INSERT INTO Users (Username, Password) VALUES ('admin', 'admin123'); 