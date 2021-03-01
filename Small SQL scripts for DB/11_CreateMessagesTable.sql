use mmileta
go

CREATE TABLE mmileta.dbo.messages(
    messageId int PRIMARY KEY IDENTITY(1,1),
    fromId int NOT NULL,
	toId int NULL,
	toGroupId int NULL,
	messageBody varchar(500) NOT NULL,
	sentAt DATE NOT NULL,
	constraint FK_MessagesFrom_Users
	FOREIGN KEY (fromId) REFERENCES mmileta.dbo.users(userId),
	constraint FK_MessagesTo_Users
	FOREIGN KEY (toId) REFERENCES mmileta.dbo.users(userId),
	constraint FK_Group_Users
	FOREIGN KEY (toGroupId) REFERENCES mmileta.dbo.groups(groupId)
);