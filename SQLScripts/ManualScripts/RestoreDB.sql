----Make Database to single user Mode
ALTER DATABASE ncels
SET SINGLE_USER WITH
ROLLBACK IMMEDIATE
 
----Restore Database
RESTORE DATABASE ncels
FROM DISK = 'C:\NCELS_BACKUPS\ncels.bak'
 
/*If there is no error in statement before database will be in multiuser
mode.
If error occurs please execute following command it will convert
database in multi user.*/
ALTER DATABASE ncels SET MULTI_USER
GO