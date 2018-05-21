SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.AuthUsers ON
GO
MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Bin = N'1234567890123', CompanyName = N'companyName', DateCreate = '2018-05-02 11:03:29.8300902', DateUpdate = '2018-05-02 11:03:29.8300913', Email = N'admin@post.net', FirstName = N'Администратор', HasIin = CONVERT(bit, 'True'), Iin = N'1234567890123', LastName = N'lastName', MiddleName = N'middleName', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'admin', UserType = NULL
WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType) VALUES (1, N'1234567890123', N'companyName', '2018-05-02 11:03:29.8300902', '2018-05-02 11:03:29.8300913', N'admin@post.net', N'Администратор', CONVERT(bit, 'True'), N'1234567890123', N'lastName', N'middleName', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'admin', NULL);
GO
SET IDENTITY_INSERT dbo.AuthUsers OFF
GO