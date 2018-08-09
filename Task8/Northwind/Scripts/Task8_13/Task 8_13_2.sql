CREATE PROCEDURE Northwind.ShippedOrdersDiff
	@SpecifiedDelay  int = 35
AS BEGIN
	SELECT OrderID, OrderDate, ShippedDate, DATEDIFF(dd, OrderDate, ShippedDate) as ShippedDelay, @SpecifiedDelay as SpecifiedDelay
	FROM Northwind.Orders
	WHERE DATEDIFF(dd, OrderDate, ShippedDate) >  @SpecifiedDelay  OR ShippedDate IS NULL
	END
