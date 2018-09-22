SELECT emp.FirstName + ' ' + emp.LastName, ter.TerritoryDescription
FROM Northwind.Employees as emp INNER JOIN Northwind.EmployeeTerritories as empTer ON emp.EmployeeID = empTer.EmployeeID
INNER JOIN Northwind.Territories as ter ON ter.TerritoryID = empTer.TerritoryID 