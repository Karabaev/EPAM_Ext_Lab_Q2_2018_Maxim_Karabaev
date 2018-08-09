CREATE PROCEDURE Northwind.SubordinationInfo
	@EmployeeID  int = 0
AS 
	BEGIN
	DECLARE @LIST CURSOR
	DECLARE @id int, @last varchar(50), @reports int, @level int, @spaces varchar(50), @iterator int
	SET @LIST = CURSOR LOCAL FAST_FORWARD READ_ONLY FOR
		WITH TestCTE (EmployeeID, Name, ReportsTo, LevelUser)	
		AS
		(
			SELECT EmployeeID, (LastName + ' ' + Firstname) as Name, ReportsTo, 0 as LevelUser
			FROM Northwind.Employees
			WHERE EmployeeID = @EmployeeID
			UNION ALL
			SELECT Emp.EmployeeID, (LastName + ' ' + Firstname) as Name, Emp.ReportsTo, Cte.LevelUser + 1
			FROM Northwind.Employees as Emp
			JOIN TestCTE as Cte ON Emp.ReportsTo = Cte.EmployeeID
		)
		SELECT EmployeeID, Name, ReportsTo, LevelUser FROM TestCTE
		OPEN @LIST
		SET @spaces = ''
		FETCH NEXT FROM @LIST INTO @id, @last, @reports, @level
		PRINT @spaces + CAST(@id as varchar) + ' ' + @last
		WHILE 1 = 1
		BEGIN
			FETCH NEXT FROM @LIST INTO @id, @last, @reports, @level
			IF @@FETCH_STATUS <> 0 break
			SET @iterator = 0
			SET @spaces = ''
			WHILE @iterator <> @level
			BEGIN
				SET @spaces = @spaces + '       '
				SET @iterator = @iterator + 1
			END
			PRINT @spaces + CAST(@id as varchar) + ' ' + @last
		END
		CLOSE @LIST
		DEALLOCATE @LIST
	END

