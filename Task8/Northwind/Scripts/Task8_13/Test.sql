SELECT OrderID, ROUND(SUM(UnitPrice - (UnitPrice * Discount)), 2) as [Sum]
												FROM Northwind.[Order Details]
												GROUP BY OrderID

--SELECT (FirstName + ' ' + LastName) as Seller, (SELECT CAST(OrderID as varchar) + ' ' + CAST(ROUND(SUM(UnitPrice - (UnitPrice * Discount)), 2) as varchar ) as [Data]
--												FROM Northwind.[Order Details]
--												GROUP BY OrderID)
--FROM Northwind.Employees


--SELECT