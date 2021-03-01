use mmileta
go

ALTER TABLE dbo.users
ADD lastActivityTimeStamp DATETIME NOT NULL DEFAULT GETDATE()