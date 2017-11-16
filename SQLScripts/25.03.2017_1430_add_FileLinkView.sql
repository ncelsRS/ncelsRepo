CREATE VIEW [dbo].[FileLinkView]
AS
SELECT        f.Id, f.CreateDate, f.FileName, f.Version, f.DocumentId,
 f.CategoryId, f.ParentId, c.Name AS Category, pf.FileName as ParentFileName
FROM            FileLinks f
INNER JOIN Dictionaries c ON f.CategoryId = c.Id
LEFT JOIN FileLinks pf on pf.Id=f.ParentId
GO
