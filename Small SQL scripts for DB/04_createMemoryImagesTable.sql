use mmileta
go

CREATE TABLE mmileta.dbo.memoryImages(
    memoryImageId int PRIMARY KEY IDENTITY(1,1),
    photoPath varchar(50) NOT NULL,
	belongsTo int  NOT NULL,
	constraint FK_MemoryImages_Users
	FOREIGN KEY (belongsTo) REFERENCES mmileta.dbo.users(userId)
);