USE mmileta
GO

IF NOT EXISTS(SELECT * FROM [dbo].users)
INSERT INTO [dbo].users
VALUES
('User FN 1', 'User LN 1', 'email1@email.com' , 'password', GETDATE(),GETDATE(), 0),
('User FN 2', 'User LN 2', 'email2@email.com' , 'password', GETDATE(),GETDATE(), 0),
('User FN 3', 'User LN 3', 'email3@email.com' , 'password', GETDATE(),GETDATE(), 0),
('User FN 4', 'User LN 4', 'email4@email.com' , 'password', GETDATE(),GETDATE(), 0),
('User FN 5', 'User LN 5', 'email5@email.com' , 'password', GETDATE(),GETDATE(), 0);

IF NOT EXISTS(SELECT * FROM [dbo].memories)
INSERT INTO [dbo].memories
VALUES
('Memory title 1', 'Memory Description 1', 1, GETDATE(), 1),
('Memory title 2', 'Memory Description 2', 0, GETDATE(), 1),
('Memory title 3', 'Memory Description 3', 0, GETDATE(), 2),
('Memory title 4', 'Memory Description 4', 1, GETDATE(), 2),
('Memory title 5', 'Memory Description 5', 0, GETDATE(), 2);

IF NOT EXISTS(SELECT * FROM [dbo].usersTaggedOnMemory)
INSERT INTO [dbo].usersTaggedOnMemory
VALUES 
(3, 1),
(3, 3),
(3, 4);

IF NOT EXISTS(SELECT * FROM [dbo].userRelationshipStatus)
INSERT INTO [dbo].userRelationshipStatus
VALUES
('FriendRequestSent'),
('FriendRequestRecieved'),
('Friends'),
('MaybeDenied');

IF NOT EXISTS(SELECT * FROM [dbo].groups)
INSERT INTO [dbo].groups
VALUES
('First group'),
('Second group'),
('Third group');


IF NOT EXISTS(SELECT * FROM [dbo].messages)
INSERT INTO [dbo].messages
VALUES
(1, 2, null, 'message is tbhis hehehe', GETDATE()),
(1, 2, null, 'message is tbhis hehehe', GETDATE()),
(2, 1, null, 'Responding', GETDATE()),
(2, 3, null, 'message is tbhis hehehe', GETDATE()),
(1, null, 1, 'message to the group heh', GETDATE()),
(1, null, 1, 'message to the group heh2', GETDATE()),
(1, null, 1, 'message to the group heh3', GETDATE()),
(1, null, 1, 'message to the group heh4', GETDATE()),
(1, null, 1, 'message to the group heh5', GETDATE()),
(2, null, 1, 'message to the group heh', GETDATE()),
(3, null, 1, 'message to the group heh', GETDATE());

IF NOT EXISTS(SELECT * FROM [dbo].usersInAGroup)
INSERT INTO [dbo].usersInAGroup
VALUES
(1,1),
(2,1),
(3,1),
(4,1);


IF NOT EXISTS(SELECT * FROM [dbo].usersInAGroup)
INSERT INTO [dbo].usersInAGroup
VALUES
(1,1),
(2,1),
(3,1),
(4,1);

IF NOT EXISTS(SELECT * FROM [dbo].notificationType)
INSERT INTO [dbo].notificationType
VALUES
('Friend Request Sent'),
('Friend Request Recieved'),
('You have been tagged in a memory');

IF NOT EXISTS(SELECT * FROM [dbo].notifications)
INSERT INTO [dbo].notifications
VALUES
(1,2, NULL, 1, GETDATE()),
(1,3, NULL, 2, GETDATE()),
(2,3, NULL, 3, GETDATE()),
(2,1, NULL, 1, GETDATE()),
(3,4, NULL, 2, GETDATE()),
(3,5, NULL, 3, GETDATE());