-- disable referential integrity
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 
GO 

EXEC sp_MSForEachTable 'DELETE FROM ?' 
GO 

-- enable referential integrity again 
EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL' 
GO

dbcc checkident(users, reseed, 0)
dbcc checkident(memories, reseed, 0)
dbcc checkident(groups, reseed, 0)
dbcc checkident(usersInAGroup, reseed, 0)
dbcc checkident(usersTaggedOnMemory, reseed, 0)
dbcc checkident(userRelationshipStatus, reseed, 0)