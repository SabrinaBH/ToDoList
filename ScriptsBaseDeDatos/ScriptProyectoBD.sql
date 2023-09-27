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