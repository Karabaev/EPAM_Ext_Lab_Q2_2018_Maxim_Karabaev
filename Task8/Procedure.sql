--13.1	Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. 
--В результатах не может быть несколько заказов одного продавца, должен быть только один и самый крупный. В результатах 
--запроса должны быть выведены следующие колонки: колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio),
--номер заказа и его стоимость. В запросе надо учитывать Discount при продаже товаров. Процедуре передается год, за который надо
--сделать отчет, и количество возвращаемых записей. Результаты запроса должны быть упорядочены по убыванию суммы заказа. Процедура
--должна быть реализована с использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. Название функции соответственно
--GreatestOrders. Необходимо продемонстрировать использование этих процедур. Также помимо демонстрации вызовов процедур в
--скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ проверочный запрос для тестирования правильности работы 
--процедуры GreatestOrders. Проверочный запрос должен выводить в удобном для сравнения с результатами работы процедур виде 
--для определенного продавца для всех его заказов за определенный указанный год в результатах следующие колонки: имя продавца, 
--номер заказа, сумму заказа. Проверочный запрос не должен повторять запрос, написанный в процедуре, - он должен выполнять только 
--то, что описано в требованиях по нему.
--ВСЕ ЗАПРОСЫ ПО ВЫЗОВУ ПРОЦЕДУР ДОЛЖНЫ БЫТЬ НАПИСАНЫ В ФАЙЛЕ Query.sql – см. пояснение ниже в разделе «Требования к оформлению».

USE Northwind

IF EXISTS(
        SELECT type_desc, type
        FROM sys.procedures 
        WHERE NAME = 'GreatestOrders' AND type = 'P')
	 BEGIN
		DROP PROCEDURE Northwind.GreatestOrders
	 END;
GO
	CREATE PROCEDURE Northwind.GreatestOrders
	@Year int
AS 
BEGIN
	SELECT LastName+' '+FirstName AS 'Name', Price
	FROM (SELECT EmployeeID,MAX(UnitPrice-UnitPrice*Discount) AS Price FROM Northwind.[Order Details] INNER JOIN Northwind.Orders 
	ON [Order Details].OrderID = Orders.OrderID
	WHERE YEAR(OrderDate) = @Year
	GROUP BY EmployeeID
	) AS D JOIN Northwind.Employees ON Employees.EmployeeID = D.EmployeeID;
END;

--13.2	Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях 
--(разница между OrderDate и ShippedDate).  В результатах должны быть возвращены заказы, срок которых превышает переданное
--значение или еще недоставленные заказы. Значению по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff.
--Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate 
--и OrderDate), SpecifiedDelay (переданное в процедуру значение).  Необходимо продемонстрировать использование этой процедуры.
GO
IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures 
        WHERE NAME = 'ShippedOrdersDiff' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE Northwind.ShippedOrdersDiff
END
GO
CREATE PROCEDURE Northwind.ShippedOrdersDiff
	@SpecifiedDelay int = 35
AS 
BEGIN
	SELECT OrderID, OrderDate, ShippedDate, DATEDIFF(dd, OrderDate, ShippedDate) as ShippedDelay, @SpecifiedDelay as SpecifiedDelay
	FROM Northwind.Orders
	WHERE DATEDIFF(dd, OrderDate, ShippedDate) >  @SpecifiedDelay  OR ShippedDate IS NULL
END;

--13.3	Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, 
--так и подчиненных его подчиненных. В качестве входного параметра функции используется EmployeeID. Необходимо 
--распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения.
--Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры SubordinationInfo. 
--В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный
--Microsoft для решения подобного типа задач. Продемонстрировать использование процедуры.
GO
IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures 
        WHERE NAME = 'SubordinationInfo' AND type = 'P')
BEGIN
	DROP PROCEDURE Northwind.SubordinationInfo
END
GO
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
	END;

--13.4	 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. 
--В качестве входного параметра функции используется EmployeeID. Название функции IsBoss. Продемонстрировать 
--использование функции для всех продавцов из таблицы Employees.
GO
IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures 
        WHERE NAME = 'IsBoss' AND type = 'P')
BEGIN
	DROP PROCEDURE Northwind.IsBoss -- просили функцию, а не хранимку
END
GO
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
	END;

