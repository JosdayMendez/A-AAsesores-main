USE [AAAsesores]
GO
SET IDENTITY_INSERT [dbo].[Province] ON 
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (2, N'Alajuela')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (3, N'Cartago')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (5, N'Guanacaste')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (4, N'Heredia')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (7, N'Limón')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (6, N'Puntarenas')
GO
INSERT [dbo].[Province] ([Id], [Name]) VALUES (1, N'San José')
GO
SET IDENTITY_INSERT [dbo].[Province] OFF
GO
SET IDENTITY_INSERT [dbo].[Canton] ON 
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (1, N'San José', 1)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (2, N'Escazú', 1)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (3, N'Alajuela', 2)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (4, N'Atenas', 2)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (5, N'Cartago', 3)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (6, N'Turrialba', 3)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (7, N'Heredia', 4)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (8, N'Barva', 4)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (9, N'Liberia', 5)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (10, N'Nicoya', 5)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (11, N'Puntarenas', 6)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (12, N'Quepos', 6)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (13, N'Limón', 7)
GO
INSERT [dbo].[Canton] ([Id], [Name], [Province]) VALUES (14, N'Pococí', 7)
GO
SET IDENTITY_INSERT [dbo].[Canton] OFF
GO
SET IDENTITY_INSERT [dbo].[District] ON 
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (1, N'Carmen', 1)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (2, N'San Antonio', 1)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (3, N'Escazú Centro', 2)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (4, N'San Rafael', 2)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (5, N'Alajuela Centro', 3)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (6, N'La Guácima', 3)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (7, N'Atenas Centro', 4)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (8, N'San Isidro', 4)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (9, N'Cartago Centro', 5)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (10, N'Oriental', 5)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (11, N'Turrialba Centro', 6)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (12, N'Santa Teresita', 6)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (13, N'Heredia Centro', 7)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (14, N'Mercedes', 7)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (15, N'Barva Centro', 8)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (16, N'San Pedro', 8)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (17, N'Liberia Centro', 9)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (18, N'Cañas Dulces', 9)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (19, N'Nicoya Centro', 10)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (20, N'Sámara', 10)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (21, N'Puntarenas Centro', 11)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (22, N'Chacarita', 11)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (23, N'Quepos Centro', 12)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (24, N'Manuel Antonio', 12)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (25, N'Limón Centro', 13)
GO
INSERT [dbo].[District] ([Id], [Name], [Canton]) VALUES (26, N'Cahuita', 13)
GO
SET IDENTITY_INSERT [dbo].[District] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (1, N'Disponible', N'Propiedades disponibles para su adquisición.', N'Propiedades')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (2, N'Adquirida', N'Propiedades que fueron adquiridas, se mantienen pa', N'Propiedades')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (3, N'Activo', N'Usuarios activos y funcionales en el sistema.', N'Empleado')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (4, N'Inactivo', N'Usuarios inactivos, se mantienen por historial de ', N'Empleado')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (5, N'Bloqueado', N'Usuarios que se encuentran activos pero no pueden ', N'Empleado')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (6, N'Pendiente', N'Cotizacion pendiente', N'Cotizaciones')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (7, N'Realizada', N'Cotizacion realizada', N'Cotizaciones')
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (8, N'Por aceptar', N'Nueva cita pendiente de confirmación ', N'Citas');
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (9, N'Confirmada', N'La cita ha sido confirmada.', N'Citas');
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (10, N'Cancelada', N'La cita ha sido cancelada.', N'Citas');
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (11, N'Concretada', N'La cita ha sido atendida.', N'Citas');
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (12, N'Espera', N'Contacto en espera.', N'Contacto');
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [RelatedTable]) VALUES (13, N'Atendida', N'Contacto realizado.', N'Contacto');
GO

SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[PropertyType] ON 
GO
INSERT [dbo].[PropertyType] ([Id], [Type], [Description]) VALUES (1, N'Residencial', N'Son propiedades de uso personal')
GO
INSERT [dbo].[PropertyType] ([Id], [Type], [Description]) VALUES (2, N'Comercial', N'Propiedades que pueden tener un uso comercial')
GO
SET IDENTITY_INSERT [dbo].[PropertyType] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactionType] ON 
GO
INSERT [dbo].[TransactionType] ([Id], [Type], [Description]) VALUES (1, N'Alquiler', N'Propiedades para uso por pago mensual')
GO
INSERT [dbo].[TransactionType] ([Id], [Type], [Description]) VALUES (2, N'Compra', N'Propiedades para adquisición por parte del interesado')
GO
SET IDENTITY_INSERT [dbo].[TransactionType] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentType] ON 
GO
INSERT [dbo].[DocumentType] ([Id], [Document]) VALUES (1, N'Persona Física Nacional')
GO
INSERT [dbo].[DocumentType] ([Id], [Document]) VALUES (2, N'Cédula DIMEX')
GO
INSERT [dbo].[DocumentType] ([Id], [Document]) VALUES (3, N'Otro')
GO
SET IDENTITY_INSERT [dbo].[DocumentType] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [DocumentType], [Identification], [Name], [FirstLastName], [SecondLastName], [PhoneNumber], [Email], [CreationDate]) VALUES (1, 1, N'0123456789', N'Juan', N'Perez', N'Gomez', N'55512341', N'jeffry1597@gmail.com', CAST(N'2024-03-18T19:47:36.713' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [DocumentType], [Identification], [Name], [FirstLastName], [SecondLastName], [PhoneNumber], [Email], [CreationDate]) VALUES (2, 2, N'0987654321', N'Maria', N'Gonzalez', N'Gomez', N'55543211', N'jeffry15@gmail.com', CAST(N'2024-03-18T19:47:36.737' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [DocumentType], [Identification], [Name], [FirstLastName], [SecondLastName], [PhoneNumber], [Email], [CreationDate]) VALUES (3, 1, N'0117220391', N'Angelo Julio', N'Alfaro', N'Atencio', N'77777777', N'prueba@hotmail.com', CAST(N'2024-03-18T20:01:25.550' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (1, N'Administrador', N'Administrador de la plataforma')
GO
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (2, N'Vendedor', N'Vendedor de propiedades')
GO
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (3, N'Ingeniero', N'Ingeniero de cotizaciones')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
------------------------Insert Employees
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [UserId], [Password],[PasswordTemp], [Role], [Status], [ImageProfile], [RetirementDate],[IsTemporary]) VALUES (1, 1, N'XnhatR1AMfcYSiMpqGo60w==', N'secretpass', 1, 3, N'/AppContent/Users/Images/ProfileDefault.png', NULL,0)
GO
INSERT [dbo].[Employee] ([Id], [UserId], [Password],[PasswordTemp], [Role], [Status], [ImageProfile], [RetirementDate],[IsTemporary]) VALUES (3, 3, N'XnhatR1AMfcYSiMpqGo60w==', N'secretpass', 2, 3, N'/AppContent/Users/Images/ProfileDefault.png', NULL,0)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO

-- Insert Currency
EXEC InsertCurrencySP 'Colón costarricense', 'CRC', '₡'
GO

EXEC InsertCurrencySP 'Dólar estadounidense', 'USD', '$'
GO

------------------------Insert Properties
SET IDENTITY_INSERT [dbo].[Property] ON 
GO
INSERT [dbo].[Property] ([Id], [Title], [EmployeeId], [Currency], [Price], [AreaLand], [AreaBuild], [Description], [SoldDate], [PropertyType], [Transactiontype], [PropertyStatus]) VALUES (1, N'Propiedad azul', 3, 1, CAST(5000.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(10, 2)), CAST(200.00 AS Decimal(10, 2)), N'Es una propiedad azul.', NULL, 1, 1, 1)
GO
INSERT [dbo].[Property] ([Id], [Title], [EmployeeId], [Currency], [Price], [AreaLand], [AreaBuild], [Description], [SoldDate], [PropertyType], [Transactiontype], [PropertyStatus]) VALUES (2, N'Propiedad verde', 1, 2, CAST(8000.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(10, 2)), CAST(250.00 AS Decimal(10, 2)), N'Casa verde de dos pisos', NULL, 2, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Property] OFF
GO
SET IDENTITY_INSERT [dbo].[PropertyAddress] ON 
GO
INSERT [dbo].[PropertyAddress] ([Id], [PropertyId], [DistrictId], [OtherSigns]) VALUES (1, 1, 2, N'De la esquina doscientos metros.')
GO
INSERT [dbo].[PropertyAddress] ([Id], [PropertyId], [DistrictId], [OtherSigns]) VALUES (2, 2, 12, N'Centro')
GO
SET IDENTITY_INSERT [dbo].[PropertyAddress] OFF
GO
--------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[PropertyImg] ON 
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (1, N'/AppContent/Properties/Images/Property1/Img-1.jpg', 1)
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (2, N'/AppContent/Properties/Images/Property1/Img-2.jpg', 1)
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (3, N'/AppContent/Properties/Images/Property1/Img-3.jpg', 1)
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (4, N'/AppContent/Properties/Images/Property2/Img-1.jpg', 2)
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (5, N'/AppContent/Properties/Images/Property2/Img-2.jpg', 2)
GO
INSERT [dbo].[PropertyImg] ([Id], [ImageUrl], [PropertyId]) VALUES (6, N'/AppContent/Properties/Images/Property2/Img-3.jpg', 2)
GO
SET IDENTITY_INSERT [dbo].[PropertyImg] OFF
GO
SET IDENTITY_INSERT [dbo].[PropertyDoc] ON 
GO
INSERT [dbo].[PropertyDoc] ([Id], [Name], [DocUrl], [PropertyId]) VALUES (1, N'Test.pdf', N'/AppContent/Properties/Docs/Property2/Test.pdf.pdf', 2)
GO
INSERT [dbo].[PropertyDoc] ([Id], [Name], [DocUrl], [PropertyId]) VALUES (3, N'G6_SC603_J_Alcance.pdf', N'/AppContent/Properties/Docs/Property2/G6_SC603_J_Alcance.pdf.pdf', 2)
GO
SET IDENTITY_INSERT [dbo].[PropertyDoc] OFF
GO
--------------------------------------------------------------------------------------------
--SET IDENTITY_INSERT [dbo].[AuditLog] ON 
--GO
--INSERT [dbo].[AuditLog] ([Id], [EmployeeId], [TableName], [CurrentAudit], [CreationDateAudit]) VALUES (1, 1, N'Empleado', N'Acción: Insertar, Empleado: Juan Perez Gomez, Identificación: 0123456789, Sección Modificada: Empleado, Datos Insertados:
--', CAST(N'2024-03-18T19:48:35.083' AS DateTime))
--GO
--INSERT [dbo].[AuditLog] ([Id], [EmployeeId], [TableName], [CurrentAudit], [CreationDateAudit]) VALUES (4, 4, N'Empleado', N'Acción: Insertar, Empleado: Angelo Julio Alfaro Atencio, Identificación: 0117220391, Sección Modificada: Empleado, Datos Insertados:
--', CAST(N'2024-03-18T20:02:23.453' AS DateTime))
--GO
--INSERT [dbo].[AuditLog] ([Id], [EmployeeId], [TableName], [CurrentAudit], [CreationDateAudit]) VALUES (5, 4, N'Empleado', N'Acción: Actualizar, Empleado: Angelo Julio Alfaro Atencio, Identificación: 0117220391, Sección Modificada: Empleado, Datos Actualizados:
--Rol Antiguo: Vendedor
--Estado Antiguo: Activo
--Rol Nuevo: Vendedor
--Estado Nuevo: Activo
--Imagen de Perfil: /AppContent/Users/Images/ProfileDefault.png

--', CAST(N'2024-03-18T20:02:23.483' AS DateTime))
--GO
--INSERT [dbo].[AuditLog] ([Id], [EmployeeId], [TableName], [CurrentAudit], [CreationDateAudit]) VALUES (6, 4, N'Propiedad', N'Acción: Insertar, Empleado: Angelo Julio Alfaro Atencio, Sección Modificada: Propiedad, Datos Insertados:
--Título: Propiedad azul
--Precio: 5000.00
--Área de Terreno: 200.00
--Área Construida: 200.00
--Descripción: Es una propiedad azul.
--Fecha de Venta: No vendida
--Tipo de Propiedad: Residencial
--Tipo de Transacción: Alquiler
--Estado de la Propiedad: Disponible

--', CAST(N'2024-03-18T20:16:04.787' AS DateTime))
--GO
--INSERT [dbo].[AuditLog] ([Id], [EmployeeId], [TableName], [CurrentAudit], [CreationDateAudit]) VALUES (7, 1, N'Propiedad', N'Acción: Insertar, Empleado: Juan Perez Gomez, Sección Modificada: Propiedad, Datos Insertados:
--Título: Propiedad verde
--Precio: 8000.00
--Área de Terreno: 250.00
--Área Construida: 250.00
--Descripción: Casa verde de dos pisos
--Fecha de Venta: No vendida
--Tipo de Propiedad: Comercial
--Tipo de Transacción: Compra
--Estado de la Propiedad: Disponible

--', CAST(N'2024-03-18T20:17:23.820' AS DateTime))
--GO
--SET IDENTITY_INSERT [dbo].[AuditLog] OFF
--GO
SET IDENTITY_INSERT [dbo].[News] ON 
GO
INSERT [dbo].[News] ([Id], [EmployeeId], [CreationDate], [Title], [Description], [Url],[ImageUrl]) VALUES (1, 1, CAST(N'2024-03-18T19:48:40.330' AS DateTime), N'¿Cuáles son los pasos para vender una propiedad?', N'Los diferentes pasos son: 
1.Estimación de precios
2. Listado
3. Anuncio y visitas
4. Firma el acuerdo de venta
5. Firma la escritura auténtica de compraventa', N'local', N'local')
GO
INSERT [dbo].[News] ([Id], [EmployeeId], [CreationDate], [Title], [Description], [Url],[ImageUrl]) VALUES (2, 1, CAST(N'2024-03-18T19:48:40.347' AS DateTime), N'¿Cuál es el plazo para un acuerdo de venta y el acto auténtico?', N'El período entre la firma del compromiso y la escritura auténtica de venta es de aproximadamente 3 meses. 
Sin embargo, este período puede variar dependiendo de las circunstancias y el acuerdo de las partes. ', N'local', N'local')
GO
INSERT [dbo].[News] ([Id], [EmployeeId], [CreationDate], [Title], [Description], [Url],[ImageUrl]) VALUES (3, 1, CAST(N'2024-03-18T19:48:40.350' AS DateTime), N'¿Cuáles son los pasos para comprar una propiedad?', N'Los diferentes pasos son: 
1.Se establecer un presupuesto, una contribución personal y preguntar sobre una posible hipoteca.
2. Encontrar vivienda.
3. Oferta de compra.
4. Firma el acuerdo de venta
5. Firma la escritura auténtica de compraventa', N'local', N'local')
GO
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[QuoteRooms] ON 
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (1, N'Baño principal')
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (2, N'Baño secundario')
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (3, N'Cochera')
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (4, N'Cocina')
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (5, N'Dormitorio principal')
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (6, 'Sala de estar');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (7, 'Comedor');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (8, 'Sala de juegos');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (9, 'Oficina en casa');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (10, 'Sala de música');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (11, 'Gimnasio');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (12, 'Despensa');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (13, 'Lavandería');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (14, 'Bodega');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (15, 'Vestíbulo');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (16, 'Terraza');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (17, 'Balcón');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (18, 'Ático');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (19, 'Sótano');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (20, 'Sala de cine');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (21, 'Sala de lectura');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (22, 'Sala de manualidades');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (23, 'Taller');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (24, 'Sala de reuniones');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (25, 'Estudio');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (26, 'Invernadero');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (27, 'Patio trasero');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (28, 'Porche');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (29, 'Cuarto de herramientas');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (30, 'Guardería');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (31, 'Desván');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (32, 'Cuarto de huéspedes');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (33, 'Cuarto de juegos');
GO
INSERT INTO QuoteRooms (Id, RoomName) VALUES (34, 'Cuarto de niños');
GO
SET IDENTITY_INSERT [dbo].[QuoteRooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Quote] ON 
GO
INSERT [dbo].[Quote] ([Id], [ClientId], [CreationDate], [Status], [DocUrl], [Details]) VALUES (1, 1, CAST(N'2024-04-14T12:20:48.660' AS DateTime), 6, NULL, N'Test')
GO
SET IDENTITY_INSERT [dbo].[Quote] OFF
GO
SET IDENTITY_INSERT [dbo].[QuoteDetailsPerRoom] ON 
GO
INSERT [dbo].[QuoteDetailsPerRoom] ([Id], [QuoteId], [QuoteRoomId], [Quantity]) VALUES (1, 1, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[QuoteDetailsPerRoom] OFF
GO
--Insert Appointment
EXEC InsertAppointmentSP N'2024-04-17 10:00', 2, 1
EXEC InsertAppointmentSP N'2024-04-18 10:00', 2, 2
EXEC InsertAppointmentSP N'2024-07-18 10:00', 1, 2
EXEC InsertAppointmentSP N'2024-06-15 10:00', 1, 1