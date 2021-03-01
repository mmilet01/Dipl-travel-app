use mmileta
go

CREATE TABLE mmileta.dbo.usersImages(
    userImageId int PRIMARY KEY IDENTITY(1,1),
    photoPath varchar(50) NOT NULL,
	belongsTo int  NOT NULL,
	constraint FK_UserImages_Users
	FOREIGN KEY (belongsTo) REFERENCES mmileta.dbo.users(userId)
);