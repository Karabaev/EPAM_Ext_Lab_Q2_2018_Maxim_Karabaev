IF NOT EXISTS(SELECT * FROM sys.databases 
          WHERE name='Forum')
BEGIN
	CREATE DATABASE Forum
	USE Forum
END;