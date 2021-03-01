use mmileta
go

ALTER TABLE dbo.notifications
ADD isRecieved bit NOT NULL DEFAULT 0