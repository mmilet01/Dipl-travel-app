use mmileta
go

CREATE TABLE mmileta.dbo.groups(
    groupId int PRIMARY KEY IDENTITY(1,1),
    groupName varchar(50) NOT NULL,
);