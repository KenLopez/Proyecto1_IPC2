CREATE DATABASE OTHELLO;

USE OTHELLO;

CREATE TABLE Usuario(
	idUsuario INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	apellido VARCHAR(100) NOT NULL,
	nombreUsuario VARCHAR(100) NOT NULL,
	contrase�a VARCHAR(100) NOT NULL,
	fechaNacimiento date NOT NULL,
	pais VARCHAR(50) NOT NULL,
	correo VARCHAR(100) NOT NULL
);

SELECT * FROM Usuario


INSERT INTO Usuario VALUES
('Usuario', 'Apellido', 'cristiano', 'Contrase�a', '2020-11-08', 'GUA', 'cristiano@gmail.com'),
('Usuario', 'Apellido', 'mats', 'Contrase�a', '2020-11-08', 'GUA', 'mats@gmail.com'),
('Usuario', 'Apellido', 'davdismag', 'Contrase�a', '2020-11-08', 'GUA', 'davdismag@gmail.com'),
('Usuario', 'Apellido', 'dieegobg', 'Contrase�a', '2020-11-08', 'GUA', 'dieegobg@gmail.com'),
('Usuario', 'Apellido', 'juliocotzo', 'Contrase�a', '2020-11-08', 'GUA', 'juliocotzo@gmail.com'),
('Usuario', 'Apellido', 'carloscordon', 'Contrase�a', '2020-11-08', 'GUA', 'carloscordon@gmail.com'),
('Usuario', 'Apellido', 'fraaans', 'Contrase�a', '2020-11-08', 'GUA', 'fraaans@gmail.com'),
('Usuario', 'Apellido', 'jposuna', 'Contrase�a', '2020-11-08', 'GUA', 'jposuna@gmail.com'),
('Usuario', 'Apellido', 'ptrcech', 'Contrase�a', '2020-11-08', 'GUA', 'ptrcech@gmail.com'),
('Usuario', 'Apellido', 'jcoloma', 'Contrase�a', '2020-11-08', 'GUA', 'jcoloma@gmail.com'),
('Usuario', 'Apellido', 'danlap', 'Contrase�a', '2020-11-08', 'GUA', 'danlap@gmail.com'),
('Usuario', 'Apellido', 'edwrick', 'Contrase�a', '2020-11-08', 'GUA', 'edwrick@gmail.com'),
('Usuario', 'Apellido', 'gpique', 'Contrase�a', '2020-11-08', 'GUA', 'gpique@gmail.com'),
('Usuario', 'Apellido', 'sramos', 'Contrase�a', '2020-11-08', 'GUA', 'sramos@gmail.com'),
('Usuario', 'Apellido', 'mhummels', 'Contrase�a', '2020-11-08', 'GUA', 'mhummels@gmail.com'),
('Usuario', 'Apellido', 'jdol8', 'Contrase�a', '2020-11-08', 'GUA', 'jdol8@gmail.com'),
('Usuario', 'Apellido', 'jdol9', 'Contrase�a', '2020-11-08', 'GUA', 'jdol9@gmail.com'),
('Usuario', 'Apellido', 'jdol10', 'Contrase�a', '2020-11-08', 'GUA', 'jdol10@gmail.com'),
('Usuario', 'Apellido', 'player1', 'Contrase�a', '2020-11-08', 'GUA', 'player1@gmail.com'),
('Usuario', 'Apellido', 'player2', 'Contrase�a', '2020-11-08', 'GUA', 'player2@gmail.com'),
('Usuario', 'Apellido', 'player3', 'Contrase�a', '2020-11-08', 'GUA', 'player3@gmail.com'),
('Usuario', 'Apellido', 'jkimmich', 'Contrase�a', '2020-11-08', 'GUA', 'jkimmich@gmail.com'),
('Usuario', 'Apellido', 'rlewan', 'Contrase�a', '2020-11-08', 'GUA', 'rlewan@gmail.com'),
('Usuario', 'Apellido', 'tmuller', 'Contrase�a', '2020-11-08', 'GUA', 'tmuller@gmail.com'),
('Usuario', 'Apellido', 'slakun10', 'Contrase�a', '2020-11-08', 'GUA', 'slakun10@gmail.com'),
('Usuario', 'Apellido', 'gabjesus', 'Contrase�a', '2020-11-08', 'GUA', 'gabjesus@gmail.com'),
('Usuario', 'Apellido', 'rsterling', 'Contrase�a', '2020-11-08', 'GUA', 'rsterling@gmail.com'),
('Usuario', 'Apellido', 'msalah', 'Contrase�a', '2020-11-08', 'GUA', 'msalah@gmail.com'),
('Usuario', 'Apellido', 'smane', 'Contrase�a', '2020-11-08', 'GUA', 'smane@gmail.com'),
('Usuario', 'Apellido', 'rfirmi', 'Contrase�a', '2020-11-08', 'GUA', 'rfirmi@gmail.com'),
('Usuario', 'Apellido', 'angeles97', 'Contrase�a', '2020-11-08', 'GUA', 'angeles97@gmail.com'),
('Usuario', 'Apellido', 'voupisfh', 'Contrase�a', '2020-11-08', 'GUA', 'voupisfh@gmail.com'),
('Usuario', 'Apellido', 'danncux', 'Contrase�a', '2020-11-08', 'GUA', 'danncux@gmail.com'),
('Usuario', 'Apellido', 'khlopez', 'Contrase�a', '2020-11-08', 'GUA', 'khlopez@gmail.com'),
('Usuario', 'Apellido', 'jpj007', 'Contrase�a', '2020-11-08', 'GUA', 'jpj007@gmail.com'),
('Usuario', 'Apellido', 'ivantest', 'Contrase�a', '2020-11-08', 'GUA', 'ivantest@gmail.com'),
('Usuario', 'Apellido', 'rudygopal', 'Contrase�a', '2020-11-08', 'GUA', 'rudygopal@gmail.com'),
('Usuario', 'Apellido', 'sergcastro', 'Contrase�a', '2020-11-08', 'GUA', 'sergcastro@gmail.com'),
('Usuario', 'Apellido', 'alex856', 'Contrase�a', '2020-11-08', 'GUA', 'alex856@gmail.com')


CREATE TABLE TipoPartida(
	idTipoPartida INT PRIMARY KEY IDENTITY,
	nombre VARCHAR(50),
);
INSERT INTO TipoPartida VALUES ('CL�SICO UN JUGADOR'), ('CL�SICO MULTIJUGADOR'), ('XTREAM MULTIJUGADOR REGULAR'), ('XTREAM MULTIJUGADOR INVERSO'), ('XTREAM UN JUGADOR REGULAR'), ('XTREAM UN JUGADOR INVERSO'), ('TORNEO');

CREATE TABLE Estado(
	idEstado INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(50) NOT NULL,
);
INSERT INTO Estado VALUES ('GANADO'), ('PERDIDO'), ('EMPATADO'), ('ABIERTO'), ('CERRADO');

CREATE TABLE Partida(
	idPartida INT PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	idAdversario INT NULL,
	horaFecha DATETIME NOT NULL,
	idTipoPartida INT NOT NULL,
	idEstado INT NOT NULL,
	turnos INT NOT NULL,
	tiempo VARCHAR(50) NULL,
	FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario),
	FOREIGN KEY (idAdversario) REFERENCES Usuario(idUsuario),
	FOREIGN KEY (idTipoPartida) REFERENCES TipoPartida(idTipoPartida),
	FOREIGN KEY (idEstado) REFERENCES Estado(idEstado),
);

CREATE TABLE Torneo(
	idTorneo INT PRIMARY KEY IDENTITY(1,1),
	idCreador INT NOT NULL,
	participantes INT NOT NULL,
	nombre VARCHAR (50) NOT NULL,
	FOREIGN KEY (idCreador) REFERENCES Usuario(idUsuario)
); 


CREATE TABLE Participante(
	idParticipante INT PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario),
);


USE OTHELLO
SELECT * FROM TipoPartida

USE OTHELLO
SELECT idPartida, Usuario.nombreUsuario, a.nombreUsuario as Adversario, horaFecha, TipoPartida.nombre as Tipo, Estado.nombre as Estado, turnos, tiempo FROM Partida
INNER JOIN Usuario on Usuario.idUsuario = Partida.idUsuario
LEFT OUTER JOIN Usuario a on a.idUsuario = Partida.idAdversario
INNER JOIN TipoPartida on TipoPartida.idTipoPartida = Partida.idTipoPartida
INNER JOIN Estado on Estado.idEstado = Partida.idEstado
