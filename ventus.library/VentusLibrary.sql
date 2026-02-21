/* ============================================================ 
    DATABASE: VentusLibrary 
    Description: Sistema de gestión de biblioteca 
    Arquitectura: Soft Delete + Reglas de Integridad 
    ============================================================ */ 

--------------------------------------------------------------- 
-- 1 Crear Base de Datos si no existe 
--------------------------------------------------------------- 
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'VentusLibrary') 
BEGIN 
    CREATE DATABASE VentusLibrary; 
END 
GO 

USE VentusLibrary; 
GO 

/* ============================================================ 
    2️ TABLA: Author 
    ============================================================ */ 
CREATE TABLE dbo.Author ( 
    Id INT IDENTITY(1,1) NOT NULL, 
    FullName VARCHAR(200) NOT NULL, 
    Description VARCHAR(200) NULL, 
    BirthDate DATE NULL, 
    City VARCHAR(100) NULL, 
    Email VARCHAR(100) NULL, 

    -- Soft Delete (0 = Activo, 1 = Eliminado) 
    IsSoftDelete BIT NOT NULL DEFAULT 0, 
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 
    UpdatedAt DATETIME2 NULL, 
    DeletedAt DATETIME2 NULL, 

    CONSTRAINT PK_Author PRIMARY KEY (Id), 
    CONSTRAINT UQ_Author_Email UNIQUE (Email), 

    -- No permite fechas futuras 
    CONSTRAINT CHK_Author_BirthDate 
        CHECK (BirthDate IS NULL OR BirthDate <= CAST(GETDATE() AS DATE)), 

    -- Consistencia Soft Delete 
    CONSTRAINT CHK_Author_SoftDeleteDeletedAt 
        CHECK ( 
            (IsSoftDelete = 0 AND DeletedAt IS NULL) OR 
            (IsSoftDelete = 1 AND DeletedAt IS NOT NULL) 
        ) 
); 
GO 


/* ============================================================ 
    3️ TABLA: Genre 
    ============================================================ */ 
CREATE TABLE dbo.Genre ( 
    Id INT IDENTITY(1,1) NOT NULL, 
    Name VARCHAR(50) NOT NULL, 
    Description VARCHAR(200) NULL, 

    IsSoftDelete BIT NOT NULL DEFAULT 0, 
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 
    UpdatedAt DATETIME2 NULL, 
    DeletedAt DATETIME2 NULL, 

    CONSTRAINT PK_Genre PRIMARY KEY (Id), 
    CONSTRAINT UQ_Genre_Name UNIQUE (Name), 

    CONSTRAINT CHK_Genre_SoftDeleteDeletedAt 
        CHECK ( 
            (IsSoftDelete = 0 AND DeletedAt IS NULL) OR 
            (IsSoftDelete = 1 AND DeletedAt IS NOT NULL) 
        ) 
); 
GO 


/* ============================================================ 
    4️ TABLA: Book 
    ============================================================ */ 
CREATE TABLE dbo.Book ( 
    Id INT IDENTITY(1,1) NOT NULL, 
    Title VARCHAR(200) NOT NULL, 
    Description VARCHAR(200) NULL, 
    PublicationYear INT NULL, 
    GenreId INT NOT NULL, 
    PageCount INT NOT NULL, 
    AuthorId INT NOT NULL, 

    IsSoftDelete BIT NOT NULL DEFAULT 0, 
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 
    UpdatedAt DATETIME2 NULL, 
    DeletedAt DATETIME2 NULL, 

    CONSTRAINT PK_Book PRIMARY KEY (Id), 

    -- Relaciones 
    CONSTRAINT FK_Book_Genre 
        FOREIGN KEY (GenreId) REFERENCES dbo.Genre(Id), 

    CONSTRAINT FK_Book_Author 
        FOREIGN KEY (AuthorId) REFERENCES dbo.Author(Id), 

    -- Reglas de negocio 
    CONSTRAINT CHK_Book_PublicationYear 
        CHECK ( 
            PublicationYear IS NULL OR 
            PublicationYear BETWEEN -3000 AND YEAR(GETDATE()) 
        ), 

    CONSTRAINT CHK_Book_PageCount 
        CHECK (PageCount > 0), 

    CONSTRAINT CHK_Book_SoftDeleteDeletedAt 
        CHECK ( 
            (IsSoftDelete = 0 AND DeletedAt IS NULL) OR 
            (IsSoftDelete = 1 AND DeletedAt IS NOT NULL) 
        ) 
); 
GO 


/* ============================================================ 
    5️ TABLA: BookLimit 
    ============================================================ */ 
CREATE TABLE dbo.BookLimit ( 
    Id INT IDENTITY(1,1) NOT NULL, 

    -- 1 = Global 
    -- 2 = Por Autor 
    -- 3 = Por Género 
    LimitType TINYINT NOT NULL, 
    MaxBooks INT NOT NULL, 

    -- Estado funcional de la regla 
    IsActive BIT NOT NULL DEFAULT 1, 

    -- Soft Delete 
    IsSoftDelete BIT NOT NULL DEFAULT 0, 
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 
    UpdatedAt DATETIME2 NULL, 
    DeletedAt DATETIME2 NULL, 

    CONSTRAINT PK_BookLimit PRIMARY KEY (Id), 

    CONSTRAINT CHK_BookLimit_Type 
        CHECK (LimitType IN (1,2,3)), 

    CONSTRAINT CHK_BookLimit_MaxBooks 
        CHECK (MaxBooks > 0), 

    CONSTRAINT UQ_BookLimit_Type UNIQUE (LimitType) 
); 
GO 


/* ============================================================ 
    6️ ÍNDICES PARA OPTIMIZACIÓN 
    ============================================================ */ 

-- Índices para relaciones 
CREATE INDEX IX_Book_AuthorId ON dbo.Book(AuthorId); 
CREATE INDEX IX_Book_GenreId ON dbo.Book(GenreId); 

-- Índices filtrados para registros activos (Soft Delete = 0) 
CREATE INDEX IX_Author_Active 
    ON dbo.Author(Id) 
    WHERE IsSoftDelete = 0; 

CREATE INDEX IX_Genre_Active 
    ON dbo.Genre(Id) 
    WHERE IsSoftDelete = 0; 

CREATE INDEX IX_Book_Active 
    ON dbo.Book(Id) 
    WHERE IsSoftDelete = 0; 
GO 

-- Insertar algunos géneros por defecto 
INSERT INTO Genre (Name, Description) VALUES 
('Novela', 'Novelas literarias'), 
('Ciencia Ficcción', 'Libros de ciencia ficción'), 
('Fantasia', 'Libros de fantasía'), 
('Horror', 'Libros de terror y suspenso'), 
('Biografía', 'Biografías y autobiografías'), 
('Historia', 'Libros históricos'); 

-- Insertar límite global por defecto 
INSERT INTO BookLimit (LimitType, MaxBooks) VALUES 
(1, 100), 
(2, 100); 
GO
