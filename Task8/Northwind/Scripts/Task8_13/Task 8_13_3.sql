CREATE PROCEDURE Northwind.SubordinationInfo
	@EmployeeID  int = 0
AS 
	BEGIN
	DECLARE @cur CURSOR
	DECLARE @id int, @name varchar(256), @reports varbinary(MAX), @level int
	SET @cur = CURSOR LOCAL FAST_FORWARD READ_ONLY FOR
		WITH EmployeeNumber AS
		(
			SELECT *, ROW_NUMBER() OVER(PARTITION BY ReportsTo ORDER BY EmployeeID) AS Number
			FROM Employees
		),
		EmpsPath
		AS
		(
			SELECT EmployeeID, (LastName + ' ' + Firstname) AS Name, 0 AS LevelUser, CAST(0x AS varbinary(MAX)) AS SortPath
			FROM Employees
			WHERE EmployeeID = @EmployeeID

			UNION ALL

			SELECT C.EmployeeID, (C.LastName + ' ' + C.Firstname) as Name, P.LevelUser + 1, P.SortPath + CAST(Number AS binary(2))
			FROM EmpsPath AS P
			JOIN EmployeeNumber AS C
			ON C.ReportsTo = P.EmployeeID
		)
		SELECT EmployeeID, Name, LevelUser
		FROM EmpsPath
		ORDER BY SortPath
		
		OPEN @cur
		FETCH NEXT FROM @cur INTO @id, @name, @level
		PRINT REPLICATE('	', @level) + CAST(@id as varchar) + ' ' + @name
		WHILE 1 = 1
		BEGIN
			FETCH NEXT FROM @cur INTO @id, @name, @level
			IF @@FETCH_STATUS <> 0 break
			PRINT REPLICATE('	', @level) + CAST(@id as varchar) + ' ' + @name
		END
		CLOSE @cur
		DEALLOCATE @cur
	END


--	USE [Task8]
--GO
--/****** Object:  StoredProcedure [Northwind].[SubordinationInfo]    Script Date: 11.08.2018 17:40:25 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--ALTER PROCEDURE [Northwind].[SubordinationInfo]
--	@EmployeeID  int = 2
--AS 
--	BEGIN
--	DECLARE @LIST CURSOR
--	DECLARE @id int, @last varchar(50), @reports varbinary(MAX), @level int, @spaces varchar(50), @iterator int
--	SET @LIST = CURSOR LOCAL FAST_FORWARD READ_ONLY FOR
--		WITH EmpsRN AS
--		(
--			SELECT *, ROW_NUMBER() OVER(PARTITION BY ReportsTo ORDER BY EmployeeID) AS N
--			FROM Employees
--		),
--		EmpsPath
--		AS
--		(
--			SELECT EmployeeID, (LastName + ' ' + Firstname) AS Name, 0 AS LevelUser, CAST(0x AS varbinary(MAX)) AS SortPath
--			FROM Employees
--			WHERE EmployeeID = @EmployeeID

--			UNION ALL

--			SELECT C.EmployeeID, (C.LastName + ' ' + C.Firstname) as Name, P.LevelUser + 1, P.SortPath + CAST(n AS binary(2))
--			FROM EmpsPath AS P
--			JOIN EmpsRN AS C
--			ON C.ReportsTo = P.EmployeeID
--		)
--		SELECT EmployeeID, Name, LevelUser
--		FROM EmpsPath
--		ORDER BY SortPath
		
--		OPEN @LIST
--		SET @spaces = ''
--		FETCH NEXT FROM @LIST INTO @id, @last, @level
--		PRINT @spaces + CAST(@id as varchar) + ' ' + @last
--		WHILE 1 = 1
--		BEGIN
--			FETCH NEXT FROM @LIST INTO @id, @last, @level
--			IF @@FETCH_STATUS <> 0 break
--			PRINT REPLICATE('	', @level) + CAST(@id as varchar) + ' ' + @last
--		END
--		CLOSE @LIST
--		DEALLOCATE @LIST
--	END