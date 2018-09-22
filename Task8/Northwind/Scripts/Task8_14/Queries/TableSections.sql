--CREATE TABLE [dbo].[Sections]
--(
--	[SectionID] INT NOT NULL PRIMARY KEY, 
--    [Name] VARCHAR(50) NULL, 
--    [Description] VARCHAR(100) NULL, 
--    [Link] VARCHAR(1000) NULL
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Sections' AND xtype = 'U') 
BEGIN 
	CREATE TABLE Sections(
	[SectionID] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(100) NULL, 
    [Link] VARCHAR(1000) NULL)
END;