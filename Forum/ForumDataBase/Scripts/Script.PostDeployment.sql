/*
Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся инструкции SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [dbo].[Roles] VALUES ('Admin', 10)
INSERT INTO [dbo].[Roles] VALUES ('User', 1)

INSERT INTO [dbo].[Users] VALUES ('Steve512', '123456', 'Steve', 2, 0, '2018-01-10', 'steve@123.ru')
INSERT INTO [dbo].[Users] VALUES ('Max', '654245', 'Max admin', 1, 0, '2017-05-10', 'max@764.ru')

INSERT INTO [dbo].[Sections] VALUES ('C# programming', 'C# programming', 'localhost:123456\CsProg')
INSERT INTO [dbo].[Sections] VALUES ('C++ programming', 'C++ programming', 'localhost:123456\CppProg')

INSERT INTO [dbo].[Topics] VALUES ('Help me please', 1, '2018-05-01', 'localhost:123456\CsProg\Topic1', 1)
INSERT INTO [dbo].[Topics] VALUES ('Trouble with strings', 1, '2018-08-07', 'localhost:123456\CppProg\Topic2', 2)

INSERT INTO [dbo].[Messages] VALUES (1, '2018-05-01', 'Help me please, I can''t run a console application at VS2017.', 1)
INSERT INTO [dbo].[Messages] VALUES (2, '2018-05-01', 'Hello, you can press F5 to run application.', 1)
INSERT INTO [dbo].[Messages] VALUES (1, '2018-05-01', 'It''s work!', 1)
INSERT INTO [dbo].[Messages] VALUES (1, '2018-08-07', 'The console displays hieroglyphs, how to fix it?', 2)

INSERT INTO [dbo].[MessageAttachments] VALUES ('ftp://testftp.ru', 1)