﻿CREATE PROCEDURE [dbo].[GetAllUsers]
	@Count INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP (@Count) *
	FROM Users
END