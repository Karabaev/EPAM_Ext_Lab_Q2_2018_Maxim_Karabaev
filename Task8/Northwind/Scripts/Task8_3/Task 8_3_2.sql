
SELECT CustomerID, Country
FROM Northwind.Customers
WHERE Country BETWEEN 'b%' AND 'h%'
ORDER BY Country