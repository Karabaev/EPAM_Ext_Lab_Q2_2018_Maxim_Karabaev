SELECT COUNT(	CASE 
					WHEN ShippedDate IS NULL THEN 0 
				END) as [Count not shipped]
FROM Northwind.Orders