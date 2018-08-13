--CREATE TABLE [dbo].[RolePermissions]
--(
--	[RPID] INT NOT NULL PRIMARY KEY, 
--    [PermissionID] INT NULL, 
--    [RoleID] INT NULL, 
--    CONSTRAINT [FK_RolePermissions_ToTable] FOREIGN KEY (PermissionID) REFERENCES [Permissions]([PermissionID]), 
--    CONSTRAINT [FK_RolePermissions_ToTable_1] FOREIGN KEY ([RoleID]) REFERENCES [Roles]([RoleID]) 
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'RolePermissions' AND xtype = 'U') 
BEGIN 
	CREATE TABLE RolePermissions(
		[RPID] INT NOT NULL PRIMARY KEY, 
		[PermissionID] INT NULL, 
		[RoleID] INT NULL, 
		CONSTRAINT [FK_RolePermissions_ToTable] FOREIGN KEY (PermissionID) REFERENCES [Permissions]([PermissionID]), 
		CONSTRAINT [FK_RolePermissions_ToTable_1] FOREIGN KEY ([RoleID]) REFERENCES [Roles]([RoleID]))
END;