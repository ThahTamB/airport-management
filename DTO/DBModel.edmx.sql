
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/25/2023 10:27:26
-- Generated from EDMX file: D:\Code\Airport_Management\Airport_Manager\DTO\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AirportManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Account__Employe__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK__Account__Employe__398D8EEE];
GO
IF OBJECT_ID(N'[dbo].[FK__Bill_Deta__Custo__4316F928]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bill_Detail] DROP CONSTRAINT [FK__Bill_Deta__Custo__4316F928];
GO
IF OBJECT_ID(N'[dbo].[FK__Bill_Deta__Fligh__440B1D61]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bill_Detail] DROP CONSTRAINT [FK__Bill_Deta__Fligh__440B1D61];
GO
IF OBJECT_ID(N'[dbo].[FK__Flight__Airline__403A8C7D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK__Flight__Airline__403A8C7D];
GO
IF OBJECT_ID(N'[dbo].[FK__Job__EmployeeID__46E78A0C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK__Job__EmployeeID__46E78A0C];
GO
IF OBJECT_ID(N'[dbo].[FK__Job__FlightID__47DBAE45]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK__Job__FlightID__47DBAE45];
GO
IF OBJECT_ID(N'[dbo].[FK_Bill_Detail_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bill_Detail] DROP CONSTRAINT [FK_Bill_Detail_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Flights_Locations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK_Flights_Locations];
GO
IF OBJECT_ID(N'[dbo].[FK_Flights_Locations1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK_Flights_Locations1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Bill_Detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bill_Detail];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Flights]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Flights];
GO
IF OBJECT_ID(N'[dbo].[Jobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jobs];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Planes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Planes];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [EmployeeID] int  NOT NULL,
    [Username] nvarchar(30)  NULL,
    [Password] nvarchar(30)  NULL,
    [AccessLevel] bit  NULL
);
GO

-- Creating table 'Bill_Detail'
CREATE TABLE [dbo].[Bill_Detail] (
    [BillID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [FlightID] int  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [SeatNumber] nvarchar(5)  NULL,
    [SeatClass] bit  NULL,
    [BookingState] int  NULL,
    [TotalPrice] decimal(19,4)  NULL,
    [BookingDate] datetime  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NULL,
    [Address] nvarchar(50)  NULL,
    [Nationality] nvarchar(20)  NULL,
    [Sex] bit  NULL,
    [DateOfBirth] datetime  NULL,
    [NationalID] nvarchar(20)  NULL,
    [Email] nvarchar(30)  NULL,
    [TeleNumber] nvarchar(18)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NULL,
    [Address] nvarchar(50)  NULL,
    [Nationality] nvarchar(20)  NULL,
    [Sex] bit  NULL,
    [DateOfBirth] datetime  NULL,
    [Email] nvarchar(30)  NULL,
    [NationID] nvarchar(30)  NULL,
    [TeleNumber] nvarchar(18)  NULL,
    [Position] nvarchar(20)  NULL
);
GO

-- Creating table 'Flights'
CREATE TABLE [dbo].[Flights] (
    [FlightID] int IDENTITY(1,1) NOT NULL,
    [PlaneID] int  NOT NULL,
    [Departure] int  NOT NULL,
    [DateOfDeparture] datetime  NOT NULL,
    [Destination] int  NOT NULL,
    [Airline] nvarchar(20)  NULL,
    [Price] decimal(12,0)  NOT NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [JobID] int IDENTITY(1,1) NOT NULL,
    [AssignedDate] datetime  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [FlightID] int  NOT NULL,
    [JobDescription] nvarchar(20)  NULL,
    [JobState] nvarchar(20)  NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [LocationID] int IDENTITY(1,1) NOT NULL,
    [LocationName] nvarchar(20)  NULL
);
GO

-- Creating table 'Planes'
CREATE TABLE [dbo].[Planes] (
    [PlaneID] int IDENTITY(1,1) NOT NULL,
    [Model] nvarchar(20)  NULL,
    [Registration] nvarchar(20)  NULL,
    [TotalSeat] int  NULL,
    [Manufacturer] nvarchar(30)  NULL,
    [State] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EmployeeID] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [BillID] in table 'Bill_Detail'
ALTER TABLE [dbo].[Bill_Detail]
ADD CONSTRAINT [PK_Bill_Detail]
    PRIMARY KEY CLUSTERED ([BillID] ASC);
GO

-- Creating primary key on [CustomerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [FlightID] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [PK_Flights]
    PRIMARY KEY CLUSTERED ([FlightID] ASC);
GO

-- Creating primary key on [JobID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([JobID] ASC);
GO

-- Creating primary key on [LocationID] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([LocationID] ASC);
GO

-- Creating primary key on [PlaneID] in table 'Planes'
ALTER TABLE [dbo].[Planes]
ADD CONSTRAINT [PK_Planes]
    PRIMARY KEY CLUSTERED ([PlaneID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EmployeeID] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK__Account__Employe__398D8EEE]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CustomerID] in table 'Bill_Detail'
ALTER TABLE [dbo].[Bill_Detail]
ADD CONSTRAINT [FK__Bill_Deta__Custo__4316F928]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Customers]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Bill_Deta__Custo__4316F928'
CREATE INDEX [IX_FK__Bill_Deta__Custo__4316F928]
ON [dbo].[Bill_Detail]
    ([CustomerID]);
GO

-- Creating foreign key on [FlightID] in table 'Bill_Detail'
ALTER TABLE [dbo].[Bill_Detail]
ADD CONSTRAINT [FK__Bill_Deta__Fligh__440B1D61]
    FOREIGN KEY ([FlightID])
    REFERENCES [dbo].[Flights]
        ([FlightID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Bill_Deta__Fligh__440B1D61'
CREATE INDEX [IX_FK__Bill_Deta__Fligh__440B1D61]
ON [dbo].[Bill_Detail]
    ([FlightID]);
GO

-- Creating foreign key on [EmployeeID] in table 'Bill_Detail'
ALTER TABLE [dbo].[Bill_Detail]
ADD CONSTRAINT [FK_Bill_Detail_Employees]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bill_Detail_Employees'
CREATE INDEX [IX_FK_Bill_Detail_Employees]
ON [dbo].[Bill_Detail]
    ([EmployeeID]);
GO

-- Creating foreign key on [EmployeeID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK__Job__EmployeeID__46E78A0C]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Job__EmployeeID__46E78A0C'
CREATE INDEX [IX_FK__Job__EmployeeID__46E78A0C]
ON [dbo].[Jobs]
    ([EmployeeID]);
GO

-- Creating foreign key on [PlaneID] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK__Flight__Airline__403A8C7D]
    FOREIGN KEY ([PlaneID])
    REFERENCES [dbo].[Planes]
        ([PlaneID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Flight__Airline__403A8C7D'
CREATE INDEX [IX_FK__Flight__Airline__403A8C7D]
ON [dbo].[Flights]
    ([PlaneID]);
GO

-- Creating foreign key on [FlightID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK__Job__FlightID__47DBAE45]
    FOREIGN KEY ([FlightID])
    REFERENCES [dbo].[Flights]
        ([FlightID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Job__FlightID__47DBAE45'
CREATE INDEX [IX_FK__Job__FlightID__47DBAE45]
ON [dbo].[Jobs]
    ([FlightID]);
GO

-- Creating foreign key on [Departure] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK_Flights_Locations]
    FOREIGN KEY ([Departure])
    REFERENCES [dbo].[Locations]
        ([LocationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Flights_Locations'
CREATE INDEX [IX_FK_Flights_Locations]
ON [dbo].[Flights]
    ([Departure]);
GO

-- Creating foreign key on [Destination] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK_Flights_Locations1]
    FOREIGN KEY ([Destination])
    REFERENCES [dbo].[Locations]
        ([LocationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Flights_Locations1'
CREATE INDEX [IX_FK_Flights_Locations1]
ON [dbo].[Flights]
    ([Destination]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------