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
IdentificadorCategoria INT NOT NULL IDENTITY (0,1),
NombreCategoria VARCHAR(20) NOT NULL,
IdentificadorUsuarioCreador UNIQUEIDENTIFIER NOT NULL,
PRIMARY KEY (IdentificadorCategoria),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario
)

CREATE TABLE Estado(
IdentificadorEstado INT NOT NULL IDENTITY (0,1),
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
Values ('Pendientes', 'F547EF79-DB4A-4803-B74C-0BF7EC66D4391'), ('En Curso', 'F547EF79-DB4A-4803-B74C-0BF7EC66D439'), ('Completadas', 'F547EF79-DB4A-4803-B74C-0BF7EC66D439') 

INSERT INTO Categoria (NombreCategoria, IdentificadorUsuarioCreador)
Values ('Entretenimiento', 'F547EF79-DB4A-4803-B74C-0BF7EC66D4391'), ('Alimentación', 'F547EF79-DB4A-4803-B74C-0BF7EC66D4391'), ('Trabajo', 'F547EF79-DB4A-4803-B74C-0BF7EC66D4391'),
	('Estudios', 'F547EF79-DB4A-4803-B74C-0BF7EC66D4391')

INSERT INTO Usuario (Nombre, PrimerApellido, SegundoApellido, Email, EsUsuarioDeJuego)
VALUES ('Sabrina', 'Brenes', 'Hernandez', 'sabry.brenes@outlook.es', 'false'), 
		('Diego', 'Quesada', 'Barrantes', 'dqb25@gmail.com', 'false')

INSERT INTO Tarea (Titulo, Descripcion, FechaInicial, FechaFinal, Dificultad, Prioridad, IdentificadorUsuarioCreador, IdentificadorCategoria, IdentificadorEstado)
VALUES ('Proyecto Bases Avanzadas', 'Investigacion de base de datos Scylla', '2023-10-05', '2023-10-12', '5', '3', 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53', '3', '0'),
		('Laboratorio Bases Avanzadas', 'Lab de ETL', '2023-10-06', '2023-10-09', '3', '3', 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53', '3', '1')
