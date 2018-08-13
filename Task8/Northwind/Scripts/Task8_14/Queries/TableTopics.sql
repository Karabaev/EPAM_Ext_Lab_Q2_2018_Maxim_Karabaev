--CREATE TABLE [dbo].[Topics]
--(
--	[TopicID] INT NOT NULL PRIMARY KEY, 
--    [Caption] VARCHAR(100) NULL, 
--    [CreatorID] INT NULL, 
--    [CreationDate] DATE NULL, 
--    [Link] VARCHAR(1000) NULL, 
--    [SectionID] INT NULL, 
--    CONSTRAINT [FK_Topics_ToTable] FOREIGN KEY ([CreatorID]) REFERENCES [Users]([UserID]), 
--    CONSTRAINT [FK_Topics_ToTable_1] FOREIGN KEY (SectionID) REFERENCES Sections(SectionID)
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Topics' AND xtype = 'U') 
BEGIN 
	CREATE TABLE Topics(
	[TopicID] INT NOT NULL PRIMARY KEY, 
    [Caption] VARCHAR(100) NULL, 
    [CreatorID] INT NULL, 
    [CreationDate] DATE NULL, 
    [Link] VARCHAR(1000) NULL, 
    [SectionID] INT NULL, 
    CONSTRAINT [FK_Topics_ToTable] FOREIGN KEY ([CreatorID]) REFERENCES [Users]([UserID]), 
    CONSTRAINT [FK_Topics_ToTable_1] FOREIGN KEY (SectionID) REFERENCES Sections(SectionID))
END;