SELECT (
	SELECT LastName + ' ' + FirstName as [Employee Name] 
	FROM Northwind.Employees
	WHERE EmployeeID = Northwind.Orders.EmployeeID) as Seller,

   (SELECT ContactName
	FROM Northwind.Customers
	WHERE CustomerID = Northwind.Orders.CustomerID) as Customer,

	COUNT(*) as Amount
FROM Northwind.Orders
WHERE YEAR(OrderDate) = 1998

GROUP By EmployeeID, CustomerID
ORDER BY Seller, Customer, Amount DESC

--Не доделан