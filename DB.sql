USE TaskManager;
GO

CREATE TABLE Usuarios (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Listas (
    ListaId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Titulo NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);
GO

CREATE TABLE Tareas (
    TareaId INT IDENTITY(1,1) PRIMARY KEY,
    ListaId INT NOT NULL,
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Completada BIT DEFAULT 0,
    FOREIGN KEY (ListaId) REFERENCES Listas(ListaId)
);
GO

-- Índice para mejorar las consultas por UsuarioId en la tabla Listas
CREATE INDEX IDX_Listas_UsuarioId ON Listas (UsuarioId);

-- Índice para mejorar las consultas por ListaId en la tabla Tareas
CREATE INDEX IDX_Tareas_ListaId ON Tareas (ListaId);

-- Constraint para asegurar que el Email sea único
ALTER TABLE Usuarios
ADD CONSTRAINT UQ_Email UNIQUE (Email);
GO

-- Constraint para poner que el valor de completado de las tareas sea 0 = No completado
ALTER TABLE Tareas
ADD CONSTRAINT DF_Tareas_Completada DEFAULT 0 FOR Completada;



