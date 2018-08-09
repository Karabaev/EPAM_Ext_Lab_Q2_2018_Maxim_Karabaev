CREATE TABLE [dbo].[Topics]
(
	[TopicID] INT NOT NULL PRIMARY KEY, 
    [Caption] VARCHAR(100) NULL, 
    [CreatorID] INT NULL, 
    [CreationDate] DATE NULL, 
    [Link] VARCHAR(1000) NULL, 
    CONSTRAINT [FK_Topics_ToTable] FOREIGN KEY ([CreatorID]) REFERENCES [Users]([UserID])
)
