use mmileta
go

CREATE TABLE mmileta.dbo.usersTaggedOnMemory(
    tagId int PRIMARY KEY IDENTITY(1,1),
    memoryId int NOT NULL,
	constraint FK_TaggedMemory_Users
	FOREIGN KEY (memoryId) REFERENCES mmileta.dbo.memories(memoryId),
	userId int NOT NULL,
	constraint FK_TaggedUsers_Memory
	FOREIGN KEY (userId) REFERENCES mmileta.dbo.users(userId)
);