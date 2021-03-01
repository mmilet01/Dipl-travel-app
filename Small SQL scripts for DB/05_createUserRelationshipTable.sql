use mmileta
go

CREATE TABLE mmileta.dbo.usersRelationship(
    relationShip int PRIMARY KEY IDENTITY(1,1),
    firstUserId int NOT NULL,
	secondUserId int  NOT NULL,
	constraint FK_FirstUser_Users
	FOREIGN KEY (firstUserId) REFERENCES mmileta.dbo.users(userId),
	constraint FK_SecondUser_Users
	FOREIGN KEY (secondUserId) REFERENCES mmileta.dbo.users(userId)
);

-- this approach will have 2 rows per each user relationship -> can be reduced to only one row if neccessery for performance issues