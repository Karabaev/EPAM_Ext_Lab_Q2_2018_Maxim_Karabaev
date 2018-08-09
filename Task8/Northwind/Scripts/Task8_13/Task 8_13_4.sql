CREATE PROCEDURE Northwind.IsBoss
	@EmployeeID  int = 0
AS 
	BEGIN
		DECLARE @cur cursor
		DECLARE @reportsToID int
		DECLARE @returnValue bit
		SET @cur = CURSOR LOCAL FAST_FORWARD READ_ONLY FOR  
		SELECT ReportsTo
		FROM Northwind.Employees
		OPEN @cur
		SET @returnValue = 0
		WHILE 1 = 1
		BEGIN
			FETCH NEXT FROM @cur INTO @reportsToID
			IF @reportsToID = @EmployeeID 
			BEGIN
				SET @returnValue = 1
				BREAK
			END
			IF @@FETCH_STATUS <> 0 BREAK
		END
	END

