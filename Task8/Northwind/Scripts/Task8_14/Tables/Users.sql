CREATE TABLE [dbo].[Users]
(
	[UserID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Login] VARCHAR(50) NULL, 
    [Password] VARCHAR(100) NULL, 
    [PublicName] VARCHAR(50) NULL, 
    [UserRoleID] INT NULL, 
    [IsBanned] BIT NULL, 
    [RegistrationDate] DATE NULL, 
    CONSTRAINT [FK_Users_ToTable] FOREIGN KEY ([UserRoleID]) REFERENCES [Roles]([RoleID])
)
