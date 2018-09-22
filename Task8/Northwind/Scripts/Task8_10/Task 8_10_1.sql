SELECT EmployeeID, (LastName + ' ' + FirstName) as Seller
FROM Northwind.Employees as emp
WHERE (	SELECT COUNT(OrderID)
		FROM Northwind.Orders 
		WHERE emp.EmployeeID = EmployeeID) > 150