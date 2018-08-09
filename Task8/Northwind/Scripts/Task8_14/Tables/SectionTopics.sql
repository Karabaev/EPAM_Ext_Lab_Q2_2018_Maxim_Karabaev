CREATE TABLE [dbo].[SectionTopics]
(
	[STID] INT NOT NULL PRIMARY KEY, 
    [SectionID] INT NULL, 
    [TopicID] INT NULL, 
    CONSTRAINT [FK_SectionTopics_ToTable] FOREIGN KEY ([SectionID]) REFERENCES Sections(SectionID), 
    CONSTRAINT [FK_SectionTopics_ToTable_1] FOREIGN KEY (TopicID) REFERENCES Topics(TopicID)
)
