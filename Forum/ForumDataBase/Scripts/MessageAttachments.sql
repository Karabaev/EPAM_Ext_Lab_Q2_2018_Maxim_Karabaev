CREATE TABLE [dbo].[MessageAttachments]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [ContentLink] VARCHAR(MAX) NULL, 
    [MessageID] INT NULL, 
    CONSTRAINT [FK_MessageAttachments_ToTable] FOREIGN KEY (MessageID) REFERENCES [Messages](ID)
)
