USE AAAsesores
GO

-- Procedimientos de la tabla Status

-- CREATE
CREATE or ALTER PROCEDURE InsertStatusSP
	@Name VARCHAR(50),
	@Description VARCHAR(50),
	@RelatedTable VARCHAR(50)
AS
BEGIN 
	INSERT INTO Status (Name, Description, RelatedTable, IsActive) VALUES (@Name, @Description, @RelatedTable, 1)
END
GO

-- Este SP es una prueba para retornar el Id que se genera; sin embargo, hay que probar como manejarlo desde código
CREATE or ALTER PROCEDURE InsertStatusSPTest
	@Name VARCHAR(50),
	@Description VARCHAR(50),
	@RelatedTable VARCHAR(50),
	@IDGenerado INT OUTPUT
AS
BEGIN 
	INSERT INTO Status (Name, Description, RelatedTable, IsActive) VALUES (@Name, @Description, @RelatedTable, 1)

	SET @IDGenerado = SCOPE_IDENTITY();
END
GO

-- READ

CREATE or ALTER PROCEDURE GetAllStatusSP
AS
BEGIN
	SELECT Id, Name, Description, RelatedTable, IsActive FROM Status WHERE IsActive = 1
END 
GO

--Read All: Muestras todo los estado activos e inactivos

CREATE or ALTER PROCEDURE GetStatusSP
AS
BEGIN
	SELECT Id, Name, Description, RelatedTable, IsActive FROM Status 
END 
GO

-- UPDATE 
CREATE or ALTER PROCEDURE UpdateStatusSP
	@Id INTEGER,
	@Name VARCHAR(50),
	@Description VARCHAR(50),
	@RelatedTable VARCHAR(50),
	@IsActive BIT
AS
BEGIN
	UPDATE STATUS
	SET 
		Name = @Name, 
		Description = @Description, 
		RelatedTable = @RelatedTable,
		IsActive = @IsActive
	WHERE Id = @Id
END
GO

-- DELETE
CREATE or ALTER PROCEDURE DeleteStatusSP
	@Id INTEGER
AS
BEGIN
	DELETE FROM STATUS WHERE Id = @Id
END
GO

---------------------------------------------------------------------------------------------------------------

-- Procedimientos de la tabla PropertyType
-- CREATE
CREATE or ALTER PROCEDURE InsertPropertyTypeSP
	@Type VARCHAR(50),
	@Description VARCHAR(128)
AS
BEGIN
	INSERT INTO PropertyType (Type, Description, IsActive) VALUES (@Type, @Description, 1)
END
GO

-- READ
CREATE or ALTER PROCEDURE GetAllPropertyTypesSP
AS
BEGIN
	SELECT Id, Type, Description, IsActive FROM PropertyType WHERE IsActive = 1 ORDER BY Id ASC
END
GO

-- READ ALL

CREATE or ALTER PROCEDURE GetPropertyTypesSP
AS
BEGIN
	SELECT Id, Type, Description, IsActive FROM PropertyType
END
GO

-- Se debe crear el SP que trae todos los tipos independiente del estado para poder administrarlos

-- UPDATE
CREATE or ALTER PROCEDURE UpdatePropertyTypeSP
	@Id INTEGER,
	@Type VARCHAR(50),
	@Description VARCHAR(50),
	@IsActive BIT
AS
BEGIN
	UPDATE PropertyType
	SET
		Type = @Type,
		Description = @Description,
		IsActive = @IsActive
	WHERE Id = @Id
END 
GO

-- DELETE
CREATE or ALTER PROCEDURE DeletePropertyTypeSP
	@Id INTEGER
AS
BEGIN
	DELETE FROM PropertyType WHERE Id = @Id
END
GO



---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla TransactionType
-- CREATE
CREATE or ALTER PROCEDURE InsertTransactionTypeSP
	@Type VARCHAR(50),
	@Description VARCHAR(128)
AS
BEGIN
	INSERT INTO TransactionType (Type, Description, IsActive) VALUES (@Type, @Description, 1)
END
GO 

-- READ
CREATE or ALTER PROCEDURE GetAllTransactionTypesSP
AS
BEGIN
	SELECT Id, Type, Description, IsActive FROM TransactionType WHERE IsActive = 1
END
GO

-- READ ALL
CREATE or ALTER PROCEDURE GetTransactionTypesSP
AS
BEGIN
	SELECT Id, Type, Description, IsActive FROM TransactionType
END
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdateTransactionTypeSP
	@Id INTEGER,
	@Type VARCHAR(50),
	@Description VARCHAR(50),
	@IsAactive BIT
AS
BEGIN
	UPDATE TransactionType
	SET
		Type = @Type,
		Description = @Description,
		IsActive = @IsAactive
	WHERE Id = @Id
END 
GO

-- DELETE
CREATE or ALTER PROCEDURE DeleteTransactionTypeSP
	@Id INTEGER
AS
BEGIN
	DELETE FROM TransactionType WHERE Id = @Id
END
GO


---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla PropertyDoc
-- CREATE
CREATE or ALTER PROCEDURE InsertPropertyDocSP
	@Name VARCHAR(128),
	@DocUrl VARCHAR(256),
	@PropertyId INTEGER
AS 
BEGIN
	INSERT INTO PropertyDoc (Name, DocUrl, PropertyId) VALUES (@Name, @DocUrl, @PropertyId)
END
GO

-- READ
CREATE or ALTER PROCEDURE GetAllPropertyDocsSP
AS
BEGIN
	SELECT Id, Name, DocUrl, PropertyId FROM PropertyDoc
END
GO

CREATE or ALTER PROCEDURE GetPropertyDocsSP
	@PropertyId INTEGER
AS
BEGIN	
	SELECT Id, DocUrl, PropertyId FROM PropertyDoc
	WHERE PropertyId = @PropertyId
END
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdatePropertyDocSP
	@Id INTEGER,
	@Name VARCHAR(128),
	@DocUrl VARCHAR(256),
	@PropertyId INTEGER
AS 
BEGIN
	UPDATE PropertyDoc
	SET
		Name = @Name,
		DocUrl = @DocUrl,
		PropertyId = @PropertyId
	WHERE Id = @Id
END
GO

-- DELETE
CREATE or ALTER PROCEDURE DeletePropertyDocSP
	@Id INTEGER
AS
BEGIN 
	DELETE FROM PropertyDoc WHERE Id = @Id
END
GO
---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla PropertyImg
-- CREATE
CREATE or ALTER PROCEDURE InsertPropertyImgSP
	@ImageUrl VARCHAR(256),
	@PropertyId INTEGER
AS 
BEGIN
	INSERT INTO PropertyImg (ImageUrl, PropertyId) VALUES (@ImageUrl, @PropertyId)
END
GO

-- READ
CREATE or ALTER PROCEDURE GetAllPropertyImgsSP
AS
BEGIN
	SELECT Id, ImageUrl, PropertyId FROM PropertyImg
END
GO

-- Extraer las imágenes de una propiedad específica
CREATE or ALTER PROCEDURE GetPropertyImgsSP
	@PropertyId INTEGER
AS
BEGIN	
	SELECT Id, ImageUrl, PropertyId FROM PropertyImg
	WHERE PropertyId = @PropertyId
END
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdatePropertyImgSP
	@Id INTEGER,
	@ImageUrl VARCHAR(256),
	@PropertyId INTEGER
AS 
BEGIN
	UPDATE PropertyImg
	SET
		ImageUrl = @ImageUrl,
		PropertyId = @PropertyId
	WHERE Id = @Id
END
GO

-- DELETE
CREATE or ALTER PROCEDURE DeletePropertyImgSP
	@Id INTEGER
AS
BEGIN 
	DELETE FROM PropertyImg WHERE Id = @Id
END
GO
---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla Property
-- CREATE
CREATE or ALTER PROCEDURE InsertPropertySP
	@Title VARCHAR(128),
	@EmployeeId INTEGER,
	@Currency INTEGER,
	@Price DECIMAL(18,2),
	@AreaLand DECIMAL(10,2),
	@AreaBuild DECIMAL(10,2),
	@Description VARCHAR(MAX),
	@PropertyType INTEGER,
	@TransactionType INTEGER,
	@PropertyStatus INTEGER,
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO Property (Title, EmployeeId, Currency, Price, AreaLand, AreaBuild, Description, CreationDate, PropertyType, Transactiontype, PropertyStatus)
	VALUES (@Title, @EmployeeId, @Currency, @Price, @AreaLand, @AreaBuild, @Description, GETDATE(), @PropertyType, @TransactionType, 1)

	SET @Id = SCOPE_IDENTITY();
END
GO

-- READ
CREATE or ALTER PROCEDURE GetAllPropertiesSP
AS
BEGIN
	SELECT P.Id, E.Id as 'EmployeeId', CONCAT(U.Name, ' ', U.FirstLastName, ' ', U.SecondLastName) as 'EmployeeName', U.Email as 'EmployeeMail', U.PhoneNumber as 'EmployeePhone', E.ImageProfile as 'EmployeeImage', P.Title, P.Currency, C.Symbol, P.Price, P.AreaLand, P.AreaBuild,
	P.Description, P.CreationDate, P.SoldDate, P.PropertyType as 'PropertyType', PT.Type as 'PropertyTypeName', P.Transactiontype as 'TransactionType', TT.Type as 'TransactionTypeName', P.PropertyStatus as 'PropertyStatus', S.Name as 'PropertyStatusName'
	FROM Property P
	JOIN Employee E ON P.EmployeeId = E.Id
	JOIN Currency C ON P.Currency = C.Id
	JOIN Users U ON E.Id = U.Id
	JOIN PropertyType PT ON P.PropertyType = PT.Id
	JOIN TransactionType TT ON P.Transactiontype = TT.Id
	JOIN Status S ON P.PropertyStatus = S.Id
END
GO 
-- Obtener una propiedad específica
CREATE or ALTER PROCEDURE GetPropertySP
	@Id INTEGER
AS
BEGIN	
	SELECT P.Id, E.Id as 'EmployeeId', CONCAT(U.Name, ' ', U.FirstLastName, ' ', U.SecondLastName) as 'EmployeeName', U.Email as 'EmployeeMail', U.PhoneNumber as 'EmployeePhone', E.ImageProfile as 'EmployeeImage', P.Title, P.Currency, C.Symbol, P.Price, P.AreaLand, P.AreaBuild,
	P.Description, P.CreationDate, PT.Id as 'PropertyType', PT.Type as 'PropertyTypeName', TT.Id as 'TransactionType', TT.Type as 'TransactionTypeName', S.Id as 'PropertyStatus', S.Name as 'PropertyStatusName'
	FROM Property P
	JOIN Employee E ON P.EmployeeId = E.Id
	JOIN Currency C ON P.Currency = C.Id
	JOIN Users U ON E.Id = U.Id
	JOIN PropertyType PT ON P.PropertyType = PT.Id
	JOIN TransactionType TT ON P.Transactiontype = TT.Id
	JOIN Status S ON P.PropertyStatus = S.Id
	WHERE P.Id = @Id
END
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdatePropertySP
	@Id INTEGER,
	@Title VARCHAR(128),
	@EmployeeId INTEGER,
	@Currency INTEGER,
	@Price DECIMAL(18,2),
	@AreaLand DECIMAL(10,2),
	@AreaBuild DECIMAL(10,2),
	@Description VARCHAR(MAX),
	@PropertyType INTEGER,
	@TransactionType INTEGER,
	@PropertyStatus INTEGER
AS
BEGIN
	UPDATE Property
	SET
		Title = @Title,
		EmployeeId = @EmployeeId,
		Currency = @Currency,
		Price = @Price,
		AreaLand = @AreaLand,
		AreaBuild = @AreaBuild,
		Description = @Description,
		PropertyType = @PropertyType,
		Transactiontype = @TransactionType,
		PropertyStatus = @PropertyStatus
	WHERE Id = @Id
END
GO

-- Cambiar entre disponible y no disponible
CREATE or ALTER PROCEDURE ChangePropertyStatusSP
    @PropertyId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StatusActual INT;
    -- Obtener el estado actual del empleado
    SELECT @StatusActual = PropertyStatus
    FROM Property
    WHERE Id = @PropertyId;
    -- Verificar si se encontró un empleado con el UserId dado
    IF @StatusActual IS NOT NULL
    BEGIN
        -- Actualizar el estado según la lógica proporcionada
        UPDATE Property
        SET PropertyStatus = CASE WHEN @StatusActual = 1 THEN 2 ELSE 1 END,
            SoldDate = CASE WHEN @StatusActual = 1 THEN GETDATE() ELSE NULL END
        WHERE Id = @PropertyId;
    END
END
GO

-- DELETE
CREATE or ALTER PROCEDURE DeletePropertySP
	@Id INTEGER
AS
BEGIN
	DELETE FROM Property WHERE Id = @Id
END
GO

-- Filtrado de estados para la tabla
CREATE or ALTER PROCEDURE GetPropertiesStatusesSP
AS
BEGIN
	SELECT Id, Name, Description, RelatedTable, IsActive from STATUS 
	WHERE RelatedTable = 'Propiedades'
	AND IsActive = 1;
END
GO

-- Procdimientos de la tabla PropertyAddress

-- CREATE
CREATE or ALTER PROCEDURE InsertPropertyAddressSP
	@PropertyId INTEGER,
	@DistrictId INTEGER,
	@OtherSigns VARCHAR(MAX)
AS
BEGIN
	INSERT INTO PropertyAddress (PropertyId, DistrictId, OtherSigns)
	VALUES (@PropertyId, @DistrictId, @OtherSigns)
END
GO

-- READ
CREATE or ALTER PROCEDURE GetAllPropertyAddressSP
AS
BEGIN
	SELECT Id, PropertyId, DistrictId, OtherSigns FROM PropertyAddress
END
GO

-- Obtener la dirección de una propiedad específica
CREATE or ALTER PROCEDURE GetPropertyAddressSP
	@PropertyId INTEGER
AS
BEGIN
	Select PA.Id, PA.PropertyId, P.Id as 'ProvinceId', P.Name as 'Province', C.Id as 'CantonId', C.Name as 'Canton', D.Id as 'DistrictId', D.Name as 'District', CONCAT(P.Name, ', ', C.Name, ', ', D.Name ) as 'FullDirection', PA.OtherSigns  
	FROM PropertyAddress PA
	JOIN District D ON PA.DistrictId = D.Id
	JOIN Canton C ON D.Canton = C.Id
	JOIN Province P ON C.Province = P.Id
	WHERE PA.PropertyId =  @PropertyId
END
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdatePropertyAddressSP
	@Id INTEGER,
	@PropertyId INTEGER,
	@DistrictId INTEGER,
	@OtherSigns VARCHAR(MAX)
AS
BEGIN
	UPDATE PropertyAddress
	SET
		PropertyId = @PropertyId,
		DistrictId = @DistrictId,
		OtherSigns = @OtherSigns
	WHERE Id = @Id
END
GO

-- DELETE
CREATE or ALTER PROCEDURE DeletePropertyAddressSP
	@Id INTEGER
AS
BEGIN
	DELETE FROM PropertyAddress WHERE Id = @Id
END
GO

-- Procedimientos para traer provincias, cantones y distritos que se encuentran activos para usarlos en las propiedades

CREATE OR ALTER PROCEDURE InsertProvinceSP
    @Name VARCHAR(50)
AS
BEGIN
    INSERT INTO Province (Name, IsActive) 
    VALUES (@Name, 1)
END
GO

CREATE OR ALTER PROCEDURE InsertCantonSP
    @Name VARCHAR(50),
    @ProvinceId INTEGER
AS
BEGIN
    INSERT INTO Canton (Name, Province, IsActive) 
    VALUES (@Name, @ProvinceId, 1)
END
GO

CREATE OR ALTER PROCEDURE InsertDistrictSP
    @Name VARCHAR(50),
    @CantonId INTEGER
AS
BEGIN
    INSERT INTO District (Name, Canton, IsActive) 
    VALUES (@Name, @CantonId, 1)
END
GO

CREATE or ALTER PROCEDURE GetAllProvincesSP
AS
BEGIN
	SELECT Id, Name, IsActive FROM Province
	WHERE IsActive = 1
	ORDER BY Id
END
GO

CREATE or ALTER PROCEDURE GetAllCantonsSP
	@ProvinceId INTEGER
AS
BEGIN
	SELECT Id, Name, Province, IsActive FROM Canton
	WHERE Province = @ProvinceId
	AND IsActive = 1
END
GO

CREATE or ALTER PROCEDURE GetAllDistrictsSP
	@CantonId INTEGER
AS
BEGIN
	SELECT Id, Name, Canton, IsActive FROM District
	WHERE Canton = @CantonId
	AND IsActive = 1
END
GO

-- SP para obtener todas las provincias (activas e inactivas)
CREATE OR ALTER PROCEDURE GetProvincesSP
AS
BEGIN
    SELECT Id, Name, IsActive
    FROM Province
    ORDER BY Id;
END;
GO

-- SP para obtener todos los cantones (activos e inactivos) de una provincia específica
CREATE OR ALTER PROCEDURE GetCantonsSP
AS
BEGIN
    BEGIN
        SELECT Id, Name, Province, IsActive
        FROM Canton
        ORDER BY Id;
    END;
END;
GO

-- SP para obtener todos los distritos (activos e inactivos) de un cantón específico
CREATE OR ALTER PROCEDURE GetDistrictsSP  
AS
BEGIN
    BEGIN
        SELECT Id, Name, Canton, IsActive
        FROM District
        ORDER BY Id;
    END;
END;
GO


-- UPDATE
CREATE OR ALTER PROCEDURE EditProvinceSP
    @Id INTEGER,
    @Name VARCHAR(50)
AS
BEGIN
    UPDATE Province
    SET Name = @Name
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE EditCantonSP
    @Id INTEGER,
    @Name VARCHAR(50),
    @ProvinceId INTEGER,
	@IsActive BIT
AS
BEGIN
    UPDATE Canton
    SET Name = @Name,
        Province = @ProvinceId,
		IsActive = @IsActive
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE EditDistrictSP
    @Id INTEGER,
    @Name VARCHAR(50),
    @CantonId INTEGER,
	@IsActive BIT
AS
BEGIN
    UPDATE District
    SET Name = @Name,
        Canton = @CantonId,
		IsActive = @IsActive
    WHERE Id = @Id;
END;
GO


---------------------------------------------------------------------------------------------------------------
--Jeff
-- Procedimientos de la tabla DocumentType
--Create
CREATE or ALTER PROCEDURE InsertDocumentTypeSP
	@Document VARCHAR(50)
AS
BEGIN 
	INSERT INTO DocumentType (Document, IsActive) VALUES (@Document, 1)
END
GO
-- READ, trae todos los tipos de documento activos
CREATE or ALTER PROCEDURE GetAllDocumentTypeSP 
AS
BEGIN
	SELECT Id, Document, IsActive FROM DocumentType WHERE IsActive = 1 ORDER BY Id ASC;
END 
GO

-- Se debe crear un SP nuevo para traer todos independiente del estado para poder administrarlos

-- READ ALL
CREATE or ALTER PROCEDURE GetDocumentTypeSP 
AS
BEGIN
	SELECT Id, Document, IsActive FROM DocumentType;
END 
GO

-- UPDATE 
CREATE or ALTER PROCEDURE UpdateDocumentTypeSP
	@Id INTEGER,
	@Document VARCHAR(50),
	@IsActive BIT
AS
BEGIN
	UPDATE DocumentType
	SET 
		Document = @Document
	WHERE Id = @Id
END
GO

-- DELETE
-- Este SP se debe modificar para que cambie el estado en lugar de eliminar
CREATE or ALTER PROCEDURE DeleteDocumentTypeSP
	@Id INTEGER
AS
BEGIN
	DELETE FROM DocumentType WHERE Id = @Id
END
GO


---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla Roles
--Insert
CREATE or ALTER PROCEDURE InsertRoleSP
    @RoleName VARCHAR(50),
    @Description VARCHAR(50)
AS
BEGIN
    INSERT INTO Role (RoleName, Description, IsActive)
    VALUES (@RoleName, @Description, 1);
END;
GO

-- READ
CREATE or ALTER PROCEDURE GetAllRoleSP
AS
BEGIN
    SELECT Id, RoleName, Description, IsActive FROM Role WHERE IsActive = 1
END;
GO

-- READ ALL
CREATE or ALTER PROCEDURE GetRoleSP
AS
BEGIN
    SELECT Id, RoleName, Description, IsActive FROM Role
END;
GO
-- UPDATE 
CREATE or ALTER PROCEDURE UpdateRoleSP
    @RoleId INT,
    @NuevoRoleName VARCHAR(50),
    @NuevaDescripcion VARCHAR(50),
	@IsActive BIT
AS
BEGIN
    UPDATE Role
    SET RoleName = @NuevoRoleName,
        Description = @NuevaDescripcion,
		IsActive = @IsActive
    WHERE Id = @RoleId;
END;
GO

-- DELETE
CREATE or ALTER PROCEDURE DeleteRoleSP
    @RoleId INT
AS
BEGIN
    DELETE FROM Role
    WHERE Id = @RoleId;
END;
GO


---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla Users
-- Insert
CREATE OR ALTER PROCEDURE InsertUserSP
    @DocumentType INT,
    @Identification VARCHAR(20),
    @Name VARCHAR(50),
    @FirstLastName VARCHAR(50),
    @SecondLastName VARCHAR(50),
    @PhoneNumber VARCHAR(20),
    @Email VARCHAR(128),
    @ID INT OUTPUT
AS
BEGIN
    -- Verifica si tanto la identificación como el correo electrónico no existen en la tabla.
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Identification = @Identification) AND NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        -- Inserta un nuevo usuario.
        INSERT INTO Users (DocumentType, Identification, Name, FirstLastName, SecondLastName, PhoneNumber, Email, CreationDate)
        VALUES (@DocumentType, @Identification, @Name, @FirstLastName, @SecondLastName, @PhoneNumber, @Email, GETDATE());

        -- Obtiene el ID generado para el nuevo usuario y lo asigna al parámetro de salida @ID.
        SET @ID = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Si la identificación existe, verifica si el correo electrónico o el número de teléfono son diferentes.
        IF EXISTS (SELECT 1 FROM Users WHERE Identification = @Identification)
        BEGIN
            IF EXISTS (SELECT 1 FROM Users WHERE Identification = @Identification AND (Email != @Email OR PhoneNumber != @PhoneNumber))
            BEGIN
                -- Actualiza el correo electrónico y/o el número de teléfono si son diferentes.
                UPDATE Users
                SET Email = CASE WHEN Email != @Email THEN @Email ELSE Email END,
                    PhoneNumber = CASE WHEN PhoneNumber != @PhoneNumber THEN @PhoneNumber ELSE PhoneNumber END
                WHERE Identification = @Identification;

                -- Obtiene el ID del usuario actualizado.
                SELECT @ID = ID FROM Users WHERE Identification = @Identification;
            END
            ELSE
            BEGIN
                -- Si ni el correo ni el número de teléfono son diferentes, no se realiza ninguna actualización.
				SELECT @ID = ID FROM Users WHERE Identification = @Identification;
            END
        END
        ELSE
        BEGIN
            -- Si la identificación no existe pero el correo electrónico ya está en uso, no se puede insertar el usuario.
            SET @ID = -3; -- Establece un valor negativo para indicar que la inserción no pudo realizarse.
        END
    END
END;
GO

-- Read
CREATE or ALTER PROCEDURE GetAllUsersSP
AS
BEGIN
    SELECT Id, DocumentType, Identification, Name, FirstLastName, SecondLastName, PhoneNumber, Email, CreationDate
    FROM Users;
END;
GO

-- Extraer solo los usuarios (Excluye los empleados)
CREATE or ALTER PROCEDURE GetAllClientsSP
AS
BEGIN
		SELECT  U.Id,
		CASE
		WHEN len(U.identification) = 10 THEN
			substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4)
		WHEN len(U.identification) > 10 THEN
			substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4) + '-' + substring(U.identification, 11, len(U.identification) - 10)
			ELSE U.identification
		END
		AS Identification, concat(U.firstlastname, ' ', U.secondlastname, ' ', U.NAME) AS 'FullName',
		CASE
		WHEN len(U.phonenumber) >= 8 THEN
			substring(U.phonenumber, 1, 4) + '-' + substring(U.phonenumber, 5, 4)
			ELSE U.phonenumber 
		END AS PhoneNumber,
		U.Email AS 'Email',
		U.CreationDate AS 'CreationDate'
		FROM Users U
		LEFT JOIN Employee E ON U.Id = E.UserId 
		WHERE E.UserId IS NULL
	END;
GO

CREATE or ALTER PROCEDURE GetUserByIdentificationSP
    @Identification VARCHAR(20)
AS
BEGIN
    SELECT	id,
            identification,
            Name,
            FirstLastName,
            SecondLastName,
            PhoneNumber,
            Email
    FROM
        Users
    WHERE
        Identification = @Identification;
END;
GO

-- Update
CREATE or ALTER PROCEDURE UpdateUserSP
    @UserId INT,
    @DocumentType INT,
    @Identification VARCHAR(20),
    @Name VARCHAR(50),
    @FirstLastName VARCHAR(50),
    @SecondLastName VARCHAR(50),
    @PhoneNumber VARCHAR(20),
    @Email VARCHAR(128)
AS
BEGIN
    UPDATE Users
    SET DocumentType = @DocumentType,
        Identification = @Identification,
        Name = @Name,
        FirstLastName = @FirstLastName,
        SecondLastName = @SecondLastName,
        PhoneNumber = @PhoneNumber,
        Email = @Email
    WHERE Id = @UserId;
END;
GO

-- Delete
CREATE or ALTER PROCEDURE DeleteUserSP
    @UserId INT
AS
BEGIN
    DELETE FROM Users
    WHERE Id = @UserId;
END;
GO

---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla Employee
-- Insert
CREATE or ALTER PROCEDURE InsertEmployeeSP
    @UserId INT,
    @PasswordTemp VARCHAR(50),
    @Role INT
AS
BEGIN
		IF NOT EXISTS (SELECT 1 FROM Employee WHERE UserId = @UserId)
		BEGIN
			INSERT INTO Employee (UserId, Password, PasswordTemp,Role, Status,IsTemporary)
			VALUES (@UserId, @PasswordTemp,@PasswordTemp, @Role, 3,1);
		END;
END;
GO

-- Read
CREATE or ALTER PROCEDURE GetAllEmployeesSP
AS
BEGIN
	SELECT  U.id AS 'IdTEmployee',
	CASE
	WHEN len(U.identification) = 10 THEN
		substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4)
	WHEN len(U.identification) > 10 THEN
		substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4) + '-' + substring(U.identification, 11, len(U.identification) - 10)
		ELSE U.identification
	END
	AS Identification, concat(U.firstlastname, ' ', U.secondlastname, ' ', U.NAME) AS 'FullName',
	CASE
	WHEN len(U.phonenumber) >= 8 THEN
		substring(U.phonenumber, 1, 4) + '-' + substring(U.phonenumber, 5, 4)
		ELSE U.phonenumber
	END
	AS phonenumber, U.email, U.creationdate, E.userid, R.rolename, E.Status, S.Name 'StatusName', E.imageprofile, E.retirementdate,E.IsTemporary
	FROM employee E 
	JOIN users U ON E.userid = U.id 
	JOIN role r ON e.role = R.id 
	JOIN status S ON e.status = S.id 
	ORDER BY fullname ASC;
END;
GO

-- Update Profile Img
CREATE or ALTER PROCEDURE UpdateEmployeeImgSP
    @UserId INT,
    @ImageProfile VARCHAR(100)
AS
BEGIN
    UPDATE Employee
    SET ImageProfile = @ImageProfile
    WHERE UserId = @UserId;
END;
GO

-- Update
CREATE or ALTER PROCEDURE UpdateEmployeeSP
    @EmployeeId INT,
    @PhoneNumber VARCHAR(20),
    @Email VARCHAR(50),
    @Role INT,
    @Status INT
AS
BEGIN
    DECLARE @PreviousStatus INT;
    
    -- Obtener el estado anterior del empleado
    SELECT @PreviousStatus = Status
    FROM Employee
    WHERE UserId = @EmployeeId;

    -- Actualizar datos en la tabla Employee
    UPDATE Employee
    SET Status = @Status,
        Role = @Role
    WHERE UserId = @EmployeeId;

    -- Actualizar datos en la tabla Users
    UPDATE Users  
    SET PhoneNumber = @PhoneNumber,
        Email = @Email
    WHERE Id = @EmployeeId;

    -- Si el estado anterior era 5 (bloqueado) y el nuevo estado es 3 (activo),
    -- actualiza FailedAttempts a 0
    IF @PreviousStatus = 5 AND @Status = 3
    BEGIN
        UPDATE Employee
        SET FailedAttempts = 0
        WHERE UserId = @EmployeeId;
    END;
END;
GO

-- Cambiar el estado de un empleado de Activo a Inactivo y viceversa
CREATE or ALTER PROCEDURE ChangeEmployeeStatusSP
@UserId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @StatusActual INT;
    DECLARE @CurrentRetirementDate DATETIME;

    -- Obtener el estado actual del empleado y su RetirementDate actual
    SELECT  @StatusActual = Status,
            @CurrentRetirementDate = RetirementDate
    FROM Employee
    WHERE UserId = @UserId;

    -- Verificar si se encontró un empleado con el UserId dado
    IF @StatusActual IS NOT NULL
    BEGIN
        -- Actualizar el estado según la lógica proporcionada
        UPDATE Employee
        SET FailedAttempts = 0,
		Status = CASE WHEN @StatusActual = 3 THEN 4 ELSE 3 END,
            RetirementDate = CASE 
                                WHEN @StatusActual = 3 THEN GETDATE()
                                ELSE COALESCE(RetirementDate, @CurrentRetirementDate)
                            END
        WHERE UserId = @UserId;
    END
END;
GO

--Delete
CREATE or ALTER PROCEDURE DeleteEmployeeSP
    @EmployeeId INT
AS
BEGIN
    DELETE FROM Employee
    WHERE Id = @EmployeeId;
END;
GO

-- Filtrado de estados para la tabla
CREATE or ALTER PROCEDURE GetEmployeeStatusesSP 
AS
BEGIN
	SELECT Id, Name, Description, RelatedTable, IsActive from STATUS 
	WHERE RelatedTable = 'Empleado'
	AND IsActive = 1;
END
GO

-- Obtener un empleado por su ID
CREATE or ALTER PROCEDURE GetEmployeeSP
@UserId INT
AS
BEGIN
	SELECT   U.Id as 'IdTEmployee',U.DocumentType, U.Identification,U.FirstLastName,U.SecondLastName, U.name,U.PhoneNumber, U.Email,U.CreationDate, E.Role,e.Status,
    R.RoleName, S.Name as 'StatusName', E.ImageProfile,E.RetirementDate,E.IsTemporary
	FROM Employee E join Users U on E.UserId = U.Id 
    join Role R on E.Role = R.Id join Status S on E.Status = S.Id 
    where e.UserId = @UserId;
END
GO

--sp para el login de los empleados
CREATE OR ALTER PROCEDURE LoginEmployeeSP
    @Identificacion VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT;
    DECLARE @Status INT;
    DECLARE @FailedAttempts INT;

    -- Inicializar variables
    SET @UserId = NULL;
    SET @Status = NULL;
    SET @FailedAttempts = 0;

    -- Obtener el UserId basado en la Identificación
    SELECT @UserId = U.Id, @FailedAttempts = E.FailedAttempts
    FROM dbo.Users U
    INNER JOIN dbo.Employee E ON U.Id = E.UserId
    WHERE U.Identification = @Identificacion;

    IF @UserId IS NOT NULL
    BEGIN
        -- Verificar si el estado del empleado es 3 (activo)
        SELECT @Status = E.Status
        FROM dbo.Employee E
        WHERE E.UserId = @UserId;

        IF @Status = 3
        BEGIN
            -- Validar la contraseña
            IF EXISTS (SELECT 1 FROM dbo.Employee WHERE UserId = @UserId AND Password = @Password)
            BEGIN
                -- La contraseña es correcta, restablecer FailedAttempts a 0
                UPDATE dbo.Employee
                SET FailedAttempts = 0
                WHERE UserId = @UserId;

                -- Devolver los datos del empleado
                SELECT 
                    U.Id,
                    U.DocumentType,
                    U.Identification,
                    U.Name,
                    U.FirstLastName,
                    U.SecondLastName,
                    U.PhoneNumber,
                    U.Email,
                    U.CreationDate,
                    E.Id AS IdTEmployee,
                    E.UserId,
                    E.Role AS Role,
                    R.RoleName,
                    E.Status,
                    S.Name AS StatusName,
                    E.ImageProfile,
                    E.RetirementDate,
                    E.IsTemporary,
					'' as Token
                FROM dbo.Users U
                INNER JOIN dbo.Employee E ON U.Id = E.UserId
                INNER JOIN dbo.Role R ON E.Role = R.Id
                INNER JOIN dbo.Status S ON E.Status = S.Id
                WHERE E.UserId = @UserId;
            END
            ELSE
            BEGIN
                -- La contraseña es incorrecta, aumentar FailedAttempts
                UPDATE dbo.Employee
                SET FailedAttempts = FailedAttempts + 1
                WHERE UserId = @UserId;

                -- Verificar si FailedAttempts alcanzó el límite
                IF @FailedAttempts + 1 >= 3
                BEGIN
                    -- Cambiar el estado del empleado a 5 (bloqueado)
                    UPDATE dbo.Employee
                    SET Status = 5
                    WHERE UserId = @UserId;
                END
            END
        END
    END

    -- Devolver el nuevo estado del empleado si está bloqueado
    IF @Status = 5
    BEGIN
        IF EXISTS (SELECT 1 FROM dbo.Employee WHERE UserId = @UserId AND Password = @Password)
        BEGIN
            SELECT 
                U.Id,
                U.DocumentType,
                U.Identification,
                U.Name,
                U.FirstLastName,
                U.SecondLastName,
                U.PhoneNumber,
                U.Email,
                U.CreationDate,
                E.Id AS IdTEmployee,
                E.UserId,
                E.Role AS Role,
                R.RoleName,
                E.Status,
                S.Name AS StatusName,
                E.ImageProfile,
                E.RetirementDate,
                E.IsTemporary,
				'' as Token
            FROM dbo.Users U
            INNER JOIN dbo.Employee E ON U.Id = E.UserId
            INNER JOIN dbo.Role R ON E.Role = R.Id
            INNER JOIN dbo.Status S ON E.Status = S.Id
            WHERE E.UserId = @UserId;
        END
    END
END;
GO

CREATE OR ALTER PROCEDURE GetEmployeeInfoByRoleAndStatus
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Employee.Id AS EmployeeId, Users.Email AS EmployeeEmail
    FROM Employee
    INNER JOIN Users ON Employee.UserId = Users.Id
    WHERE Employee.Role = 3
    AND Employee.Status = 3;
END;
GO

CREATE OR ALTER PROCEDURE GetVendorAndAdminEmails
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Employee.Id AS EmployeeId, Users.Email AS EmployeeEmail
    FROM Employee
    INNER JOIN Users ON Employee.UserId = Users.Id
    WHERE Employee.Role IN (1, 2)
    AND Employee.Status = 3;
END;
GO

-- Change Password
CREATE OR ALTER PROCEDURE ResetPasswordSP
    @Identification VARCHAR(200),
    @Password VARCHAR(200),
    @PasswordTemp VARCHAR(200),
    @IsTemporary BIT
AS
BEGIN
    DECLARE @UserId BIGINT

    SELECT @UserId = U.Id
    FROM Users U
    JOIN Employee E ON U.Id = E.UserId
    WHERE Identification = @Identification 
        AND PasswordTemp = @PasswordTemp
        AND Status = 3

    IF (@UserId IS NOT NULL)
    BEGIN
        UPDATE Employee
        SET Password = @Password,
            IsTemporary = @IsTemporary
        WHERE UserId = @UserId
    END
END
GO

--Forget password
CREATE OR ALTER PROCEDURE ForgetPasswordSP
    @Identification VARCHAR(200),
    @Password VARCHAR(200),
    @IsTemporary BIT
AS
BEGIN
    DECLARE @UserId BIGINT

    SELECT @UserId = U.Id
    FROM Users U
    JOIN Employee E ON U.Id = E.UserId
    WHERE Identification = @Identification 
        AND Status = 3

    IF (@UserId IS NOT NULL)
    BEGIN
        UPDATE Employee
        SET Password =  @Password, 
            PasswordTemp = @Password,
            IsTemporary = @IsTemporary
        WHERE UserId = @UserId;
			--Restorna el empleado
	SELECT  U.Id as 'IdTEmployee',U.DocumentType, U.Identification,U.FirstLastName,U.SecondLastName, U.name,U.PhoneNumber, U.Email,U.CreationDate, E.Role,e.Status,
            R.RoleName, S.Name as 'StatusName', E.ImageProfile,E.RetirementDate,E.IsTemporary
	FROM Employee E join Users U on E.UserId = U.Id 
    join Role R on E.Role = R.Id join Status S on E.Status = S.Id 
    where e.UserId = @UserId;
    END
END
GO

--Reset Password
CREATE OR ALTER PROCEDURE ChangePasswordSP
    @Identification VARCHAR(200),
    @Password VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON; -- Esto evita que se muestren los mensajes de "row affected"

    DECLARE @UserId BIGINT

    SELECT @UserId = U.Id
    FROM Users U
    JOIN Employee E ON U.Id = E.UserId
    WHERE Identification = @Identification 
        AND Status = 3
    
    IF (@UserId IS NOT NULL)
    BEGIN
        -- Actualizar la contraseña y mostrar el mensaje de "row affected"
        SET NOCOUNT OFF;
        UPDATE Employee
        SET Password =  @Password
        WHERE UserId = @UserId;
    END

    SET NOCOUNT OFF; -- Restaurar el comportamiento predeterminado
END
GO 
---------------------------------------------------------------------------------------------------------------
-- Josday
-- Procedimientos de la tabla News
-- Create
CREATE or ALTER PROCEDURE InsertNewsSP
    @EmployeeId INTEGER,
    @Title VARCHAR(256),
    @Description VARCHAR(512),
    @Url VARCHAR(MAX),
    @ImageUrl VARCHAR(MAX)
AS
BEGIN
    INSERT INTO News (EmployeeId, CreationDate, Title, Description, Url, ImageUrl)
    VALUES (@EmployeeId, GETDATE(), @Title, @Description, @Url, @ImageUrl);
END;
GO
-- Read
CREATE OR ALTER PROCEDURE GetAllNewsSP
AS
BEGIN
    SELECT Id, EmployeeId, CreationDate, Title, Description, Url, ImageUrl
    FROM News
    ORDER BY CreationDate DESC;
END;
GO

-- Get Id News
CREATE or ALTER PROCEDURE GetNewsByIdSP
    @NewsId INTEGER
AS
BEGIN
    -- Seleccionar las noticias por su ID
    SELECT Id, EmployeeId, CreationDate, Title, Description, Url, ImageUrl
    FROM News
    WHERE Id = @NewsId;
END;
GO
-- Uptade
CREATE or ALTER PROCEDURE UpdateNewsSP
    @NewsId INTEGER,
    @EmployeeId INTEGER ,
    @Title VARCHAR(256),
    @Description VARCHAR(512),
    @Url VARCHAR(MAX),
    @ImageUrl VARCHAR(MAX)
AS
BEGIN
    UPDATE News
    SET 
        EmployeeId = @EmployeeId ,
        CreationDate = GETDATE(),
        Title = @Title,
        Description = @Description,
        Url = @Url,
        ImageUrl = @ImageUrl
    WHERE Id = @NewsId;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteNewsSP
    @NewsId INTEGER
AS
BEGIN
    DELETE FROM News
    WHERE Id = @NewsId;
END;
GO

-- UPDATE
CREATE or ALTER PROCEDURE UpdateUrlImgSP
    @Id INT,
    @ImageUrl VARCHAR(256)
AS
BEGIN
    UPDATE News
    SET ImageUrl = @ImageUrl
    WHERE Id = @Id;
END;
GO

---------------------------------------------------------------------------------------------------------------
-- Procedimientos de la tabla LogAudit
-- Create
CREATE or ALTER PROCEDURE InsertAuditLogSP
    @EmployeeId INTEGER,
    @CurrentAudit VARCHAR(512)
AS
BEGIN
    INSERT INTO AuditLog (EmployeeId, CurrentAudit, CreationDateAudit)
    VALUES (@EmployeeId, @CurrentAudit, GETDATE());
END;
GO
-- Read
CREATE or ALTER PROCEDURE GetAllAuditLogSP
AS
BEGIN
    SELECT EmployeeId, CurrentAudit, CreationDateAudit FROM AuditLog;
END;
GO
-- Uptade
CREATE or ALTER PROCEDURE UpdateAuditLogSP
    @AuditLogId INTEGER,
    @EmployeeId INTEGER,
    @CurrentAudit VARCHAR(512)
AS
BEGIN
    UPDATE AuditLog
    SET 
        EmployeeId = @EmployeeId,
        CurrentAudit = @CurrentAudit,
        CreationDateAudit = GETDATE()
    WHERE Id = @AuditLogId;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteAuditLogSP
    @AuditLogId INTEGER
AS
BEGIN
    DELETE FROM AuditLog
    WHERE Id = @AuditLogId;
END;
GO

-- Procedimientos para la tabla QuoteRooms
-- Create
CREATE or ALTER PROCEDURE InsertQuoteRoomSP
    @RoomName VARCHAR(50)
AS
BEGIN
    INSERT INTO QuoteRooms (RoomName, IsActive)
    VALUES (@RoomName, 1)
END;
GO
-- Read
CREATE or ALTER PROCEDURE GetRoomsSP
AS
BEGIN
	SELECT Id, RoomName, IsActive 
    FROM QuoteRooms
	ORDER BY RoomName ASC
END
GO

-- Obtener una habitación por su ID
CREATE OR ALTER PROCEDURE GetRoomByIdSP
    @IdRoom INTEGER
AS
BEGIN
    SELECT Id, RoomName, IsActive
    FROM QuoteRooms
    WHERE Id = @IdRoom;
END;
GO

-- Update
CREATE or ALTER PROCEDURE UpdateQuoteRoomSP
    @Id INTEGER,
    @RoomName VARCHAR(50),
	@IsActive BIT
AS
BEGIN
    UPDATE QuoteRooms
    SET RoomName = @RoomName,
		IsActive = @IsActive
    WHERE Id = @Id;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteQuoteRoomSP
    @Id INTEGER
AS
BEGIN
    DELETE FROM QuoteRooms
    WHERE Id = @Id;
END;
GO

-- SP para cambiar el estado de una habitación
CREATE OR ALTER PROCEDURE ChangeRoomStatusSP
    @RoomId INT
AS
BEGIN
    UPDATE QuoteRooms
    SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
    WHERE Id = @RoomId;
END;
GO

-- Procedimientos para la tabla QuoteDetailsPerRoom
--Create
CREATE OR ALTER PROCEDURE InsertQuoteDetailsPerRoomSP
    @QuoteRoomId INTEGER,
    @RoomQuantity INTEGER = 1 -- Asigna 1 como valor predeterminado si no se proporciona ningún valor
AS
BEGIN
    -- Declarar una variable para almacenar el ID más reciente
    DECLARE @LastQuoteId INTEGER
    -- Seleccionar el último ID de QuoteDetails directamente de la tabla
    SELECT TOP 1 @LastQuoteId = Id FROM Quote ORDER BY Id DESC
    -- Insertar en la tabla QuoteDetailsPerRoom
    INSERT INTO QuoteDetailsPerRoom (QuoteId, QuoteRoomId, Quantity)
    VALUES (@LastQuoteId, @QuoteRoomId, @RoomQuantity)
END;
GO

/* 
-- Read
CREATE or ALTER PROCEDURE GetQuoteDetailsPerRoomSP
AS
BEGIN
    SELECT Id, QuoteDetailsId, QuoteRoomId
    FROM QuoteDetailsPerRoom
END;
GO
-- Update
CREATE or ALTER PROCEDURE UpdateQuoteDetailsPerRoomSP
    @Id INTEGER,
    @QuoteDetailsId INTEGER,
    @QuoteRoomId INTEGER
AS
BEGIN
    UPDATE QuoteDetailsPerRoom
    SET QuoteDetailsId = @QuoteDetailsId,
        QuoteRoomId = @QuoteRoomId
    WHERE Id = @Id;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteQuoteDetailsPerRoomSP
    @Id INTEGER
AS
BEGIN
    DELETE FROM QuoteDetailsPerRoom
    WHERE Id = @Id;
END;
GO
-- Procedimientos para la tabla Quote
*/
-- Create
CREATE or ALTER PROCEDURE InsertQuoteSP
    @ClientId INTEGER,
	@Details VARCHAR(512)
AS
BEGIN
    INSERT INTO Quote (ClientId, CreationDate, Status, Details)
    VALUES (@ClientId, GETDATE(), 6, @Details)
END;
GO

/*
-- Read
CREATE or ALTER PROCEDURE GetQuoteSP
AS
BEGIN
    SELECT Id, ClientId, CreationDate, Status, Details
    FROM Quote
END;
GO

*/

CREATE OR ALTER PROCEDURE GetQuoteDetails
AS
BEGIN
    SET NOCOUNT ON;

SELECT q.Id AS QuoteId,
       CONCAT(u.Name, ' ', u.FirstLastName) AS ClientFullName,
       CASE
           WHEN LEN(u.PhoneNumber) >= 8 THEN
               SUBSTRING(u.PhoneNumber, 1, 4) + '-' + SUBSTRING(u.PhoneNumber, 5, 4)
           ELSE u.PhoneNumber
       END AS PhoneNumber,
       u.Email,
       CASE
           WHEN LEN(u.Identification) = 10 THEN
               SUBSTRING(u.Identification, 1, 2) + '-' + SUBSTRING(u.Identification, 3, 4) + '-' + SUBSTRING(u.Identification, 7, 4)
           WHEN LEN(u.Identification) > 10 THEN
               SUBSTRING(u.Identification, 1, 2) + '-' + SUBSTRING(u.Identification, 3, 4) + '-' + SUBSTRING(u.Identification, 7, 4) + '-' + SUBSTRING(u.Identification, 11, LEN(u.Identification) - 10)
           ELSE u.Identification
       END AS ClientIdentificationNumber,
       q.CreationDate,
       s.Description AS StatusDescription,
       q.DocUrl,
       q.Details,
       q.Status,
       STRING_AGG(CONCAT(qr.RoomName, ': ', qdpr.Quantity), ', ') AS RoomDetails
FROM Quote q
INNER JOIN Users u ON q.ClientId = u.Id
INNER JOIN QuoteDetailsPerRoom qdpr ON q.Id = qdpr.QuoteId
INNER JOIN QuoteRooms qr ON qdpr.QuoteRoomId = qr.Id
INNER JOIN Status s ON q.Status = s.Id
GROUP BY q.Id, u.Name, u.FirstLastName, u.Identification, u.PhoneNumber,
         u.Email, q.CreationDate, s.Description, q.DocUrl, q.Details, q.Status;

END;
GO

CREATE OR ALTER PROCEDURE ChangeQuoteStatusSP
    @QuoteId INTEGER
AS
BEGIN
    DECLARE @NewStatus INTEGER;

    -- Verificar el estado actual del registro
    SELECT @NewStatus = 
        CASE 
            WHEN Status = 6 THEN 7  
            WHEN Status = 7 THEN 6  
        END
    FROM Quote
    WHERE Id = @QuoteId;

    -- Actualizar el estado
    UPDATE Quote
    SET Status = @NewStatus
    WHERE Id = @QuoteId;
END;
GO

CREATE OR ALTER PROCEDURE AddDocUrlToQuote
    @QuoteId INTEGER,
    @DocUrl VARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el campo DocUrl para el registro específico
    UPDATE Quote
    SET DocUrl = @DocUrl
    WHERE Id = @QuoteId;
END;
GO

CREATE OR ALTER PROCEDURE DeleteDocUrlFromQuote
    @QuoteId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si existe un registro con el Id proporcionado
    IF EXISTS (SELECT 1 FROM Quote WHERE Id = @QuoteId)
    BEGIN
        -- Eliminar el campo DocUrl del registro que coincida con el Id proporcionado
        UPDATE Quote
        SET DocUrl = NULL
        WHERE Id = @QuoteId;
    END
END;
GO

/*
CREATE OR ALTER PROCEDURE GetQuoteInformationSP
AS
BEGIN
    SELECT 
        u.Id AS UserId,
        u.Identification AS UserIdentification,
        u.Name AS UserName,
        u.FirstLastName AS UserFirstLastName,
        u.SecondLastName AS UserSecondLastName,
        u.PhoneNumber AS UserPhoneNumber,
        u.Email AS UserEmail,
        q.Id AS QuoteId,
        q.CreationDate AS QuoteCreationDate,
        qStatus.Name AS QuoteStatus,
        q.DocUrl AS QuoteDocumentUrl,
        qd.WalkInClosetQuantity AS WalkInClosetQuantity,
        qd.SecondaryRooms AS SecondaryRooms,
        qd.SecondaryBathrooms AS SecondaryBathrooms,
        qd.Details AS QuoteDetails,
        STUFF((SELECT ', ' + qr.RoomName
        FROM QuoteDetailsPerRoom qdr
        INNER JOIN QuoteRooms qr ON qdr.QuoteRoomId = qr.Id
        WHERE qdr.QuoteDetailsId = qd.Id
        FOR XML PATH('')), 1, 2, '') AS Rooms
    FROM 
        Quote q
    INNER JOIN 
        Users u ON q.ClientId = u.Id
    INNER JOIN 
        QuoteDetails qd ON q.QuoteDetails = qd.Id
    INNER JOIN 
        Status qStatus ON q.Status = qStatus.Id;
END;

CREATE OR ALTER PROCEDURE ChangeQuoteStatusSP
    @QuoteId INTEGER
AS
BEGIN
    DECLARE @NewStatus INTEGER;

    -- Verificar el estado actual del registro
    SELECT @NewStatus = 
        CASE 
            WHEN Status = 6 THEN 1003  -- Si el estado actual es 6, actualizar a 1003
            WHEN Status = 1003 THEN 6  -- Si el estado actual es 1003, actualizar a 6
        END
    FROM Quote
    WHERE Id = @QuoteId;

    -- Actualizar el estado
    UPDATE Quote
    SET Status = @NewStatus
    WHERE Id = @QuoteId;
END;

-- Update
CREATE or ALTER PROCEDURE UpdateQuoteSP
    @Id INTEGER,
    @ClientId INTEGER,
    @QuoteDetailsId INTEGER,
    @CreationDate DATETIME,
    @Status INTEGER
AS
BEGIN
    UPDATE Quote
    SET ClientId = @ClientId,
        QuoteDetails = @QuoteDetailsId,
        CreationDate = @CreationDate,
        Status = @Status
    WHERE Id = @Id;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteQuoteSP
    @Id INTEGER
AS
BEGIN
    DELETE FROM Quote
    WHERE Id = @Id;
END;
GO*/

GO

-- Procedimientos para la tabla Contact
--Create
CREATE OR ALTER PROCEDURE InsertContactSP
(
    @UserId INTEGER,
    @Message VARCHAR(512)
)
AS
BEGIN
    DECLARE @Status INTEGER;
    SET @Status = 12;

    INSERT INTO Contact (UserId, Message, Status)
    VALUES (@UserId, @Message, @Status);
END;
GO

--Read
CREATE OR ALTER PROCEDURE GetContactsSP
AS
BEGIN
    SELECT c.Id, u.Identification AS UserIdentification, 
           u.Name + ' ' + u.FirstLastName + ' ' + u.SecondLastName AS UserName,
           u.Email AS UserEmail, c.Message, c.Status, s.Name AS StatusName
    FROM Contact c
    INNER JOIN Users u ON c.UserId = u.Id
    INNER JOIN Status s ON c.Status = s.Id;
END;
GO
	
CREATE OR ALTER PROCEDURE ChangeContactStatusSP
    @ContactId INTEGER
AS
BEGIN
    DECLARE @NewStatus INTEGER;

    SELECT @NewStatus = 
        CASE 
            WHEN Status = 12 THEN 13 
            WHEN Status = 13 THEN 12 
        END
    FROM Contact
    WHERE Id = @ContactId;

    UPDATE Contact
    SET Status = @NewStatus
    WHERE Id = @ContactId;
END;
GO

/* ------------------------------------------------
        TRIGGERS PARA LA AUDITORIA    
------------------------------------------------*/

/* --------------------------------
        TRIGGERS TABLA EMPLOYEE       
--------------------------------*/
 -- INSERT 
CREATE OR ALTER TRIGGER Employee_Insert_Trigger
ON Employee
AFTER INSERT
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Insertar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Empleado';

    SELECT @IdEmpleado = UserId, @IdUsuario = UserId
    FROM inserted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
           @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Insertados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE insert_cursor CURSOR FOR 
    SELECT Id FROM inserted;
    
    OPEN insert_cursor;
    
    FETCH NEXT FROM insert_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosInsertados NVARCHAR(MAX);
        DECLARE @Rol VARCHAR(50);
        DECLARE @Estado VARCHAR(50);
        DECLARE @ImageUrl VARCHAR(100);
        DECLARE @RetirementDate DATETIME;
        
        SELECT @Rol = R.RoleName,
               @Estado = S.Name,
               @ImageUrl = ISNULL(ImageProfile, ''),
               @RetirementDate = RetirementDate
        FROM inserted I
        INNER JOIN Role R ON I.Role = R.Id
        INNER JOIN Status S ON I.Status = S.Id
        WHERE I.Id = @ID;
        
        SET @DatosInsertados = 
            'Rol: ' + @Rol + CHAR(13) + CHAR(10) +
            'Estado: ' + @Estado + CHAR(13) + CHAR(10) +
            'Imagen de Perfil: ' + @ImageUrl + CHAR(13) + CHAR(10) +
            'Fecha de Retiro: ' + ISNULL(CONVERT(VARCHAR, @RetirementDate), 'No especificada') + CHAR(13) + CHAR(10);
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosInsertados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM insert_cursor INTO @ID;
    END
    
    CLOSE insert_cursor;
    DEALLOCATE insert_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- UPDATE 
CREATE OR ALTER TRIGGER Employee_Update_Trigger
ON Employee
AFTER UPDATE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Actualizar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Empleado';

    SELECT @IdEmpleado = UserId, @IdUsuario = UserId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
           @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Actualizados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE update_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN update_cursor;
    
    FETCH NEXT FROM update_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosActualizados NVARCHAR(MAX);
        DECLARE @RolAntiguo VARCHAR(50);
        DECLARE @EstadoAntiguo VARCHAR(50);
        DECLARE @RolNuevo VARCHAR(50);
        DECLARE @EstadoNuevo VARCHAR(50);
        DECLARE @ImageUrl VARCHAR(100);
        DECLARE @RetirementDateAntiguo DATETIME;
        DECLARE @RetirementDateNuevo DATETIME;
        
        SELECT @RolAntiguo = R.RoleName,
               @EstadoAntiguo = S.Name,
               @RetirementDateAntiguo = D.RetirementDate
        FROM deleted D
        INNER JOIN Role R ON D.Role = R.Id
        INNER JOIN Status S ON D.Status = S.Id
        WHERE D.Id = @ID;
        
        SELECT @RolNuevo = R.RoleName,
               @EstadoNuevo = S.Name,
               @ImageUrl = ISNULL(I.ImageProfile, ''),
               @RetirementDateNuevo = I.RetirementDate
        FROM inserted I
        INNER JOIN Role R ON I.Role = R.Id
        INNER JOIN Status S ON I.Status = S.Id
        WHERE I.Id = @ID;

        SET @DatosActualizados = 
            'Rol Antiguo: ' + @RolAntiguo + CHAR(13) + CHAR(10) +
            'Estado Antiguo: ' + @EstadoAntiguo + CHAR(13) + CHAR(10) +
            'Rol Nuevo: ' + @RolNuevo + CHAR(13) + CHAR(10) +
            'Estado Nuevo: ' + @EstadoNuevo + CHAR(13) + CHAR(10) +
            'Imagen de Perfil: ' + @ImageUrl + CHAR(13) + CHAR(10) +
            'Fecha de Retiro Antiguo: ' + ISNULL(CONVERT(VARCHAR, @RetirementDateAntiguo), 'No especificada') + CHAR(13) + CHAR(10) +
            'Fecha de Retiro Nuevo: ' + ISNULL(CONVERT(VARCHAR, @RetirementDateNuevo), 'No especificada') + CHAR(13) + CHAR(10);
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosActualizados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM update_cursor INTO @ID;
    END
    
    CLOSE update_cursor;
    DEALLOCATE update_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- DELETE 
CREATE OR ALTER TRIGGER Employee_Delete_Trigger
ON Employee
AFTER DELETE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Eliminar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Empleado';
    
    SELECT @IdEmpleado = UserId, @IdUsuario = UserId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
           @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO


/* --------------------------------
            TRIGGERS TABLA APPOIMENT       
--------------------------------*/
    -- Trigger Insert
/*CREATE OR ALTER TRIGGER SP_Appointment_Insert_Trigger
ON Appointment
AFTER INSERT
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Insertar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Citas';
    
    SELECT @IdEmpleado = EmployeeId, @IdUsuario = UserId
    FROM inserted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
            @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Insertados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE insert_cursor CURSOR FOR 
    SELECT Id FROM inserted;
    
    OPEN insert_cursor;
    
    FETCH NEXT FROM insert_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosInsertados NVARCHAR(MAX);
        
        SELECT @DatosInsertados =            
            'Fecha de cita: ' + 
                CASE 
                    WHEN i.AppointmentDate IS NOT NULL THEN CONVERT(NVARCHAR, i.AppointmentDate, 121)
                    ELSE 'No definida'
                END + CHAR(13) + CHAR(10) +
            'Estado: ' + (SELECT Name FROM Status WHERE Id = i.Status) + CHAR(13) + CHAR(10) +
            'Usuario: ' + (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = i.UserId) + CHAR(13) + CHAR(10) +
            'Empleado: ' + 
                CASE 
                    WHEN i.EmployeeId IS NOT NULL THEN (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = i.EmployeeId)
                    ELSE 'No asignado'
                END + CHAR(13) + CHAR(10) +
            'Propiedad: ' + (SELECT Title FROM Property WHERE Id = i.PropertyId) + CHAR(13) + CHAR(10)
        FROM inserted i
        WHERE Id = @ID;
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosInsertados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM insert_cursor INTO @ID;
    END
    
    CLOSE insert_cursor;
    DEALLOCATE insert_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- Trigger Update
CREATE OR ALTER TRIGGER SP_Appointment_Update_Trigger
ON Appointment
AFTER UPDATE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Actualizar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Citas';
    
    SELECT @IdEmpleado = EmployeeId, @IdUsuario = UserId
    FROM inserted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
            @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Actualizados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE update_cursor CURSOR FOR 
    SELECT Id FROM inserted;
    
    OPEN update_cursor;
    
    FETCH NEXT FROM update_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosActualizados NVARCHAR(MAX);
        
        SELECT @DatosActualizados =        
            'Fecha de cita (Anterior): ' + CONVERT(NVARCHAR, d.AppointmentDate, 121) + CHAR(13) + CHAR(10) +
            'Fecha de cita (Actual): ' + CONVERT(NVARCHAR, i.AppointmentDate, 121) + CHAR(13) + CHAR(10) +
            'Estado (Anterior): ' + (SELECT Name FROM Status WHERE Id = d.Status) + CHAR(13) + CHAR(10) +
            'Estado (Actual): ' + (SELECT Name FROM Status WHERE Id = i.Status) + CHAR(13) + CHAR(10) +
            'Usuario (Anterior): ' + (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = d.UserId) + CHAR(13) + CHAR(10) +
            'Usuario (Actual): ' + (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = i.UserId) + CHAR(13) + CHAR(10) +
            'Empleado (Anterior): ' + 
                CASE 
                    WHEN d.EmployeeId IS NOT NULL THEN (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = d.EmployeeId)
                    ELSE 'No asignado'
                END + CHAR(13) + CHAR(10) +
            'Empleado (Actual): ' + 
                CASE 
                    WHEN i.EmployeeId IS NOT NULL THEN (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = i.EmployeeId)
                    ELSE 'No asignado'
                END + CHAR(13) + CHAR(10) +
            'Propiedad (Anterior): ' + (SELECT Title FROM Property WHERE Id = d.PropertyId) + CHAR(13) + CHAR(10) +
            'Propiedad (Actual): ' + (SELECT Title FROM Property WHERE Id = i.PropertyId) + CHAR(13) + CHAR(10)
        FROM deleted d
        JOIN inserted i ON d.Id = i.Id;

        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosActualizados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM update_cursor INTO @ID;
    END
    
    CLOSE update_cursor;
    DEALLOCATE update_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- Trigger Delete
CREATE OR ALTER TRIGGER SP_Appointment_Delete_Trigger
ON Appointment
AFTER DELETE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Eliminar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Citas';
    
    SELECT @IdEmpleado = EmployeeId, @IdUsuario = UserId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
            @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Eliminados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE delete_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN delete_cursor;
    
    FETCH NEXT FROM delete_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosEliminados NVARCHAR(MAX);
        
        SELECT @DatosEliminados =         
            'Fecha de cita: ' + 
                CASE 
                    WHEN d.AppointmentDate IS NOT NULL THEN CONVERT(NVARCHAR, d.AppointmentDate, 121)
                    ELSE 'No definida'
                END + CHAR(13) + CHAR(10) +
            'Estado: ' + (SELECT Name FROM Status WHERE Id = d.Status) + CHAR(13) + CHAR(10) +
            'Usuario: ' + (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = d.UserId) + CHAR(13) + CHAR(10) +
            'Empleado: ' + 
                CASE 
                    WHEN d.EmployeeId IS NOT NULL THEN (SELECT Name + ' ' + FirstLastName + ' ' + SecondLastName FROM Users WHERE Id = d.EmployeeId)
                    ELSE 'No asignado'
                END + CHAR(13) + CHAR(10) +
            'Propiedad: ' + (SELECT Title FROM Property WHERE Id = d.PropertyId) + CHAR(13) + CHAR(10)
        FROM deleted d
        WHERE Id = @ID;
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosEliminados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM delete_cursor INTO @ID;
    END
    
    CLOSE delete_cursor;
    DEALLOCATE delete_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO*/


/* --------------------------------
        TRIGGERS TABLA NEWS       
--------------------------------*/

-- Trigger Insert
CREATE OR ALTER TRIGGER Employee_Insert_Trigger
ON Employee
AFTER INSERT
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Insertar';
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Empleado';

    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);

    DECLARE @DetallesEmpleado NVARCHAR(255);

    -- Inicializar una tabla temporal para almacenar los datos de inserción
    DECLARE @InsertedData TABLE (
        UserId INT,
        RoleName VARCHAR(50),
        StatusName VARCHAR(50),
        ImageProfile VARCHAR(100)
    );

    -- Insertar los datos de inserción en la tabla temporal
    INSERT INTO @InsertedData (UserId, RoleName, StatusName, ImageProfile)
    SELECT I.UserId, R.RoleName, S.Name, I.ImageProfile
    FROM inserted I
    INNER JOIN Role R ON I.Role = R.Id
    INNER JOIN Status S ON I.Status = S.Id;

    -- Recorrer los datos de la tabla temporal
    DECLARE @UserId INT;

    DECLARE insert_cursor CURSOR FOR 
    SELECT UserId FROM @InsertedData;

    OPEN insert_cursor;
    
    FETCH NEXT FROM insert_cursor INTO @UserId;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
                @Identificacion = U.Identification
        FROM Users U
        WHERE U.Id = @UserId;

        SET @MensajeAuditoria = 'Acción: ' + @Accion 
                                + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                                + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                                + ', Sección Modificada: ' + @SeccionModificada
                                + ', Datos Insertados:' + CHAR(13) + CHAR(10);

        -- Insertar en la tabla AuditLog para cada fila insertada
        INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
        VALUES (@UserId, @SeccionModificada, @MensajeAuditoria, GETDATE());

        FETCH NEXT FROM insert_cursor INTO @UserId;
    END

    CLOSE insert_cursor;
    DEALLOCATE insert_cursor;
END;

GO

-- Trigger para UPDATE
CREATE OR ALTER TRIGGER SP_News_Update_Trigger
ON News
AFTER UPDATE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Actualizar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Noticias';
    
    SELECT @IdEmpleado = EmployeeId, @IdUsuario = EmployeeId
    FROM deleted; 

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
            @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Actualizados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE update_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN update_cursor;
    
    FETCH NEXT FROM update_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosActualizados NVARCHAR(MAX);
        
		SELECT @DatosActualizados =          
			'Fecha de creacion (Anterior): ' + CONVERT(NVARCHAR(MAX), d.CreationDate, 121) + CHAR(13) + CHAR(10) +
			'Fecha de creacion (Actual): ' + CONVERT(NVARCHAR(MAX), i.CreationDate, 121) + CHAR(13) + CHAR(10) +
			'Titulo (Anterior): ' + CAST(d.Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
			'Titulo (Actual): ' + CAST(i.Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
			'Descripcion (Anterior): ' + CAST(d.Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
			'Descripcion (Actual): ' + CAST(i.Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
			'Url (Anterior): ' + ISNULL(CAST(d.Url AS NVARCHAR(MAX)), '') + CHAR(13) + CHAR(10) +
			'Url (Actual): ' + ISNULL(CAST(i.Url AS NVARCHAR(MAX)), '') + CHAR(13) + CHAR(10)
		FROM deleted d
JOIN inserted i ON d.Id = i.Id;

        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosActualizados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM update_cursor INTO @ID;
    END
    
    CLOSE update_cursor;
    DEALLOCATE update_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- Trigger para DELETE
CREATE OR ALTER TRIGGER SP_News_Delete_Trigger
ON News
AFTER DELETE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Eliminar';
    DECLARE @IdEmpleado INT;
    DECLARE @IdUsuario INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @Identificacion VARCHAR(20);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Noticias';
    
    SELECT @IdEmpleado = EmployeeId, @IdUsuario = EmployeeId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT  @NombreEmpleado = COALESCE(U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName, 'Desconocido'),
            @Identificacion = U.Identification
    FROM Users U
    WHERE U.Id = @IdUsuario;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Identificación: ' + ISNULL(@Identificacion, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Eliminados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE delete_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN delete_cursor;
    
    FETCH NEXT FROM delete_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosEliminados NVARCHAR(MAX);
        
        SELECT @DatosEliminados =           
            'Fecha de creacion: ' + CONVERT(NVARCHAR(MAX), CreationDate, 121) + CHAR(13) + CHAR(10) +
            'Titulo: ' + CAST(Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Descripcion: ' + CAST(Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Url: ' + ISNULL(CAST(Url AS NVARCHAR(MAX)), '') + CHAR(13) + CHAR(10)
        FROM deleted
        WHERE Id = @ID;
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosEliminados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM delete_cursor INTO @ID;
    END
    
    CLOSE delete_cursor;
    DEALLOCATE delete_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

/* --------------------------------
        TRIGGERS TABLA PROPERTY       
--------------------------------*/

-- INSERT 
CREATE OR ALTER TRIGGER Property_Insert_Trigger
ON Property
AFTER INSERT
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Insertar';
    DECLARE @IdEmpleado INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Propiedad';
    
    SELECT @IdEmpleado = e.Id
    FROM inserted i
    INNER JOIN Employee e ON i.EmployeeId = e.Id;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(u.Name + ' ' + u.FirstLastName + ' ' + u.SecondLastName, 'Desconocido')
    FROM Users u
    WHERE u.Id = @IdEmpleado;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Insertados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE insert_cursor CURSOR FOR 
    SELECT Id FROM inserted;
    
    OPEN insert_cursor;
    
    FETCH NEXT FROM insert_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosInsertados NVARCHAR(MAX);
        
        SELECT @DatosInsertados = 
            'Título: ' + CAST(i.Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Precio: ' + (SELECT Symbol FROM Currency WHERE Id = i.Currency) + CAST(i.Price AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área de Terreno: ' + CAST(i.AreaLand AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área Construida: ' + CAST(i.AreaBuild AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Descripción: ' + CAST(i.Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Fecha de Venta: ' + ISNULL(CONVERT(NVARCHAR, i.SoldDate, 121), 'No vendida') + CHAR(13) + CHAR(10) +
            'Tipo de Propiedad: ' + (SELECT Type FROM PropertyType WHERE Id = i.PropertyType) + CHAR(13) + CHAR(10) +
            'Tipo de Transacción: ' + (SELECT Type FROM TransactionType WHERE Id = i.Transactiontype) + CHAR(13) + CHAR(10) +
            'Estado de la Propiedad: ' + (SELECT Name FROM Status WHERE Id = i.PropertyStatus) + CHAR(13) + CHAR(10)
        FROM inserted i
        WHERE Id = @ID;
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosInsertados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM insert_cursor INTO @ID;
    END
    
    CLOSE insert_cursor;
    DEALLOCATE insert_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- UPDATE 
CREATE OR ALTER TRIGGER Property_Update_Trigger
ON Property
AFTER UPDATE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Actualizar';
    DECLARE @IdEmpleado INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Propiedad';

    SELECT @IdEmpleado = EmployeeId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(u.Name + ' ' + u.FirstLastName + ' ' + u.SecondLastName, 'Desconocido')
    FROM Users u
    WHERE u.Id = @IdEmpleado;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Actualizados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE update_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN update_cursor;
    
    FETCH NEXT FROM update_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosActualizados NVARCHAR(MAX);
        
        SELECT @DatosActualizados = 
            'Título: ' + CAST(i.Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Precio: ' + (SELECT Symbol FROM Currency WHERE Id = i.Currency) + CAST(i.Price AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área de Terreno: ' + CAST(i.AreaLand AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área Construida: ' + CAST(i.AreaBuild AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Descripción: ' + CAST(i.Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Fecha de Venta: ' + ISNULL(CONVERT(NVARCHAR, i.SoldDate, 121), 'No vendida') + CHAR(13) + CHAR(10) +
            'Tipo de Propiedad: ' + (SELECT Type FROM PropertyType WHERE Id = i.PropertyType) + CHAR(13) + CHAR(10) +
            'Tipo de Transacción: ' + (SELECT Type FROM TransactionType WHERE Id = i.Transactiontype) + CHAR(13) + CHAR(10) +
            'Estado de la Propiedad: ' + (SELECT Name FROM Status WHERE Id = i.PropertyStatus) + CHAR(13) + CHAR(10)
        FROM deleted d
        JOIN inserted i ON d.Id = i.Id;

        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosActualizados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM update_cursor INTO @ID;
    END
    
    CLOSE update_cursor;
    DEALLOCATE update_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO

-- DELETE 
CREATE OR ALTER TRIGGER Property_Delete_Trigger
ON Property
AFTER DELETE
AS
BEGIN
    DECLARE @Accion VARCHAR(20) = 'Eliminar';
    DECLARE @IdEmpleado INT;
    DECLARE @NombreEmpleado VARCHAR(100);
    DECLARE @MensajeAuditoria NVARCHAR(MAX) = '';
    DECLARE @SeccionModificada NVARCHAR(100) = 'Propiedad';
    
    SELECT @IdEmpleado = EmployeeId
    FROM deleted;

    DECLARE @DetallesEmpleado NVARCHAR(255);
    SELECT @NombreEmpleado = COALESCE(u.Name + ' ' + u.FirstLastName + ' ' + u.SecondLastName, 'Desconocido')
    FROM Users u
    WHERE u.Id = @IdEmpleado;

    SET @MensajeAuditoria = 'Acción: ' + @Accion 
                            + ', Empleado: ' + ISNULL(@NombreEmpleado, 'Desconocido')
                            + ', Sección Modificada: ' + @SeccionModificada
                            + ', Datos Eliminados:' + CHAR(13) + CHAR(10);

    DECLARE @ID INT;
    DECLARE delete_cursor CURSOR FOR 
    SELECT Id FROM deleted;
    
    OPEN delete_cursor;
    
    FETCH NEXT FROM delete_cursor INTO @ID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @DatosEliminados NVARCHAR(MAX);
        
        SELECT @DatosEliminados = 
            'Título: ' + CAST(Title AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Precio: ' + (SELECT Symbol FROM Currency WHERE Id = deleted.Currency) + CAST(Price AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área de Terreno: ' + CAST(AreaLand AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Área Construida: ' + CAST(AreaBuild AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Descripción: ' + CAST(Description AS NVARCHAR(MAX)) + CHAR(13) + CHAR(10) +
            'Fecha de Venta: ' + ISNULL(CONVERT(NVARCHAR, SoldDate, 121), 'No vendida') + CHAR(13) + CHAR(10) +
            'Tipo de Propiedad: ' + (SELECT Type FROM PropertyType WHERE Id = PropertyType) + CHAR(13) + CHAR(10) +
            'Tipo de Transacción: ' + (SELECT Type FROM TransactionType WHERE Id = Transactiontype) + CHAR(13) + CHAR(10) +
            'Estado de la Propiedad: ' + (SELECT Name FROM Status WHERE Id = PropertyStatus) + CHAR(13) + CHAR(10)
        FROM deleted;
        
        SET @MensajeAuditoria = @MensajeAuditoria + @DatosEliminados + CHAR(13) + CHAR(10);
        
        FETCH NEXT FROM delete_cursor INTO @ID;
    END
    
    CLOSE delete_cursor;
    DEALLOCATE delete_cursor;

    INSERT INTO AuditLog (EmployeeId, TableName, CurrentAudit, CreationDateAudit)
    VALUES (@IdEmpleado, @SeccionModificada, @MensajeAuditoria, GETDATE());
END;
GO
--Appointments SP
-- Insert
CREATE OR ALTER PROCEDURE InsertAppointmentSP
       @AppointmentDate DATETIME,
    @UserId INT,
    @PropertyId INT
AS
BEGIN    
    -- Verificar si el usuario ya tiene una cita para la misma propiedad en el mismo día en estado 8 o 9
    IF EXISTS (
        SELECT * 
        FROM Appointment 
        WHERE 
            CONVERT(DATE, AppointmentDate) = CONVERT(DATE, @AppointmentDate) -- Compara solo la fecha
            AND UserId = @UserId
            AND PropertyId = @PropertyId 
            AND (Status = 8 OR Status = 9)
    )
    BEGIN
        -- Mostrar mensaje de error o tomar la acción necesaria
        RETURN; -- Salir del procedimiento sin realizar la inserción
    END

    -- Verificar si ya existe una cita en la misma fecha y hora con estado 8 o 9
    IF EXISTS (
        SELECT * 
        FROM Appointment 
        WHERE 
            AppointmentDate = @AppointmentDate -- Compara la fecha y hora exactas
            AND (Status = 8 OR Status = 9)
    )
    BEGIN
        -- Mostrar mensaje de error o tomar la acción necesaria
        RETURN; -- Salir del procedimiento sin realizar la inserción
    END

    -- Si no se cumplen las restricciones, insertar la nueva cita en estado 8
    INSERT INTO Appointment (AppointmentDate, Status, UserId, EmployeeId, PropertyId, CreationDate)
    VALUES (@AppointmentDate, 8, @UserId, null, @PropertyId, GETDATE());
END
GO
--Select
CREATE OR ALTER PROCEDURE GetAllAppoinmentsSP
AS
BEGIN
    SELECT A.Id, A.AppointmentDate, A.CreationDate, A.Status, S.Name AS StatusName,U.ID as UserId, U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName AS UserName,
	CASE
		WHEN len(U.identification) = 10 THEN
			substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4)
		WHEN len(U.identification) > 10 THEN
			substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4) + '-' + substring(U.identification, 11, len(U.identification) - 10)
		ELSE U.identification
	END
	AS IdentificationUser,U.Email,CASE WHEN len(U.phonenumber) >= 8 THEN substring(U.phonenumber, 1, 4) + '-' + substring(U.phonenumber, 5, 4)  ELSE U.phonenumber
	END
	AS phonenumber,E.Id as EmployeeId, E.Name + ' ' + E.FirstLastName + ' ' + E.SecondLastName AS EmployeeName,CASE
		WHEN len(U.identification) = 10 THEN
			substring(E.identification, 1, 2) + '-' + substring(E.identification, 3, 4) + '-' + substring(E.identification, 7, 4)
		WHEN len(U.identification) > 10 THEN
			substring(E.identification, 1, 2) + '-' + substring(E.identification, 3, 4) + '-' + substring(E.identification, 7, 4) + '-' + substring(E.identification, 11, len(E.identification) - 10)
			ELSE U.identification
		END
		AS IdentificationEmployee, P.Title as PropertyName
			FROM Appointment A
			INNER JOIN Status S ON A.Status = S.Id
			INNER JOIN Users U ON A.UserId = U.Id
			LEFT JOIN Users E ON A.EmployeeId = E.Id
			INNER JOIN Property P ON A.PropertyId = P.Id;
	END
GO
--ChangeStatus
CREATE OR ALTER PROCEDURE ChangeAppointmentStatus
@Id INT
AS
BEGIN
    DECLARE @StatusActual INT;

    -- Obtener el estado actual de la cita
    SELECT  @StatusActual = Status
    FROM Appointment
    WHERE Id = @Id;

    -- Verificar si se encontró una cita con el Id dado
    IF @StatusActual IS NOT NULL
    BEGIN
        -- Actualizar el estado de la cita según la lógica proporcionada
        UPDATE Appointment
        SET Status = CASE WHEN @StatusActual = 8 THEN 10 ELSE 8 END
		WHERE Id = @Id;
    END
END;
GO
--Listado de estados de las citas
CREATE OR ALTER PROCEDURE GetAppointmentStatusesSP
AS
BEGIN
	SELECT Id, Name, Description, RelatedTable, IsActive from STATUS 
	WHERE RelatedTable = 'Citas'
	AND IsActive = 1;
END
GO
--Obtener una cita por Id
CREATE OR ALTER PROCEDURE GetAppointmentSP
@Id INT
AS
BEGIN
    SELECT A.Id, A.AppointmentDate,A.Status, U.ID as UserId, U.Name + ' ' + U.FirstLastName + ' ' + U.SecondLastName AS UserName,
    CASE
WHEN len(U.identification) = 10 THEN
    substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4)
WHEN len(U.identification) > 10 THEN
    substring(U.identification, 1, 2) + '-' + substring(U.identification, 3, 4) + '-' + substring(U.identification, 7, 4) + '-' + substring(U.identification, 11, len(U.identification) - 10)
    ELSE U.identification 
END
AS IdentificationUser,U.Email,CASE WHEN len(U.phonenumber) >= 8 THEN substring(U.phonenumber, 1, 4) + '-' + substring(U.phonenumber, 5, 4)  ELSE U.phonenumber
END
AS phonenumber,E.Id as EmployeeId, P.Title as PropertyName
    FROM Appointment A
    INNER JOIN Status S ON A.Status = S.Id
    INNER JOIN Users U ON A.UserId = U.Id
    LEFT JOIN Users E ON A.EmployeeId = E.Id
    INNER JOIN Property P ON A.PropertyId = P.Id
    WHERE A.Id = @Id;
END
GO

--Actualizar cita
CREATE OR ALTER PROCEDURE UpdateAppointmentSP
@Id INT,
@Status INT,
@EmployeeId INT,
@AppointmentDate DATETIME
AS
BEGIN
    -- Actualizar la cita
    UPDATE Appointment
    SET Status = @Status,
        EmployeeId = @EmployeeId,
		AppointmentDate =@AppointmentDate
    WHERE Id = @Id;
END
GO

-- SP para obtener el ID de Province
CREATE OR ALTER PROCEDURE GetProvinceByIdSP
    @ProvinceId INTEGER
AS
BEGIN
    SELECT Id, Name, IsActive
    FROM Province
    WHERE Id = @ProvinceId;
END;
GO

-- SP para obtener el ID de Canton
CREATE OR ALTER PROCEDURE GetCantonByIdSP
    @CantonId INTEGER
AS
BEGIN
    SELECT Id, Name, Province, IsActive
    FROM Canton
    WHERE Id = @CantonId;
END;
GO

-- SP para obtener el ID de District
CREATE OR ALTER PROCEDURE GetDistrictByIdSP
    @DistrictId INTEGER
AS
BEGIN
    SELECT Id, Name, Canton, IsActive
    FROM District
    WHERE Id = @DistrictId;
END;
GO

-- SP para cambiar el estado de Province
CREATE OR ALTER PROCEDURE ChangeProvinceStatusSP
    @ProvinceId INT
AS
BEGIN
    UPDATE Province
    SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
    WHERE Id = @ProvinceId;
END;
GO

-- SP para cambiar el estado de Canton
CREATE OR ALTER PROCEDURE ChangeCantonStatusSP
    @CantonId INT
AS
BEGIN
    UPDATE Canton
    SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
    WHERE Id = @CantonId;
END;
GO

-- SP para cambiar el estado de District
CREATE OR ALTER PROCEDURE ChangeDistrictStatusSP
    @DistrictId INT
AS
BEGIN
    UPDATE District
    SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
    WHERE Id = @DistrictId;
END;
GO

-- SP para obtener el ID de DocumentType
CREATE OR ALTER PROCEDURE GetDocumentTypeByIdSP
    @DocumentTypeId INTEGER
AS
BEGIN
    SELECT Id, Document, IsActive
    FROM DocumentType
    WHERE Id = @DocumentTypeId;
END;
GO

-- SP para obtener el ID de Status
CREATE OR ALTER PROCEDURE GetStatusByIdSP
    @StatusId INTEGER
AS
BEGIN
    SELECT Id, Name, Description, RelatedTable, IsActive
    FROM Status
    WHERE Id = @StatusId;
END;
GO

-- SP para obtener el ID de Role
CREATE OR ALTER PROCEDURE GetRoleByIdSP
    @RoleId INTEGER
AS
BEGIN
    SELECT Id, RoleName, Description, IsActive
    FROM Role
    WHERE Id = @RoleId;
END;
GO

-- SP para obtener el ID de PropertyType
CREATE OR ALTER PROCEDURE GetPropertyTypeByIdSP
    @PropertyTypeId INTEGER
AS
BEGIN
    SELECT Id, Type, Description, IsActive
    FROM PropertyType
    WHERE Id = @PropertyTypeId;
END;
GO

-- SP para obtener el ID de TransactionType
CREATE OR ALTER PROCEDURE GetTransactionTypeByIdSP
    @TransactionTypeId INTEGER
AS
BEGIN
    SELECT Id, Type, Description, IsActive
    FROM TransactionType
    WHERE Id = @TransactionTypeId;
END;
GO


-- SP para cambiar el estado de Province

CREATE or ALTER PROCEDURE ChangeProvinceStatusSP
@ProvinceId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM Province
    WHERE Id = @ProvinceId;

    IF @CurrentStatus IS NOT NULL
    BEGIN
 
        UPDATE Province
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @ProvinceId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
      
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de Canton
CREATE or ALTER PROCEDURE ChangeCantonStatusSP
@CantonId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

   
    SELECT @CurrentStatus = IsActive
    FROM Canton
    WHERE Id = @CantonId;

    
    IF @CurrentStatus IS NOT NULL
    BEGIN
       
        UPDATE Canton
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @CantonId;
        
       
        SELECT 1;
    END
    ELSE
    BEGIN
       
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de District
CREATE or ALTER PROCEDURE ChangeDistrictStatusSP
@DistrictId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM District
    WHERE Id = @DistrictId;

    IF @CurrentStatus IS NOT NULL
    BEGIN
        UPDATE District
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @DistrictId;
   
        SELECT 1;
    END
    ELSE
    BEGIN
      
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de DocumentType
CREATE or ALTER PROCEDURE ChangeDocumentTypeStatusSP
@DocumentTypeId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM DocumentType
    WHERE Id = @DocumentTypeId;


    IF @CurrentStatus IS NOT NULL
    BEGIN
        UPDATE DocumentType
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @DocumentTypeId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de PropertyType
CREATE or ALTER PROCEDURE ChangePropertyTypeStatusSP
@PropertyTypeId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM PropertyType
    WHERE Id = @PropertyTypeId;

    IF @CurrentStatus IS NOT NULL
    BEGIN

        UPDATE PropertyType
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @PropertyTypeId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
     
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de TransactionType
CREATE or ALTER PROCEDURE ChangeTransactionTypeStatusSP
@TransactionTypeId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM TransactionType
    WHERE Id = @TransactionTypeId;

    IF @CurrentStatus IS NOT NULL
    BEGIN
        UPDATE TransactionType
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @TransactionTypeId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT -1;
    END
END;
GO

-- SP para cambiar el estado de Status

CREATE or ALTER PROCEDURE ChangeStatusStatusSP
@StatusId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM Status
    WHERE Id = @StatusId;

    IF @CurrentStatus IS NOT NULL
    BEGIN
        UPDATE Status
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @StatusId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT -1;
    END
END;
GO


-- SP para cambiar el estado de Role
CREATE or ALTER PROCEDURE ChangeRoleStatusSP
@RoleId INT
AS
BEGIN
    SET NOCOUNT ON; 
    DECLARE @CurrentStatus INT;

    SELECT @CurrentStatus = IsActive
    FROM Role
    WHERE Id = @RoleId;

    IF @CurrentStatus IS NOT NULL
    BEGIN
        UPDATE Role
        SET IsActive = CASE WHEN @CurrentStatus = 1 THEN 0 ELSE 1 END
        WHERE Id = @RoleId;
        
        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT -1;
    END
END;
GO


-- SP para actualizar una provincia
CREATE OR ALTER PROCEDURE UpdateProvinceSP
    @ProvinceId INT,
    @Name VARCHAR(50),
    @IsActive BIT
AS
BEGIN
    UPDATE Province
    SET Name = @Name,
        IsActive = @IsActive
    WHERE Id = @ProvinceId;
    IF @IsActive = 0
    BEGIN
        UPDATE Canton
        SET IsActive = 0
        WHERE Province = @ProvinceId;
    END;
END;
GO


-- SP para actualizar un cantón
CREATE OR ALTER PROCEDURE UpdateCantonSP
    @CantonId INT,
    @Name VARCHAR(50),
    @ProvinceId INT,
    @IsActive BIT
AS
BEGIN
    UPDATE Canton
    SET Name = @Name,
        Province = @ProvinceId,
        IsActive = @IsActive
    WHERE Id = @CantonId;

    IF @IsActive = 0
    BEGIN
        UPDATE District
        SET IsActive = 0
        WHERE Canton = @CantonId;
    END;
END;
GO

-- SP para actualizar un distrito
CREATE OR ALTER PROCEDURE UpdateDistrictSP
    @DistrictId INT,
    @Name VARCHAR(50),
    @CantonId INT,
    @IsActive BIT
AS
BEGIN
    UPDATE District
    SET Name = @Name,
        Canton = @CantonId,
        IsActive = @IsActive
    WHERE Id = @DistrictId;
END;
GO

CREATE OR ALTER PROCEDURE GetEmployeeByIdentificationSP
    @Identification VARCHAR(200)
AS
BEGIN
    DECLARE @UserId BIGINT

    SELECT @UserId = U.Id
    FROM Users U
    JOIN Employee E ON U.Id = E.UserId
    WHERE Identification = @Identification 
        AND Status = 3

    IF (@UserId IS NOT NULL)
    BEGIN
			--Restorna el empleado
	SELECT	U.Identification,U.FirstLastName,U.SecondLastName, U.name,U.Email
	FROM Employee E join Users U on E.UserId = U.Id 
    join Role R on E.Role = R.Id join Status S on E.Status = S.Id 
    where e.UserId = @UserId;
    END
END
GO

-- Procedimientos para la tabla currency ------------
CREATE or ALTER PROCEDURE InsertCurrencySP
    @Name VARCHAR(64),
    @CurrencyCode VARCHAR(64),
    @Symbol NCHAR
AS
BEGIN
    INSERT INTO Currency (Name, CurrencyCode, Symbol)
    VALUES (@Name, @CurrencyCode, @Symbol);
END;
GO
-- Read
CREATE OR ALTER PROCEDURE GetAllCurrenciesSP
AS
BEGIN
    SELECT Id, Name, CurrencyCode, Symbol 
    FROM Currency
	ORDER BY Id
END;
GO

-- Obtener información por el Id
CREATE or ALTER PROCEDURE GetCurrencyByIdSP
    @CurrencyId INTEGER
AS
BEGIN
    SELECT Id, Name, CurrencyCode, Symbol
    FROM Currency
    WHERE Id = @CurrencyId;
END;
GO
-- Uptade
CREATE or ALTER PROCEDURE UpdateCurrencySP
    @CurrencyId INTEGER,
    @Name VARCHAR(64),
    @CurrencyCode VARCHAR(64),
    @Symbol NCHAR
AS
BEGIN
    UPDATE Currency
    SET 
        Name = @Name,
        CurrencyCode = @CurrencyCode,
        Symbol = @Symbol
    WHERE Id = @CurrencyId;
END;
GO
-- Delete
CREATE or ALTER PROCEDURE DeleteCurrencySP
    @CurrencyId INTEGER
AS
BEGIN
    DELETE FROM Currency
    WHERE Id = @CurrencyId;
END;
GO
