-- Create Database AAAsesores;
Use AAAsesores;
GO

CREATE TABLE Province (
	Id INTEGER IDENTITY(1, 1), 
	Name VARCHAR(50) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_Province PRIMARY KEY (Id),
	CONSTRAINT UK_Province_ProvinceName UNIQUE (Name),
);

CREATE TABLE Canton (
	Id INTEGER IDENTITY(1, 1), 
	Name VARCHAR(50) NOT NULL,
	Province INTEGER NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_Canton PRIMARY KEY (Id),
	CONSTRAINT FK_Canton_Province FOREIGN KEY (Province) REFERENCES Province (Id),
  CONSTRAINT UK_Canton_CantonName UNIQUE (Name)
);

CREATE TABLE District (
	Id INTEGER IDENTITY(1, 1), 
	Name VARCHAR(50) NOT NULL,
	Canton INTEGER NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_District PRIMARY KEY (Id),
	CONSTRAINT FK_District_Canton FOREIGN KEY (Canton) REFERENCES Canton (Id),
    CONSTRAINT UK_District_DistrictName UNIQUE (Name)
);

--1) Table structure for table 'Document Type'
CREATE TABLE DocumentType (
  Id INTEGER IDENTITY(1, 1), 
  Document VARCHAR (50) NOT NULL,
  IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
  CONSTRAINT PK_DocumentType PRIMARY KEY (Id),
  CONSTRAINT UK_DocumentType_DocumentName UNIQUE (Document)
);
--2) Table structure for table 'Status'
CREATE TABLE Status (
  Id INTEGER IDENTITY(1, 1), 
  Name VARCHAR (50) NOT NULL, 
  Description VARCHAR (50) NOT NULL, 
  RelatedTable VARCHAR (50) NOT NULL,
  IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
  CONSTRAINT PK_Status PRIMARY KEY (Id),
  CONSTRAINT UK_Status_StatusName UNIQUE (Name)
);
--3) Table structure for table 'Roles'
CREATE TABLE Role (
  Id INTEGER IDENTITY(1, 1), 
  RoleName VARCHAR (50) NOT NULL, 
  Description VARCHAR (50) NOT NULL, 
  IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
  CONSTRAINT PK_Role PRIMARY KEY (Id),
  CONSTRAINT UK_Role_RoleName UNIQUE (RoleName)
);

--54 Table structure for table 'USERS'
CREATE TABLE Users (
  Id INTEGER IDENTITY(1, 1), 
  DocumentType INTEGER NOT NULL, 
  Identification VARCHAR (20) NOT NULL, 
  Name VARCHAR(50) NOT NULL, 
  FirstLastName VARCHAR(50) NOT NULL, 
  SecondLastName VARCHAR (50) NOT NULL, 
  PhoneNumber Varchar (20) NOT NULL, 
  Email VARCHAR(128) NOT NULL,
  CreationDate DATETIME NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY (Id),
  CONSTRAINT FK_Users_DocumentType FOREIGN KEY (DocumentType) REFERENCES DocumentType(Id),
  CONSTRAINT UK_Users_Identification UNIQUE (Identification),
  CONSTRAINT UK_Users_Email UNIQUE (Email)
  );
--5) Table structure for table 'Employees'
CREATE TABLE Employee (
  Id INTEGER IDENTITY(1, 1),
  UserId INTEGER NOT NULL, 
  Password VARCHAR(50) NOT NULL,
  PasswordTemp VARCHAR(50) NOT NULL,
  Role INTEGER NOT NULL, 
  Status INTEGER NOT NULL, 
  ImageProfile VARCHAR(100) NULL,
  RetirementDate DATETIME NULL, 
  IsTemporary BIT NOT NULL,
  FailedAttempts INTEGER NOT NULL DEFAULT 0,
  CONSTRAINT PK_Employee PRIMARY KEY (Id),
  CONSTRAINT FK_Employee_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
  CONSTRAINT FK_Employee_Role FOREIGN KEY (Role) REFERENCES Role(Id),
  CONSTRAINT FK_Employee_Status FOREIGN KEY (Status) REFERENCES Status(Id),
  CONSTRAINT UK_Employee_UserId UNIQUE (UserId)
  );

----------------------------------------------------------------------------------

--7) Tipos de propiedades
CREATE TABLE PropertyType(
	Id INTEGER IDENTITY(1, 1), 
	Type VARCHAR(50) NOT NULL,
	Description VARCHAR(128) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_PropertyType PRIMARY KEY (Id)
);

--8) Tipos de transacción
CREATE TABLE TransactionType(
	Id INTEGER IDENTITY(1, 1), 
	Type VARCHAR(50) NOT NULL,
	Description VARCHAR(128) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_TransactionType PRIMARY KEY (Id)
);

CREATE TABLE Currency (
	Id INTEGER IDENTITY(1, 1),
	Name VARCHAR(64) NOT NULL,
	CurrencyCode VARCHAR(64) NOT NULL,
	Symbol NCHAR NOT NULL,
	CONSTRAINT PK_Currency PRIMARY KEY (Id)
);

--8) Table structure for table 'Properties'
CREATE TABLE Property (
  Id INTEGER IDENTITY(1, 1),
  Title VARCHAR(250) NOT NULL,
  EmployeeId INTEGER NOT NULL,
  Currency INTEGER NOT NULL,
  Price DECIMAL (18, 2) NOT NULL, 
  AreaLand DECIMAL (10, 2) NOT NULL, 
  AreaBuild DECIMAL (10, 2) NOT NULL,  
  Description VARCHAR(MAX) NOT NULL,
  CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
  SoldDate DATETIME NULL,
  PropertyType INTEGER NOT NULL, 
  Transactiontype INTEGER NOT NULL,
  PropertyStatus INTEGER NOT NULL,
  CONSTRAINT FK_Property PRIMARY KEY (Id),
  CONSTRAINT FK_Property_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
  CONSTRAINT FK_Property_Currency FOREIGN KEY (Currency) REFERENCES Currency(Id),
  CONSTRAINT FK_Property_PropertyType FOREIGN KEY (PropertyType) REFERENCES PropertyType(Id),
  CONSTRAINT FK_Property_Transactiontype FOREIGN KEY (Transactiontype) REFERENCES TransactionType(Id),
  CONSTRAINT FK_Property_Status FOREIGN KEY (PropertyStatus) REFERENCES Status(Id)
);

CREATE TABLE PropertyAddress (
	Id INTEGER IDENTITY(1, 1),
	PropertyId INTEGER NOT NULL,
	DistrictId INTEGER NOT NULL,
	OtherSigns VARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_PropertyAddress PRIMARY KEY (Id),
	CONSTRAINT FK_PropertyAddress_Property FOREIGN KEY (PropertyId) REFERENCES Property (Id),
	CONSTRAINT FK_PropertyAddress_District FOREIGN KEY (DistrictId) REFERENCES District (Id)
);

--9) Table structure for table 'Properties Images'
CREATE TABLE PropertyImg (
  Id INTEGER IDENTITY(1, 1), 
  ImageUrl VARCHAR(256) NOT NULL,
  PropertyId INTEGER NOT NULL,
  CONSTRAINT PK_PropertyImg PRIMARY KEY (Id),
  CONSTRAINT FK_PropertyImg_Property FOREIGN KEY (PropertyId) REFERENCES Property(Id)
);

--10) Table structure for table 'Properties Documents'
CREATE TABLE PropertyDoc(
  Id INTEGER IDENTITY(1, 1), 
  Name VARCHAR(128) NOT NULL,
  DocUrl VARCHAR(256) NOT NULL,
  PropertyId INTEGER NOT NULL,
  CONSTRAINT Pk_PropertyDoc PRIMARY KEY (Id),
  CONSTRAINT FK_PropertyDoc_Property FOREIGN KEY (PropertyId) REFERENCES Property(Id)
);
----------------------------------------------------------------------------------
--12) Table structure for table 'Contact'
CREATE TABLE Contact (
  Id INTEGER IDENTITY(1, 1),
  UserId INTEGER NOT NULL,
  Message VARCHAR(512) NOT NULL,
  Status INTEGER NOT NULL, 
  CONSTRAINT PK_Contact PRIMARY KEY (Id),
  CONSTRAINT FK_Contact_User FOREIGN KEY (UserId) REFERENCES Users(Id),
  CONSTRAINT FK_Contact_Status FOREIGN KEY (Status) REFERENCES Status(Id)
  );
----------------------------------------------------------------------------------
--13) Table structure for table 'News'
CREATE TABLE News (
  Id INTEGER IDENTITY(1, 1), 
  EmployeeId INTEGER NOT NULL,
  CreationDate DATETIME NOT NULL,
  Title VARCHAR(256) NOT NULL, 
  Description VARCHAR(MAX) NOT NULL,
  Url VARCHAR(MAX) NULL,
  ImageUrl VARCHAR(MAX) NULL,
  CONSTRAINT PK_News PRIMARY KEY (Id),
  CONSTRAINT FK_News_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);
/*
CREATE TABLE NewsImages (
    Id INTEGER IDENTITY(1,1) PRIMARY KEY,
    NewsId INTEGER,
    ImageUrl VARCHAR(256),
    FOREIGN KEY (NewsId) REFERENCES News(Id)
)*/

GO
----------------------------------------------------------------------------------
--14) Table structure for table 'VirtualAssistant'
CREATE TABLE VirtualAssistant (
  Id INTEGER IDENTITY(1, 1), 
  Question VARCHAR(50) NOT NULL, 
  Answer VARCHAR(50) NOT NULL, 
  CONSTRAINT PK_VirtualAssistant PRIMARY KEY (Id)
);
----------------------------------------------------------------------------------

--16) Table structure for table 'Appointment' 
CREATE TABLE Appointment (
  Id INTEGER IDENTITY(1, 1), 
  EmployeeId INTEGER NULL, --Lo llena el admin
  PropertyId INTEGER NOT NULL, 
  CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
  AppointmentDate DATETIME NOT NULL, 
  Status INTEGER NOT NULL, 
  UserId INTEGER NOT NULL, 
  CONSTRAINT PK_Appointment PRIMARY KEY (Id), 
  CONSTRAINT FK_Appointment_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id), 
  CONSTRAINT FK_Appointment_Property FOREIGN KEY (PropertyId) REFERENCES Property(Id), 
  CONSTRAINT FK_Appointment_User FOREIGN KEY (UserId) REFERENCES Users(Id), 
  CONSTRAINT FK_Appointment_Status FOREIGN KEY (Status) REFERENCES Status(Id)
);

--19) Table structure for table 'QuoteDetails'
/*CREATE TABLE QuoteDetails (
  Id INTEGER IDENTITY(1, 1), 
  WalkInClosetQuantity INTEGER NOT NULL, 
  SecondaryRooms INTEGER NOT NULL, 
  SecondaryBathrooms INTEGER NOT NULL, 
  Details VARCHAR(256) NOT NULL, 
  CONSTRAINT PK_QuoteDetails PRIMARY KEY (Id), 
);*/

--18) Table structure for table 'QuoteCharacteristics'
--cambiar por intermedia
CREATE TABLE QuoteRooms (
	Id INTEGER IDENTITY(1, 1),
	RoomName VARCHAR(50) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1, -- Para poder deshabilitar en mantenimiento
	CONSTRAINT PK_QuoteRooms PRIMARY KEY (Id),
    CONSTRAINT UK_QuoteRooms_RoomName UNIQUE (RoomName)
);

--20) Table structure for table 'Quotes'
CREATE TABLE Quote (
  Id INTEGER IDENTITY(1, 1), 
  ClientId INTEGER NOT NULL, 
  /*QuoteDetails INTEGER NOT NULL,*/ 
  CreationDate DATETIME NOT NULL, 
  Status INTEGER NOT NULL, 
  DocUrl VARCHAR(256) NULL,
  Details VARCHAR(512) NOT NULL,
  CONSTRAINT PK_Quote PRIMARY KEY (Id),
  CONSTRAINT FK_Quote_Client FOREIGN KEY (ClientId) REFERENCES Users(Id),
  /*CONSTRAINT FK_Quote_QuoteDetails FOREIGN KEY (QuoteDetails) REFERENCES QuoteDetails(Id),*/
  CONSTRAINT FK_Quote_Status FOREIGN KEY (Status) REFERENCES Status(Id)
);

CREATE TABLE QuoteDetailsPerRoom (
	Id INTEGER IDENTITY(1, 1),
	QuoteId INTEGER NOT NULL,
	QuoteRoomId INTEGER NOT NULL,
	Quantity INTEGER NOT NULL,
	CONSTRAINT PK_QuoteDetailsPerRoom PRIMARY KEY (Id),
	/*CONSTRAINT FK_QuoteDetailsPerRoom_QuoteDetails FOREIGN KEY (QuoteDetailsId) REFERENCES QuoteDetails (Id),*/
	CONSTRAINT FK_QuoteDetailsPerRoom_Quote FOREIGN KEY (QuoteId) REFERENCES Quote (Id),
	CONSTRAINT FK_QuoteDetailsPerRoom_QuoteRooms FOREIGN KEY (QuoteRoomId) REFERENCES QuoteRooms (Id)
);
----------------------------------------------------------------------------------
--24) Table structure for table 'ErrorLog'
CREATE TABLE ErrorLog (
  ErrorId INTEGER identity(1, 1), 
  ErrorDate DATETIME NOT null, 
  ErrorMessage varchar(4000) NOT null, 
  ErrorUser INTEGER NOT null, 
  CONSTRAINT PK_ErrorLog PRIMARY KEY (ErrorId),
  CONSTRAINT FK_ErrorLog_Employee FOREIGN KEY (ErrorUser) REFERENCES Employee(Id)
);

CREATE TABLE AuditLog (
    Id INTEGER IDENTITY(1, 1),
    EmployeeId INTEGER NOT NULL,
    TableName NVARCHAR(MAX) NOT NULL,
    CurrentAudit NVARCHAR(MAX) NOT NULL,
    CreationDateAudit DATETIME NOT NULL,
    CONSTRAINT PK_Audit PRIMARY KEY (Id),
    CONSTRAINT FK_Audit_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);

----------------------------------------------------------------------------------
--Drops tables en orden inverso
/*
DROP TABLE ErrorLog;
DROP TABLE PropertyAudit;
DROP TABLE EmployeeAudit;
DROP TABLE Quote;
DROP TABLE QuoteDetails;
DROP TABLE QuoteCharacteristics;
DROP TABLE Appointment;
DROP TABLE VirtualAssistant;
DROP TABLE News;
DROP TABLE Contact;	 
DROP TABLE PropertyImg;
DROP TABLE PropertyDoc;
DROP TABLE Property;
DROP TABLE PropertyType;
DROP TABLE TransactionType;	
DROP TABLE Employee;
DROP TABLE Users;
DROP TABLE Role;
DROP TABLE Status;
DROP TABLE DocumentType;
*/