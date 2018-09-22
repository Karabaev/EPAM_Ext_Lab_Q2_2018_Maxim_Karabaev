CREATE PROCEDURE Northwind.IsBoss
	@EmployeeID  int = 0
AS 
	BEGIN
		DECLARE @cur cursor
		DECLARE @reportsToID int
		SET @cur = CURSOR LOCAL FAST_FORWARD READ_ONLY FOR  
		SELECT ReportsTo
		FROM Northwind.Employees
		OPEN @cur
		WHILE 1 = 1
		BEGIN
			FETCH NEXT FROM @cur INTO @reportsToID
			IF @reportsToID = @EmployeeID 
			BEGIN
				RETURN 1
			END
			IF @@FETCH_STATUS <> 0 BREAK
		END
		RETURN 0
	END

