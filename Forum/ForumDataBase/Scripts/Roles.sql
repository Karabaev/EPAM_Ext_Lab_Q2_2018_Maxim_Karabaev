﻿CREATE TABLE [dbo].[Roles]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [AccessLevel] INT NULL
)
