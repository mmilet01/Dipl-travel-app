use mmileta
go

CREATE TABLE mmileta.dbo.memories(
    memoryId int PRIMARY KEY IDENTITY(1,1),
    title varchar(50) NOT NULL,
	memoryDescription varchar(500) NOT NULL,
	isPrivate bit NOT NULL,
	createdOn date NOT NULL,
	createdBy int NOT NULL,
	constraint FK_Memories_Users
	FOREIGN KEY (createdBy) REFERENCES mmileta.dbo.users(userId)
);