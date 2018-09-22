CREATE TABLE [dbo].[Messages]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [AuthorID] INT NULL, 
    [CreationDate] DATE NULL, 
    [Content] TEXT NULL, 
    [TopicID] INT NULL, 
    CONSTRAINT [FK_Messages_ToTable] FOREIGN KEY ([AuthorID]) REFERENCES Users(ID), 
    CONSTRAINT [FK_Messages_ToTable_1] FOREIGN KEY (TopicID) REFERENCES Topics(ID)
)