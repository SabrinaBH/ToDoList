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


INSERT INTO Usuario (Nombre, PrimerApellido, SegundoApellido, Email, EsUsuarioDeJuego)
VALUES ('The Legend of Zelda', '', '', 'Zelda@gmail.com', 'true'),
		('Dark Souls I', '', '', 'Solaire@gmail.com', 'true');

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



INSERT INTO Tarea (Titulo, Descripcion, FechaInicial, FechaFinal, Dificultad, Prioridad, IdentificadorUsuarioCreador, IdentificadorCategoria, IdentificadorEstado)
VALUES ('Derrota al jefe final', 'Derrota al Señor Oscuro: Ganondorf', '2023-12-05', '', '5', '3', '42425333-BD31-4A6A-B2A3-85D6980199B1', '16', '0'),
		('Derrota al jefe del Patíbulo del Desierto', 'Derrota al Fosíl de las Sombras: Stallord', '2023-12-05', '2023-12-15', '3', '3', '42425333-BD31-4A6A-B2A3-85D6980199B1', '16', '2'),
		('Derrota al jefe de Celestia', 'Derrota al Dragón de las Sombras: Argorok', '2023-12-05', '2023-12-15', '4', '3', '42425333-BD31-4A6A-B2A3-85D6980199B1', '16', '2'),
		('Derrota al jefe del Palacio del Crepúsculo', 'Derrota al Rey Usurpador: Zant', '2023-12-05', '', '5', '3', '42425333-BD31-4A6A-B2A3-85D6980199B1', '16', '0'),
		('Derrota al jefe de Anor Londo', 'Derrota a Ornstein el Asesino de dragones y Smough el Verdugo', '2023-12-02', '2023-12-27', '5', '3', 'D67C161B-76BC-483A-B130-B9A5C7EE05CA', '16', '2'),
		('Derrota al jefe de la Tumba de los gigantes', 'Derrota a Nito el Rey del Cementerio', '2023-12-02', '', '5', '3', 'D67C161B-76BC-483A-B130-B9A5C7EE05CA', '16', '0'),
		('Derrota al jefe del Bosque Real', 'Derrota al Caballero Artorias', '2023-12-02', '', '5', '1', 'D67C161B-76BC-483A-B130-B9A5C7EE05CA', '16', '0');

Delete from Tarea where Tarea.IdentificadorUsuarioCreador = '42425333-BD31-4A6A-B2A3-85D6980199B1'
Delete from Usuario where Usuario.IdentificadorUsuario = '42425333-BD31-4A6A-B2A3-85D6980199B1'