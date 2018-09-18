CREATE TABLE [dbo].[Users]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Login] VARCHAR(50) NULL, 
    [PasswordHash] VARCHAR(100) NULL, 
    [PublicName] VARCHAR(50) NULL, 
    [UserRole] INT NULL, 
    [IsBanned] BIT NULL, 
    [RegistrationDate] DATE NULL, 
	[Email] VARCHAR(50) NULL,
    CONSTRAINT [FK_Users_ToTable] FOREIGN KEY ([UserRole]) REFERENCES [Roles]([ID]) ON DELETE SET NULL
)
