using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;

namespace Teme.Shared.Data.Migrations
{
    public partial class AuthUsersData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "--"
 + "-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0"
 + "-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio"
 + "-- Дата скрипта: 18.05.2018 0:19:44"
 + "-- Версия сервера: 14.00.3008"
 + "--"
 +
"SET DATEFORMAT ymd"
 + "SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON"
 + "SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF"
 + "GO"
 +
"SET IDENTITY_INSERT dbo.AuthUserRoles ON"
 + "GO"
 + "MERGE INTO dbo.AuthUserRoles t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:51:51.6391285', DateUpdate = '2018-05-17 23:51:53.6184742', RoleId = 1, UserId = 4"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleId, UserId) VALUES (1, '2018-05-17 23:51:51.6391285', '2018-05-17 23:51:53.6184742', 1, 4);"
 + "MERGE INTO dbo.AuthUserRoles t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:52:07.9202462', DateUpdate = '2018-05-17 23:52:09.7578059', RoleId = 2, UserId = 8"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleId, UserId) VALUES (2, '2018-05-17 23:52:07.9202462', '2018-05-17 23:52:09.7578059', 2, 8);"
 + "MERGE INTO dbo.AuthUserRoles t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:52:24.0227012', DateUpdate = '2018-05-17 23:52:26.8285778', RoleId = 3, UserId = 9"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleId, UserId) VALUES (3, '2018-05-17 23:52:24.0227012', '2018-05-17 23:52:26.8285778', 3, 9);"
 + "MERGE INTO dbo.AuthUserRoles t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:52:41.8708488', DateUpdate = '2018-05-17 23:52:43.9704288', RoleId = 4, UserId = 10"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleId, UserId) VALUES (4, '2018-05-17 23:52:41.8708488', '2018-05-17 23:52:43.9704288', 4, 10);"
 + "GO"
 + "SET IDENTITY_INSERT dbo.AuthUserRoles OFF"
 + "GO"
 +
"SET IDENTITY_INSERT dbo.AuthUsers ON"
 + "GO"
 + "MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)"
 + "WHEN MATCHED THEN UPDATE  SET Bin = N'1234567890123', CompanyName = N'companyName', DateCreate = '2018-05-02 11:03:29.8300902', DateUpdate = '2018-05-02 11:03:29.8300913', Email = N'admin@post.net', FirstName = N'Администратор', HasIin = CONVERT(bit, 'True'), Iin = N'1234567890123', LastName = N'lastName', MiddleName = N'middleName', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'admin', UserType = NULL, IconRecordId = NULL"
 + "WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType, IconRecordId) VALUES (1, N'1234567890123', N'companyName', '2018-05-02 11:03:29.8300902', '2018-05-02 11:03:29.8300913', N'admin@post.net', N'Администратор', CONVERT(bit, 'True'), N'1234567890123', N'lastName', N'middleName', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'admin', NULL, NULL);"
 + "MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)"
 + "WHEN MATCHED THEN UPDATE  SET Bin = N'123123123123', CompanyName = N'companyName', DateCreate = '2018-05-17 23:45:17.6399078', DateUpdate = '2018-05-17 23:45:20.4259112', Email = N'cheef@post.net', FirstName = N'Иван', HasIin = CONVERT(bit, 'True'), Iin = N'123123123', LastName = N'Иванов', MiddleName = N'Иванович', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'cheef', UserType = NULL, IconRecordId = NULL"
 + "WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType, IconRecordId) VALUES (4, N'123123123123', N'companyName', '2018-05-17 23:45:17.6399078', '2018-05-17 23:45:20.4259112', N'cheef@post.net', N'Иван', CONVERT(bit, 'True'), N'123123123', N'Иванов', N'Иванович', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'cheef', NULL, NULL);"
 + "MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)"
 + "WHEN MATCHED THEN UPDATE  SET Bin = N'123123123321', CompanyName = N'companyName', DateCreate = '2018-05-17 23:47:15.6938464', DateUpdate = '2018-05-17 23:47:17.3614251', Email = N'executor@post.net', FirstName = N'Вячеслав', HasIin = CONVERT(bit, 'True'), Iin = N'123123321', LastName = N'Славин', MiddleName = N'Вячеславович', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'expert', UserType = NULL, IconRecordId = NULL"
 + "WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType, IconRecordId) VALUES (8, N'123123123321', N'companyName', '2018-05-17 23:47:15.6938464', '2018-05-17 23:47:17.3614251', N'executor@post.net', N'Вячеслав', CONVERT(bit, 'True'), N'123123321', N'Славин', N'Вячеславович', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'expert', NULL, NULL);"
 + "MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)"
 + "WHEN MATCHED THEN UPDATE  SET Bin = N'123321123', CompanyName = N'companyName', DateCreate = '2018-05-17 23:49:00.0959485', DateUpdate = '2018-05-17 23:49:02.1430175', Email = N'ceo@post.net', FirstName = N'Дария', HasIin = CONVERT(bit, 'True'), Iin = N'123321123', LastName = N'Дарина', MiddleName = N'Ивановна', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'ceo', UserType = NULL, IconRecordId = NULL"
 + "WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType, IconRecordId) VALUES (9, N'123321123', N'companyName', '2018-05-17 23:49:00.0959485', '2018-05-17 23:49:02.1430175', N'ceo@post.net', N'Дария', CONVERT(bit, 'True'), N'123321123', N'Дарина', N'Ивановна', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'ceo', NULL, NULL);"
 + "MERGE INTO dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 10)"
 + "WHEN MATCHED THEN UPDATE  SET Bin = N'321123123', CompanyName = N'companyName', DateCreate = '2018-05-17 23:50:23.9133853', DateUpdate = '2018-05-17 23:50:26.0991358', Email = N'viewer@post.net', FirstName = N'Наталия', HasIin = CONVERT(bit, 'True'), Iin = N'321123123', LastName = N'Наталинова', MiddleName = N'Ивановна', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserName = N'viewer', UserType = NULL, IconRecordId = NULL"
 + "WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserName, UserType, IconRecordId) VALUES (10, N'321123123', N'companyName', '2018-05-17 23:50:23.9133853', '2018-05-17 23:50:26.0991358', N'viewer@post.net', N'Наталия', CONVERT(bit, 'True'), N'321123123', N'Наталинова', N'Ивановна', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', N'viewer', NULL, NULL);"
 + "GO"
 + "SET IDENTITY_INSERT dbo.AuthUsers OFF"
 + "GO"
 +
"SET IDENTITY_INSERT dbo.AuthUserScopes ON"
 + "GO"
 + "MERGE INTO dbo.AuthUserScopes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)"
 + "WHEN MATCHED THEN UPDATE  SET AuthUserId = 1, DateCreate = '2018-05-02 11:03:29.8310194', DateUpdate = '2018-05-02 11:03:29.8310198', Scope = N'ext'"
 + "WHEN NOT MATCHED THEN INSERT (Id, AuthUserId, DateCreate, DateUpdate, Scope) VALUES (1, 1, '2018-05-02 11:03:29.8310194', '2018-05-02 11:03:29.8310198', N'ext');"
 + "GO"
 + "SET IDENTITY_INSERT dbo.AuthUserScopes OFF"
 + "GO"
 +
"SET IDENTITY_INSERT dbo.Permissions ON"
 + "GO"
 + "MERGE INTO dbo.Permissions t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:39:05.5251017', DateUpdate = '2018-05-17 23:39:07.9306769', PermissionCode = N'IsCheef', RoleId = 1"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, PermissionCode, RoleId) VALUES (3, '2018-05-17 23:39:05.5251017', '2018-05-17 23:39:07.9306769', N'IsCheef', 1);"
 + "MERGE INTO dbo.Permissions t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:42:04.7393079', DateUpdate = '2018-05-17 23:42:06.3055081', PermissionCode = N'IsExecutor', RoleId = 2"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, PermissionCode, RoleId) VALUES (4, '2018-05-17 23:42:04.7393079', '2018-05-17 23:42:06.3055081', N'IsExecutor', 2);"
 + "MERGE INTO dbo.Permissions t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:42:30.5077050', DateUpdate = '2018-05-17 23:42:32.7253545', PermissionCode = N'IsCeo', RoleId = 3"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, PermissionCode, RoleId) VALUES (5, '2018-05-17 23:42:30.5077050', '2018-05-17 23:42:32.7253545', N'IsCeo', 3);"
 + "MERGE INTO dbo.Permissions t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:43:03.2486805', DateUpdate = '2018-05-17 23:43:05.1415778', PermissionCode = N'Viewer', RoleId = 4"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, PermissionCode, RoleId) VALUES (6, '2018-05-17 23:43:03.2486805', '2018-05-17 23:43:05.1415778', N'Viewer', 4);"
 + "GO"
 + "SET IDENTITY_INSERT dbo.Permissions OFF"
 + "GO"
 +
"SET IDENTITY_INSERT dbo.Roles ON"
 + "GO"
 + "MERGE INTO dbo.Roles t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:39:40.5986721', DateUpdate = '2018-05-17 23:39:43.2673095', RoleName = N'Cheef'"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleName) VALUES (1, '2018-05-17 23:39:40.5986721', '2018-05-17 23:39:43.2673095', N'Cheef');"
 + "MERGE INTO dbo.Roles t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:40:52.2493227', DateUpdate = '2018-05-17 23:40:54.2122994', RoleName = N'Executor'"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleName) VALUES (2, '2018-05-17 23:40:52.2493227', '2018-05-17 23:40:54.2122994', N'Executor');"
 + "MERGE INTO dbo.Roles t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:41:30.1560122', DateUpdate = '2018-05-17 23:41:32.1039480', RoleName = N'Ceo'"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleName) VALUES (3, '2018-05-17 23:41:30.1560122', '2018-05-17 23:41:32.1039480', N'Ceo');"
 + "MERGE INTO dbo.Roles t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)"
 + "WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-17 23:41:47.5222084', DateUpdate = '2018-05-17 23:41:49.0451574', RoleName = N'Viewer'"
 + "WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, RoleName) VALUES (4, '2018-05-17 23:41:47.5222084', '2018-05-17 23:41:49.0451574', N'Viewer');"
 + "GO"
 + "SET IDENTITY_INSERT dbo.Roles OFF"
 + "GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
