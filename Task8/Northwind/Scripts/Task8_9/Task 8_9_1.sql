SELECT CompanyName
FROM Northwind.Suppliers
WHERE SupplierID IN(SELECT SupplierID 
					FROM Northwind.Products 
					WHERE UnitsInStock = 0)
-- Оператор IN нельзя заменить на =, потому что подзапросявляется многострочным.