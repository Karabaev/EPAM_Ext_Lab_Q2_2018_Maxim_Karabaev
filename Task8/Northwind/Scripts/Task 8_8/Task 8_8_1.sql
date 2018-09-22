SELECT cust.ContactName, COUNT(ord.CustomerID) as [Count]
FROM Northwind.Customers as cust LEFT OUTER JOIN Northwind.Orders as ord ON cust.CustomerID = ord.CustomerID
GROUP BY cust.ContactName
ORDER BY [Count]