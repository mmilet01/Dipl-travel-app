use mmileta
go

CREATE TABLE mmileta.dbo.users(
    userId int PRIMARY KEY IDENTITY(1,1),
    firstName varchar(50) NOT NULL,
	lastName varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	passwordField text NOT NULL,
	createdOn date NOT NULL
);