SELECT OrderID, ShippedDate, ShipVia
FROM Northwind.Orders
--WHERE ShippedDate > DATEFROMPARTS(1998, 5, 6) AND ShipVia >= 2
WHERE ShippedDate > '19960506' AND ShipVia >= 2
--По заданию сказано искать после 02.05.1998 но таких записей нет