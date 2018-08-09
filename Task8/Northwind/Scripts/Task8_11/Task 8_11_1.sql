SELECT ContactName
FROM Northwind.Customers  as cust
WHERE NOT EXISTS(	SELECT CustomerID
		FROM Northwind.Orders 
		WHERE cust.CustomerID = CustomerID)