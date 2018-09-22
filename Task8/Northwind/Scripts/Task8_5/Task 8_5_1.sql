SELECT CONVERT(varchar, CONVERT(MONEY,ROUND(SUM(UnitPrice-Discount*UnitPrice),2)), 1) as Totals
FROM Northwind.[Order Details]
--В принципе результат выводит, но немного не выполняет требования задания