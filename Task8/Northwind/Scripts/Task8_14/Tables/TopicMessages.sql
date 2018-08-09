CREATE TABLE [dbo].[TopicMessages]
(
	[TMID] INT NOT NULL PRIMARY KEY, 
    [TopicID] INT NULL, 
    [MessageID] INT NULL, 
    CONSTRAINT [FK_TopicMessages_ToTable] FOREIGN KEY (TopicID) REFERENCES Topics(TopicID), 
    CONSTRAINT [FK_TopicMessages_ToTable_1] FOREIGN KEY (MessageID) REFERENCES [Messages](MessageID)
)
