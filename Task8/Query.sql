﻿--13.1
USE Northwind
EXEC Northwind.GreatestOrders 1996;

--Проверочный запрос
SELECT [Name], UnitPrice 
FROM	(SELECT (FirstName + LastName) AS [Name], EmployeeID 
		FROM Northwind.Employees) AS Emps LEFT JOIN (SELECT OrderID, OrderDate, EmployeeID 
												FROM Northwind.Orders) AS Ords  ON Emps.EmployeeID = Ords.EmployeeID 
								     LEFT JOIN (SELECT UnitPrice, OrderID 
												FROM Northwind.[Order Details]) AS Ord_dls ON Ords.OrderID = Ord_dls.OrderID 
WHERE YEAR(OrderDate) = 1996 
GROUP BY Name,UnitPrice;

--13.2
EXEC Northwind.ShippedOrdersDiff;

--13.3
EXEC Northwind.SubordinationInfo 2;

--13.4
DECLARE @isBoss int;
EXEC @isBoss = Northwind.IsBoss 2
SELECT 'IsBoss' = @isBoss;

--1.1	Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно и которые 
--доставлены с ShipVia >= 2. Формат указания даты должен быть верным при любых региональных настройках, согласно требованиям статьи 
--“Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing Relational Data Overview”. 
--Этот метод использовать далее для всех заданий. Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia. 
--Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate. 

SELECT OrderID, ShippedDate, ShipVia
FROM Northwind.Orders
WHERE ShippedDate > '19960506' AND ShipVia >= 2 --как ты понимаешь, где у тебя месяц, а где у тебя дата? Нужно сделать запрос независимым от культуры. CONVERT(DATETIME, '19980506', 101)
-- Булево значение «неизвестно» ведет себя также, как «ложь» — строка, на которой предикат принимает значение «неизвестно», 
-- не включается в результат запроса

--1.2	Написать запрос, который выводит только недоставленные заказы из таблицы Orders. В результатах запроса высвечивать для колонки 
--ShippedDate вместо значений NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ. Запрос должен высвечивать только колонки 
--OrderID и ShippedDate.

SELECT OrderID, 
CASE 
	WHEN ShippedDate IS NULL THEN 'Not shipped'
END ShippedDate
FROM Northwind.Orders
WHERE ShippedDate IS NULL

--1.3	Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые еще 
--не доставлены. В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в 
--Shipped Date). В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, для остальных 
--значений высвечивать дату в формате по умолчанию.

SELECT OrderID as 'Order Number',
CASE
	WHEN ShippedDate IS NULL THEN 'Not shipped' ELSE CONVERT(nchar, ShippedDate) 
END as 'Shipped date'
FROM Northwind.Orders
WHERE ShippedDate >'19980506' OR ShippedDate IS NULL--аналогично

--2.1	Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. Запрос сделать с только помощью оператора IN. 
--Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени 
--заказчиков и по месту проживания.

SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country

--2.2	Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора IN. 
--Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса 
--по имени заказчиков.

SELECT ContactName, Country
FROM Northwind.Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName

--2.3	Выбрать из таблицы Customers все страны, в которых проживают заказчики. Страна должна быть упомянута только один раз 
--и список отсортирован по убыванию. Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах 
--запроса. 

SELECT DISTINCT Country
FROM Northwind.Customers
ORDER BY Country DESC

--3.1	Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), где встречаются продукты с 
--количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. Использовать оператор BETWEEN. Запрос 
--должен высвечивать только колонку OrderID.

SELECT DISTINCT OrderID
FROM Northwind.[Order Details]
WHERE Quantity BETWEEN 3 AND 10

--3.2	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. 
--Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен высвечивать только 
--колонки CustomerID и Country и отсортирован по Country.

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE Country BETWEEN 'b%' AND 'h%'
ORDER BY Country

--3.3	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, 
--не используя оператор BETWEEN. С помощью опции “Execution Plan” определить какой запрос предпочтительнее 3.2 или 3.3 – 
--для этого надо ввести в скрипт выполнение текстового Execution Plan-a для двух этих запросов, результаты выполнения 
--Execution Plan надо ввести в скрипт в виде комментария и по их результатам дать ответ на вопрос – по какому параметру 
--было проведено сравнение. Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country
--Execution plan не забыть ввести

--4.1	В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
--Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты,
--которые удовлетворяют этому условию. Подсказка: результаты запроса должны высвечивать 2 строки.

SELECT ProductName
FROM Northwind.Products
WHERE ProductName LIKE 'cho_olade'--запрос некорректный. Должна вывестись ещё одна запись (в формулировке же написано :С)
--Проверить работу скрипта!

--5.1	Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
--Результат округлить до сотых и высветить в стиле 1 для типа данных money.  Скидка (колонка Discount) составляет процент 
--из стоимости для данного товара. Для определения действительной цены на проданный продукт надо вычесть скидку из указанной
--в колонке UnitPrice цены. Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.

SELECT CONVERT(varchar, CONVERT(MONEY,ROUND(SUM(UnitPrice-Discount*UnitPrice),2)), 1) as Totals
FROM Northwind.[Order Details]

--5.2	По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate 
--нет значения даты доставки). Использовать при этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.

SELECT COUNT(CASE 
				WHEN ShippedDate IS NULL THEN 0 
			 END) as [Count not shipped]
FROM Northwind.Orders

--5.3	По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. Использовать функцию
--COUNT и не использовать предложения WHERE и GROUP.

SELECT COUNT(DISTINCT CustomerID) as [Count_of_customers]
FROM Northwind.Orders

--6.1	По таблице Orders найти количество заказов с группировкой по годам. 
--В результатах запроса надо высвечивать две колонки c названиями Year и Total. 
--Написать проверочный запрос, который вычисляет количество всех заказов.

SELECT YEAR(OrderDate) AS 'Year', COUNT(*) AS 'Total'
FROM Northwind.Orders
GROUP BY YEAR(Orders.OrderDate);

--Проверочный запрос
SELECT COUNT(*)AS 'All orders' FROM Northwind.Orders;

--6.2	По таблице Orders найти количество заказов, cделанных каждым продавцом. 
--Заказ для указанного продавца – это любая запись в таблице Orders, 
--где в колонке EmployeeID задано значение для данного продавца. 
--В результатах запроса надо высвечивать колонку с именем продавца 
--(Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
--Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
--Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки 
--‘Seller’ и колонку c количеством заказов высвечивать с названием 'Amount'. 
--Результаты запроса должны быть упорядочены по убыванию количества заказов. 

SELECT Seller, COUNT(OrderID) AS Amount 
FROM Northwind.Orders JOIN (SELECT (FirstName + ' ' + LastName) AS Seller, EmployeeID 
							FROM Northwind.Employees) AS Emps ON Orders.EmployeeID = Emps.EmployeeID
GROUP BY Orders.EmployeeID, Seller
ORDER BY Amount;

--6.3	По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя. 
--Необходимо определить это только для заказов сделанных в 1998 году. 
--В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’), 
--колонку с именем покупателя (название колонки ‘Customer’)  
--и колонку c количеством заказов высвечивать с названием 'Amount'. 
--В запросе необходимо использовать специальный оператор языка T-SQL для работы с выражением GROUP 
--(Этот же оператор поможет выводить строку “ALL” в результатах запроса). 
--Группировки должны быть сделаны по ID продавца и покупателя. Результаты запроса должны быть упорядочены 
--по продавцу, покупателю и по убыванию количества продаж. 
--В результатах должна быть сводная информация по продажам. 

SELECT ISNULL(Seller, 'ALL') AS Seller, ISNULL(ContactName, 'ALL') AS Cust, COUNT(*) AS Amount 
FROM	(SELECT EmployeeID, (FirstName + ' ' + LastName) AS Seller 
		FROM Northwind.Employees) AS Emps LEFT JOIN (SELECT EmployeeID,CustomerID 
													FROM Northwind.Orders 
													WHERE YEAR(OrderDate) = 1998) AS Ords 
										ON Emps.EmployeeID = Ords.EmployeeID LEFT JOIN (SELECT ContactName,CustomerID 
																						FROM Northwind.Customers) AS Custs 
																			 ON Custs.CustomerID = Ords.CustomerID 
GROUP BY CUBE(Seller, ContactName) 
ORDER BY ContactName, Seller;

--6.4	Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один или несколько 
--продавцов или только один или несколько покупателей, то информация о таких покупателя и продавцах не должна попадать 
--в результирующий набор. Не использовать конструкцию JOIN. В результатах запроса необходимо вывести следующие заголовки для 
--результатов запроса: ‘Person’, ‘Type’ (здесь надо выводить строку ‘Customer’ или  ‘Seller’ в завимости от типа записи), 
--‘City’. Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.

DECLARE @type varchar(64),@type2 varchar(64)
SET @type ='Seller'
SET @type2 ='Customer'

SELECT Custs.Person, Custs.[Type], Custs.City 
FROM (SELECT @type2 AS 'Type',ContactName AS Person, City, CustomerID AS CustID 
	 FROM Northwind.Customers) AS Custs,
	 (SELECT @type AS 'Type', (FirstName + ' ' + LastName) AS Person, City, CONVERT(varchar(10), EmployeeID) AS EmpID 
	 FROM Northwind.Employees) AS Emps
	 WHERE Custs.City = Emps.City 
UNION
SELECT Emps.Person, Emps.[Type], Emps.City 
FROM (SELECT @type2 AS 'Type',ContactName AS Person, City, CustomerID AS CustID 
	 FROM Northwind.Customers) AS Custs,
	 (SELECT @type AS 'Type', (FirstName + ' ' + LastName) AS Person, City, CONVERT(varchar(10), EmployeeID) AS EmpID 
	 FROM Northwind.Employees) AS Emps
WHERE Custs.City = Emps.City
ORDER BY City;  

--6.5	Найти всех покупателей, которые живут в одном городе. 
--В запросе использовать соединение таблицы Customers c собой - самосоединение. 
--Высветить колонки CustomerID и City. Запрос не должен высвечивать дублируемые записи. 
--Для проверки написать запрос, который высвечивает города, 
--которые встречаются более одного раза в таблице Customers. 
--Это позволит проверить правильность запроса.

SELECT CustomerID,City 
FROM Northwind.Customers AS Custs
WHERE Custs.City IN (SELECT City
					FROM Northwind.Customers
					GROUP BY City
					HAVING COUNT(*) > 1)
GROUP BY Custs.City, Custs.CustomerID;

 --6.6	По таблице Employees найти для каждого продавца его руководителя, 
--т.е. кому он делает репорты. Высветить колонки с именами 'User Name' (LastName) и 'Boss'. 
--В колонках должны быть высвечены имена из колонки LastName. Высвечены ли все продавцы в этом запросе?

SELECT [User Name], Boss 
FROM (SELECT LastName AS 'User Name', ReportsTo 
	 FROM Northwind.Employees) AS Sellers JOIN (SELECT LastName AS Boss, EmployeeID 
											   FROM Northwind.Employees) AS Bosses
										  ON Sellers.ReportsTo = Bosses.EmployeeID;
--У одного продавца нет руководителя, потому он не выводится.

--7.1	Определить продавцов, которые обслуживают регион 'Western' (таблица Region). Результаты запроса должны высвечивать 
--два поля: 'LastName' продавца и название обслуживаемой территории ('TerritoryDescription' из таблицы Territories). Запрос 
--должен использовать JOIN в предложении FROM. Для определения связей между таблицами Employees и Territories надо использовать
--графические диаграммы для базы Northwind.

SELECT emp.FirstName + ' ' + emp.LastName, ter.TerritoryDescription
FROM Northwind.Employees as emp INNER JOIN Northwind.EmployeeTerritories as empTer ON emp.EmployeeID = empTer.EmployeeID
INNER JOIN Northwind.Territories as ter ON ter.TerritoryID = empTer.TerritoryID 

--8.1	Высветить в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество
--их заказов из таблицы Orders. Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны
--быть выведены в результатах запроса. Упорядочить результаты запроса по возрастанию количества заказов.

SELECT cust.ContactName, COUNT(ord.CustomerID) as [Count]
FROM Northwind.Customers as cust LEFT OUTER JOIN Northwind.Orders as ord ON cust.CustomerID = ord.CustomerID
GROUP BY cust.ContactName
ORDER BY [Count]

--9.1	Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых нет хотя бы одного продукта 
--на складе (UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием
--оператора IN. Можно ли использовать вместо оператора IN оператор '=' ?

SELECT CompanyName
FROM Northwind.Suppliers
WHERE SupplierID IN(SELECT SupplierID 
					FROM Northwind.Products 
					WHERE UnitsInStock = 0)
-- Оператор IN нельзя заменить на =, потому что подзапрос является многострочным.

--10.1	Высветить всех продавцов, которые имеют более 150 заказов. Использовать вложенный коррелированный SELECT.

SELECT EmployeeID, (LastName + ' ' + FirstName) as Seller
FROM Northwind.Employees as emp
WHERE (	SELECT COUNT(OrderID)
		FROM Northwind.Orders 
		WHERE emp.EmployeeID = EmployeeID) > 150

--11.1	Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
--Использовать коррелированный SELECT и оператор EXISTS.

SELECT ContactName
FROM Northwind.Customers  as cust
WHERE NOT EXISTS(SELECT CustomerID
				FROM Northwind.Orders 
				WHERE cust.CustomerID = CustomerID)

--12.1	Для формирования алфавитного указателя Employees высветить из таблицы 
--Employees список только тех букв алфавита, с которых начинаются фамилии Employees
--(колонка LastName ) из этой таблицы. Алфавитный список должен быть отсортирован по возрастанию.

SELECT DISTINCT LEFT(LastName, 1) as Word
FROM Northwind.Employees
ORDER BY Word

--14 На основе диаграммы классов проработайте архитектуру базы данных вашего финального проекта.
--Напишите скрипт создания сущностей пользователя, ролей и зависимых сущностей (достаточных для выполнения CRUD операций над пользователями и выдачи им определенных ролей)
--*Напишите скрипт создания оставшихся сущностей вашей диаграммы. 

----Создание базы
--IF NOT EXISTS(SELECT * FROM sys.databases 
--          WHERE name='Forum')
--BEGIN
--	CREATE DATABASE Forum
--	USE Forum
--END;

----Таблица ролей
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Roles' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE Roles(
--		RoleID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
--		[Name] VARCHAR(50) NULL)
--END;

----Таблица прав
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Permissions' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE Permissions(
--		[PermissionID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
--		[Name] VARCHAR(50) NULL, 
--		[Description] VARCHAR(100) NULL)
--END;

----Таблица наличия прав к ролях
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'RolePermissions' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE RolePermissions(
--		[RPID] INT NOT NULL PRIMARY KEY, 
--		[PermissionID] INT NULL, 
--		[RoleID] INT NULL, 
--		CONSTRAINT [FK_RolePermissions_ToTable] FOREIGN KEY (PermissionID) REFERENCES [Permissions]([PermissionID]), 
--		CONSTRAINT [FK_RolePermissions_ToTable_1] FOREIGN KEY ([RoleID]) REFERENCES [Roles]([RoleID]))
--END;

----Таблица пользователей
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Users' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE Users(
--		[UserID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
--		[Login] VARCHAR(50) NULL, 
--		[Password] VARCHAR(100) NULL, 
--		[PublicName] VARCHAR(50) NULL, 
--		[UserRoleID] INT NULL, 
--		[IsBanned] BIT NULL, 
--		[RegistrationDate] DATE NULL, 
--		CONSTRAINT [FK_Users_ToTable] FOREIGN KEY ([UserRoleID]) REFERENCES [Roles]([RoleID]))
--END;

----Таблица разделов форума
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Sections' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE Sections(
--	[SectionID] INT NOT NULL PRIMARY KEY, 
--    [Name] VARCHAR(50) NULL, 
--    [Description] VARCHAR(100) NULL, 
--    [Link] VARCHAR(1000) NULL)
--END;

----Таблица тем
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Topics' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE Topics(
--	[TopicID] INT NOT NULL PRIMARY KEY, 
--    [Caption] VARCHAR(100) NULL, 
--    [CreatorID] INT NULL, 
--    [CreationDate] DATE NULL, 
--    [Link] VARCHAR(1000) NULL, 
--    [SectionID] INT NULL, 
--    CONSTRAINT [FK_Topics_ToTable] FOREIGN KEY ([CreatorID]) REFERENCES [Users]([UserID]), 
--    CONSTRAINT [FK_Topics_ToTable_1] FOREIGN KEY (SectionID) REFERENCES Sections(SectionID))
--END;

----Таблица сообщений
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Messages' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE [Messages](
--	[MessageID] INT NOT NULL PRIMARY KEY, 
--    [CreatorID] INT NULL, 
--    [CreationDate] DATE NULL, 
--    [Content] TEXT NULL, 
--    [ExtraContentID] INT NULL, 
--    [TopicID] INT NULL, 
--    CONSTRAINT [FK_Messages_ToTable] FOREIGN KEY (CreatorID) REFERENCES Users(UserID), 
--    CONSTRAINT [FK_Messages_ToTable_1] FOREIGN KEY (ExtraContentID) REFERENCES ExtraContents(ExtraContentID), 
--    CONSTRAINT [FK_Messages_ToTable_2] FOREIGN KEY (TopicID) REFERENCES Topics(TopicID))
--END;

----Таблица доп. вложений в сообщениях
--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'ExtraContents' AND xtype = 'U') 
--BEGIN 
--	CREATE TABLE ExtraContents(
--	[ExtraContentID] INT NOT NULL PRIMARY KEY, 
--    [ContentPath] VARCHAR(MAX) NULL)
--END;
