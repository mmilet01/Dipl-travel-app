use mmileta
go

ALTER TABLE dbo.usersRelationship
ADD relationshipStatus int NOT NULL
constraint FK_UserRelationshipStatus_Stauts
FOREIGN KEY (relationshipStatus) REFERENCES mmileta.dbo.userRelationshipStatus(statusId)