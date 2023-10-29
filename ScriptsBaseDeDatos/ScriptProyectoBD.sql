USE ProyectoToDoList;
GO

CREATE TABLE Usuario (
IdentificadorUsuario UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
Nombre VARCHAR (50) NOT NULL,
PrimerApellido VARCHAR (50),
SegundoApellido VARCHAR (50),
Email VARCHAR(100),
EsUsuarioDeJuego BIT NOT NULL, -- 1 si es de juegos, 0 si es una persona
PRIMARY KEY (IdentificadorUsuario),
UNIQUE(Email)
)

CREATE TABLE Categoria(
IdentificadorCategoria INT NOT NULL IDENTITY (1,1),
NombreCategoria VARCHAR(20) NOT NULL,
IdentificadorUsuarioCreador UNIQUEIDENTIFIER NOT NULL,
PRIMARY KEY (IdentificadorCategoria),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario
)

CREATE TABLE Estado(
IdentificadorEstado INT NOT NULL IDENTITY (1,1),
NombreEstado VARCHAR(20) NOT NULL,
IdentificadorUsuarioCreador UNIQUEIDENTIFIER NOT NULL,
PRIMARY KEY (IdentificadorEstado),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario
)

CREATE TABLE Tarea (
IdentificadorTarea UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
Titulo VARCHAR (100) NOT NULL,
Descripcion VARCHAR (1000),
FechaInicial DATE NOT NULL,
FechaFinal DATE,
Dificultad SMALLINT, 
Prioridad SMALLINT,
IdentificadorUsuarioCreador UNIQUEIDENTIFIER NOT NULL,
IdentificadorCategoria INT NOT NULL,
IdentificadorEstado INT NOT NULL,
PRIMARY KEY(IdentificadorTarea),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario,
FOREIGN KEY (IdentificadorCategoria) REFERENCES Categoria,
FOREIGN KEY (IdentificadorEstado) REFERENCES Estado
)

DROP TABLE Tarea;
DROP TABLE Categoria;
DROP TABLE Estado;
DROP TABLE Usuario;

INSERT INTO Usuario (Nombre, PrimerApellido, SegundoApellido, Email, EsUsuarioDeJuego)
VALUES ('Admin', '', '', 'AdminDefault@gmail.com', 'false');

INSERT INTO Estado (NombreEstado, IdentificadorUsuarioCreador)
Values ('Pendientes', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71'), ('En Curso', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71'), ('Completadas', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71') 

INSERT INTO Categoria (NombreCategoria, IdentificadorUsuarioCreador)
Values ('Entretenimiento', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71'), ('Alimentación', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71'), ('Trabajo', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71'),
	('Estudios', '6EB883AB-7CB1-469A-A97B-5B6BDF27FB71')

INSERT INTO Usuario (Nombre, PrimerApellido, SegundoApellido, Email, EsUsuarioDeJuego)
VALUES ('Sabrina', 'Brenes', 'Hernandez', 'sabry.brenes@outlook.es', 'false'), 
		('Diego', 'Quesada', 'Barrantes', 'dqb25@gmail.com', 'false')

INSERT INTO Tarea (Titulo, Descripcion, FechaInicial, FechaFinal, Dificultad, Prioridad, IdentificadorUsuarioCreador, IdentificadorCategoria, IdentificadorEstado)
VALUES ('Proyecto Bases Avanzadas', 'Investigacion de base de datos Scylla', '2023-10-05', '2023-10-12', '5', '3', 'E239C218-B4DD-4DD8-BEEA-24FEC7AF2F8B', '4', '1'),
		('Laboratorio Bases Avanzadas', 'Lab de ETL', '2023-10-06', '2023-10-09', '3', '3', 'E239C218-B4DD-4DD8-BEEA-24FEC7AF2F8B', '4', '2')
