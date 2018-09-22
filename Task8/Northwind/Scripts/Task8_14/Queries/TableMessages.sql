--CREATE TABLE [dbo].[Messages]
--(
--	[MessageID] INT NOT NULL PRIMARY KEY, 
--    [CreatorID] INT NULL, 
--    [CreationDate] DATE NULL, 
--    [Content] TEXT NULL, 
--    [ExtraContentID] INT NULL, 
--    [TopicID] INT NULL, 
--    CONSTRAINT [FK_Messages_ToTable] FOREIGN KEY (CreatorID) REFERENCES Users(UserID), 
--    CONSTRAINT [FK_Messages_ToTable_1] FOREIGN KEY (ExtraContentID) REFERENCES ExtraContents(ExtraContentID), 
--    CONSTRAINT [FK_Messages_ToTable_2] FOREIGN KEY (TopicID) REFERENCES Topics(TopicID)
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Messages' AND xtype = 'U') 
BEGIN 
	CREATE TABLE [Messages](
	[MessageID] INT NOT NULL PRIMARY KEY, 
    [CreatorID] INT NULL, 
    [CreationDate] DATE NULL, 
    [Content] TEXT NULL, 
    [ExtraContentID] INT NULL, 
    [TopicID] INT NULL, 
    CONSTRAINT [FK_Messages_ToTable] FOREIGN KEY (CreatorID) REFERENCES Users(UserID), 
    CONSTRAINT [FK_Messages_ToTable_1] FOREIGN KEY (ExtraContentID) REFERENCES ExtraContents(ExtraContentID), 
    CONSTRAINT [FK_Messages_ToTable_2] FOREIGN KEY (TopicID) REFERENCES Topics(TopicID))
END;
