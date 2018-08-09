SELECT ROUND(SUM(UnitPrice - UnitPrice * Discount), 2) as Totals
FROM Northwind.[Order Details]
--В принципе результат выводит, но немного не выполняет требования задания