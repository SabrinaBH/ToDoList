USE ProyectoToDoList;
GO

CREATE TABLE Usuario (
IdentificadorUsuario INT NOT NULL IDENTITY(1, 1),
Nombre VARCHAR (50) NOT NULL,
PrimerApellido VARCHAR (50) NOT NULL,
SegundoApellido VARCHAR (50),
Nickname VARCHAR(50), 
Email VARCHAR(100) NOT NULL,
PRIMARY KEY (IdentificadorUsuario),
UNIQUE(Email)
)

CREATE TABLE Categoria(
IdentificadorCategoria INT NOT NULL,
NombreCategoria VARCHAR(20) NOT NULL,
IdentificadorUsuarioCreador INT NOT NULL,
PRIMARY KEY (IdentificadorCategoria),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario
)

CREATE TABLE Estado(
IdentificadorEstado INT NOT NULL,
NombreEstado VARCHAR(20) NOT NULL,
IdentificadorUsuarioCreador INT NOT NULL,
PRIMARY KEY (IdentificadorEstado),
FOREIGN KEY (IdentificadorUsuarioCreador) REFERENCES Usuario
)

CREATE TABLE Tarea (
IdentificadorTarea INT NOT NULL IDENTITY (1,1),
Titulo VARCHAR (100) NOT NULL,
Descripcion VARCHAR (1000),
FechaInicial DATE NOT NULL,
FechaFinal DATE,
Dificultad SMALLINT, 
Prioridad SMALLINT,
IdentificadorUsuarioCreador INT NOT NULL,
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

INSERT INTO Usuario
VALUES ('Admin', 'Deafult', '', '', 'adminDefault@gmail.com')

INSERT INTO Estado 
Values ('0', 'Pendientes', '1'), ('1', 'En Curso', '1'), ('2', 'Completadas', '1') 

INSERT INTO Categoria
Values ('0', 'Entretenimiento', '1'), ('1', 'Alimentación', '1'), ('2', 'Trabajo', '1'), ('3', 'Estudios', '1')

INSERT INTO Usuario
VALUES ('Sabrina', 'Brenes', 'Hernandez', 'EvenTinierTurtle', 'sabry.brenes@outlook.es'), 
		('Diego', 'Quesada', 'Barrantes', 'TinyTurtle', 'dqb25@gmail.com')

INSERT INTO Tarea
VALUES ('Proyecto Bases Avanzadas', 'Investigacion de base de datos Scylla', '2023-10-05', '2023-10-12', '5', '3', '2', '3', '1'),
		('Laboratorio Bases Avanzadas', 'Lab de ETL', '2023-10-06', '2023-10-09', '3', '3', '2', '3', '1')
