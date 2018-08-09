SELECT (
	SELECT LastName + ' ' + FirstName as [Employee Name] 
	FROM Northwind.Employees
	WHERE EmployeeID = Northwind.Orders.EmployeeID) as Seller, COUNT(*) as Amount
FROM Northwind.Orders
GROUP By EmployeeID
ORDER BY Amount DESC