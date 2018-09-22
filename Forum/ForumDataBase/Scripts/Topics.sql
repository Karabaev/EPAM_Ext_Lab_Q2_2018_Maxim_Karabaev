CREATE TABLE [dbo].[Topics]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Caption] VARCHAR(100) NULL, 
    [AuthorID] INT NULL, 
    [CreationDate] DATE NULL, 
    [Link] VARCHAR(1000) NULL, 
    [SectionID] INT NULL, 
    CONSTRAINT [FK_Topics_ToTable] FOREIGN KEY ([AuthorID]) REFERENCES Users([ID]), 
    CONSTRAINT [FK_Topics_ToTable_1] FOREIGN KEY (SectionID) REFERENCES Sections(ID)
)
