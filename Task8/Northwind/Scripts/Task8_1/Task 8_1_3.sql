SELECT OrderID as 'Order Number',
CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped' ELSE CONVERT(nchar, ShippedDate) 
END as 'Shipped date'
FROM Northwind.Orders
WHERE ShippedDate >'19980506' OR ShippedDate IS NULL