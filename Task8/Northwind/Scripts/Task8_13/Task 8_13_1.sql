CREATE PROCEDURE Northwind.GreatestOrders
	@Year int
AS BEGIN
	SELECT LastName+' '+FirstName AS 'Name', Price
	FROM (SELECT EmployeeID,MAX(UnitPrice-UnitPrice*Discount) AS Price FROM Northwind.[Order Details] INNER JOIN Northwind.Orders 
	ON [Order Details].OrderID = Orders.OrderID
	WHERE YEAR(OrderDate) = @Year
	GROUP BY EmployeeID
	) AS D JOIN Northwind.Employees ON Employees.EmployeeID = D.EmployeeID;
END