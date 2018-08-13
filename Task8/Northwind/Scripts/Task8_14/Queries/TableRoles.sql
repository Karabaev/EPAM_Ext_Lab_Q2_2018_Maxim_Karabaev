﻿--CREATE TABLE [dbo].[Roles]
--(
--	[RoleID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
--    [Name] VARCHAR(50) NULL
--)

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Roles' AND xtype = 'U') 
BEGIN 
	CREATE TABLE Roles(
		RoleID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
		[Name] VARCHAR(50) NULL)
END;