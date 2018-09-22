CREATE TABLE [dbo].[Sections]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(100) NULL, 
    [Link] VARCHAR(1000) NULL
)