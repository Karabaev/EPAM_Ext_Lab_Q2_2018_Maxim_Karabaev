SELECT COUNT(*) as Total, YEAR(OrderDate) as [Year]
FROM Northwind.Orders
GROUP By YEAR(OrderDate)

--SELECT COUNT(*) as Total
--FROM Northwind.Orders