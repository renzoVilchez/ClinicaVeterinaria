CREATE DATABASE ClinicaVeterinariaDB;
GO
USE ClinicaVeterinariaDB;
GO
--
-- ============================
-- 1. Usuarios
-- ============================
CREATE TABLE Usuarios (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombreUsuario NVARCHAR(100) NOT NULL,
    contrasena NVARCHAR(100) NOT NULL,
    rol NVARCHAR(30) CHECK (rol IN ('Administrador','Cliente','Veterinario')),
    estado BIT DEFAULT 1
);
GO

-- ============================
-- 2. Clientes
-- ============================
CREATE TABLE Clientes (
    idCliente INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NOT NULL,
    nombres NVARCHAR(100) NOT NULL,
    apellidoPaterno NVARCHAR(100) NOT NULL,
    apellidoMaterno NVARCHAR(100) NOT NULL,
    tipoDocumento NVARCHAR(20) CHECK (tipoDocumento IN ('DNI','Carnet de Extranjería')),
    numeroDocumento NVARCHAR(20) UNIQUE NOT NULL,
    celular NVARCHAR(20),
    direccion NVARCHAR(150),
    estado BIT DEFAULT 1,
    CONSTRAINT FK_Clientes_Usuarios
        FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
        ON DELETE CASCADE
);
GO

-- ============================
-- 3. Veterinarios
-- ============================
CREATE TABLE Veterinarios (
    idVeterinario INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NOT NULL,
    especialidad NVARCHAR(100),
    estado BIT DEFAULT 1,
	nombres NVARCHAR(100),
    apellidoPaterno NVARCHAR(100),
    apellidoMaterno NVARCHAR(100),
    tipoDocumento NVARCHAR(20),
    numeroDocumento NVARCHAR(20),
    celular NVARCHAR(20),
    direccion NVARCHAR(150),
    CONSTRAINT FK_Veterinarios_Usuarios
        FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
        ON DELETE CASCADE
);
GO

-- ============================
-- 4. Horarios Veterinarios
-- ============================
CREATE TABLE HorariosVeterinarios (
    idHorario INT IDENTITY(1,1) PRIMARY KEY,
    idVeterinario INT NOT NULL,
    diaSemana NVARCHAR(15) CHECK (diaSemana IN ('Lunes','Martes','Miercoles','Jueves','Viernes','Sabado')),
    horaInicio TIME NOT NULL DEFAULT '08:00',
    horaFin TIME NOT NULL DEFAULT '18:00',
    disponible BIT DEFAULT 1,
    CONSTRAINT FK_Horarios_Veterinarios
        FOREIGN KEY (idVeterinario) REFERENCES Veterinarios(idVeterinario)
        ON DELETE CASCADE
);
GO

-- ============================
-- 5. Especies
-- ============================
CREATE TABLE Especies (
    idEspecie INT IDENTITY(1,1) PRIMARY KEY,
    nombreEspecie NVARCHAR(50)
);
GO

-- ============================
-- 6. Mascotas
-- ============================
CREATE TABLE Mascotas (
    idMascota INT IDENTITY(1,1) PRIMARY KEY,
    idCliente INT NOT NULL,
    idEspecie INT NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    raza NVARCHAR(100),
    edad INT,
    peso DECIMAL(5,2),
    sexo NVARCHAR(10) CHECK (sexo IN ('Macho','Hembra')),
    estado BIT DEFAULT 1,
    CONSTRAINT FK_Mascotas_Clientes
        FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente)
        ON DELETE CASCADE,
    CONSTRAINT FK_Mascotas_Especies
        FOREIGN KEY (idEspecie) REFERENCES Especies(idEspecie)
        ON DELETE CASCADE
);
GO

-- ============================
-- 7. Servicios
-- ============================
CREATE TABLE Servicios (
    idServicio INT IDENTITY(1,1) PRIMARY KEY,
    nombreServicio NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(250),
    precio DECIMAL(10,2) NOT NULL
);
GO

-- ============================
-- 8. Citas
-- ============================
CREATE TABLE Citas (
    idCita INT IDENTITY(1,1) PRIMARY KEY,
    idMascota INT NOT NULL,
    idServicio INT NOT NULL,
    idVeterinario INT NOT NULL,
    fechaCita DATE NOT NULL,
    horaCita TIME NOT NULL,
    estado NVARCHAR(30) DEFAULT 'Pendiente'
        CHECK (estado IN ('Pendiente','Atendida','Cancelada','Completada')),
    fechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Citas_Mascotas
        FOREIGN KEY (idMascota) REFERENCES Mascotas(idMascota)
        ON DELETE CASCADE,
    CONSTRAINT FK_Citas_Servicios
        FOREIGN KEY (idServicio) REFERENCES Servicios(idServicio)
        ON DELETE CASCADE,

    -- *** NO CASCADE PARA EVITAR CICLOS ***
    CONSTRAINT FK_Citas_Veterinarios
        FOREIGN KEY (idVeterinario) REFERENCES Veterinarios(idVeterinario)
        ON DELETE NO ACTION
);
GO

-- ============================
-- 9. Tickets
-- ============================
CREATE TABLE Tickets (
    idTicket INT IDENTITY(1,1) PRIMARY KEY,
    idCita INT NOT NULL,
    codigoTicket AS ('TCK-' + RIGHT('0000' + CAST(idTicket AS VARCHAR(4)), 4)) PERSISTED,
    fechaEmision DATETIME DEFAULT GETDATE(),
    montoTotal DECIMAL(10,2) NOT NULL,
    metodoPago NVARCHAR(20) CHECK (metodoPago IN ('Efectivo','Yape','Tarjeta')),
    estado NVARCHAR(20) DEFAULT 'Pendiente'
        CHECK (estado IN ('Pendiente','Pagado','Cancelado')),
    fechaPago DATETIME NULL,
    observaciones NVARCHAR(200),
    CONSTRAINT FK_Tickets_Citas
        FOREIGN KEY (idCita) REFERENCES Citas(idCita)
        ON DELETE CASCADE
);
GO

-- ==========================================
-- 10. TABLA CHATBOT (preguntas frecuentes)
-- ==========================================
CREATE TABLE ChatBot (
    idPregunta INT IDENTITY(1,1) PRIMARY KEY,
    pregunta NVARCHAR(200) NOT NULL,
    respuesta NVARCHAR(500) NOT NULL
);
GO

-- ============================
-- TRIGGER
-- ============================
CREATE TRIGGER trg_SetFechaPago
ON Tickets
AFTER UPDATE
AS
BEGIN
    IF UPDATE(estado)
    BEGIN
        UPDATE T
        SET T.fechaPago = ISNULL(T.fechaPago, GETDATE())
        FROM Tickets T
        WHERE T.estado = 'Pagado'
          AND T.fechaPago IS NULL;
    END
END;
GO


