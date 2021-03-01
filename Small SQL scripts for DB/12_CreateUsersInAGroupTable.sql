use mmileta
go

CREATE TABLE mmileta.dbo.usersInAGroup(
    usersGroupId int PRIMARY KEY IDENTITY(1,1),
    userId int NOT NULL,
	constraint FK_UsersGroup_Users
	FOREIGN KEY (userId) REFERENCES mmileta.dbo.users(userId),
	groupId int NOT NULL,
	constraint FK_Groups_User
	FOREIGN KEY (groupId) REFERENCES mmileta.dbo.groups(groupId)
);