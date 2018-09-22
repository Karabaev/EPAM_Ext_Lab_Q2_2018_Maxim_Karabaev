--CREATE TABLE [dbo].[ExtraContents]
--(
--	[ExtraContentID] INT NOT NULL PRIMARY KEY, 
--    [ContentPath] VARCHAR(MAX) NULL
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'ExtraContents' AND xtype = 'U') 
BEGIN 
	CREATE TABLE ExtraContents(
	[ExtraContentID] INT NOT NULL PRIMARY KEY, 
    [ContentPath] VARCHAR(MAX) NULL)
END;
