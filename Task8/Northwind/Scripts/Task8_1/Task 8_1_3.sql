SELECT OrderID as 'Order Number',
CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped' ELSE CONVERT(nchar, ShippedDate) 
END as 'Shipped date'
FROM Northwind.Orders
WHERE ShippedDate >'19960506' OR ShippedDate IS NULL
--Выводится ли дата в формате по умолчанию?