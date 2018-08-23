CREATE TABLE [dbo].[RolePermissions]
(
	[PermissionID] INT  NULL,
	[RoleID] INT NULL, 
    CONSTRAINT [FK_RolePermissions_ToTable] FOREIGN KEY ([PermissionID]) REFERENCES [Permissions](ID),
	CONSTRAINT [FK_RolePermissions_ToTable_1] FOREIGN KEY ([RoleID]) REFERENCES [Roles](ID)
)
