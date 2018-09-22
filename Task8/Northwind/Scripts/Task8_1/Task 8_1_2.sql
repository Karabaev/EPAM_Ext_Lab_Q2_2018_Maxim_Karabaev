SELECT OrderID, 
CASE 
	WHEN ShippedDate IS NULL THEN 'Not shipped'
END ShippedDate
FROM Northwind.Orders
WHERE ShippedDate IS NULL