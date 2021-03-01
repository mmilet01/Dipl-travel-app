use mmileta
go

CREATE TABLE mmileta.dbo.notificationType(
    notificationTypeId int PRIMARY KEY IDENTITY(1,1),
    name varchar(50) NOT NULL
);



CREATE TABLE mmileta.dbo.notifications(
    notificationId int PRIMARY KEY IDENTITY(1,1),
    fromId int NOT NULL,
	toId int NULL,
	toGroupId int NULL,
	notificationTypeId int NOT NULL,
	createdDate DATE NOT NULL,
	constraint FK_Notification_NotificationType
	FOREIGN KEY (notificationTypeId) REFERENCES mmileta.dbo.notificationType(notificationTypeId),
	constraint FK_NotificationTo_Users
	FOREIGN KEY (toId) REFERENCES mmileta.dbo.users(userId),
	constraint FK_GroupNotification_Users
	FOREIGN KEY (toGroupId) REFERENCES mmileta.dbo.groups(groupId),
	constraint FK_NotificationFrom_Users
	FOREIGN KEY (fromId) REFERENCES mmileta.dbo.users(userId)
);