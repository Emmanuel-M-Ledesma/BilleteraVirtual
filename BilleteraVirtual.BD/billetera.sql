--Crea Tablas
--Primary key
--Foreign Key

Create database Billetera
go

use Billetera
go
CREATE TABLE [Personas] (
  [tipo_documento] char(3),
  [nro_documento] char(8),
  [nombre] varchar(50),
  [apellido] varchar(50),
  [telefono] varchar(10),  
  PRIMARY KEY ([tipo_documento], [nro_documento])    	  
);
go
CREATE TABLE [UsuarioBilletera] (
  [Id] NVARCHAR(450),
  [Email] NVARCHAR(256),
  [UserName] NVARCHAR(256),
  [tipo_documento] char(3)NOT NULL,
  [nro_documento] char(8) NOT NULL,
  [CVU] bigint NOT NULL,
  PRIMARY KEY ([Id], [Email],[UserName])    	  
);
go
CREATE TABLE [Contactos] (
  [Id] NVARCHAR(450),
  [UserName] NVARCHAR(256),  
  [Email] NVARCHAR(256),
  [CVU] bigint
);
go
CREATE TABLE [Tipos_dep-extr] (
  [id_tipo] int,
  [nombre_tipo] varchar(15),
  PRIMARY KEY ([id_tipo])
);
go
CREATE TABLE [compra-venta_Divisas] (
  [id_tipo] int,
  [id_extraccion] int,
  [id_deposito] int,
  [monto] decimal,
  [id_moneda] int
);
go
CREATE TABLE [Cuentas] (
  [CVU] bigint ,
  [alias] varchar(50),
  [saldo] decimal,
  [id_moneda] int
  PRIMARY KEY ([CVU])
);
go
CREATE TABLE [Operaciones] (
  [id_operacion] int,
  [CVU] bigint ,
  [fecha_operacion] date,
  [hora_oprecion] time,
  [id_deposito] int,
  [id_extraccion] int,
  [id_transferencia] int,
  PRIMARY KEY ([id_operacion])
);
go
CREATE TABLE [Depositos] (
  [id_deposito] int,
  [cod_seguridad] varchar(50),
  [id_tipo] int,
  [monto] decimal,
  [id_moneda] int,
  PRIMARY KEY ([id_deposito])    
);
go
CREATE TABLE [Monedas] (
  [id_moneda] int,
  [id_plataforma] varchar(150),
  [plataforma] varchar(250),   -- https://api.coingecko.com/api/v3/exchanges/
  [simbolo] varchar(100),
  [nom_moneda] varchar(100),
  [unidad] varchar(50),
  [Cot_Comp] decimal,
  [Cot_Vta] decimal,
  [tipo] varchar(50),
  PRIMARY KEY ([id_moneda])
);
go
CREATE TABLE [Extracciones] (
  [id_extraccion] int,
  [cod_seguridad] varchar(50),
  [id_tipo] int,
  [monto] decimal,
  [id_moneda] int,
  PRIMARY KEY ([id_extraccion])    
);
go
CREATE TABLE [Transferencias] (
  [id_transferencia] int,
  [CVU] bigint,
  [monto] decimal,
  [id_moneda] int,
  [concepto] varchar(50),
  [mensaje] varchar(50),
  PRIMARY KEY ([id_transferencia])  
);
go
-----------
ALTER TABLE [UsuarioBilletera] ADD FOREIGN KEY ([tipo_documento], [nro_documento]) REFERENCES [Personas]([tipo_documento], [nro_documento]);
ALTER TABLE [UsuarioBilletera] ADD FOREIGN KEY ([CVU])REFERENCES [Cuentas]([CVU]);
-----------
ALTER TABLE [Contactos] ADD FOREIGN KEY ([Id], [Email],[UserName])REFERENCES [UsuarioBilletera]([Id], [Email],[UserName]);
ALTER TABLE [Contactos] ADD FOREIGN KEY ([CVU])REFERENCES [Cuentas]([CVU]);
-----------
ALTER TABLE [compra-venta_Divisas] ADD FOREIGN KEY ([id_tipo])REFERENCES [Tipos_dep-extr]([id_tipo]);
ALTER TABLE [compra-venta_Divisas] ADD FOREIGN KEY ([id_moneda])REFERENCES [Monedas]([id_moneda]);
-----------
ALTER TABLE [Operaciones] ADD FOREIGN KEY ([CVU])REFERENCES [Cuentas]([CVU]);
ALTER TABLE [Operaciones] ADD FOREIGN KEY ([id_deposito])REFERENCES [Depositos]([id_deposito]);
ALTER TABLE [Operaciones] ADD FOREIGN KEY ([id_extraccion])REFERENCES [Extracciones]([id_extraccion]);
ALTER TABLE [Operaciones] ADD FOREIGN KEY ([id_transferencia])REFERENCES [Transferencias]([id_transferencia]);
-----------
ALTER TABLE [Depositos] ADD FOREIGN KEY ([id_tipo])REFERENCES [Tipos_dep-extr]([id_tipo]);

-----------

ALTER TABLE [Extracciones] ADD FOREIGN KEY ([id_tipo])REFERENCES [Tipos_dep-extr]([id_tipo]);
-----------

ALTER TABLE [Transferencias] ADD FOREIGN KEY ([CVU])REFERENCES [Cuentas]([CVU]);

---------
ALTER TABLE [Cuentas] ADD FOREIGN KEY ([id_moneda])REFERENCES [Monedas]([id_moneda]);
