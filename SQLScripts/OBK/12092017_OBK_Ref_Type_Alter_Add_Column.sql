ALTER TABLE [OBK_Ref_Type]
ADD [ViewOption] INT NULL

GO

UPDATE [OBK_Ref_Type]
SET [ViewOption] = 1 -- Отображать при создании договора
WHERE [ViewOption] IS NULL

GO

ALTER TABLE [OBK_Ref_Type]
ALTER COLUMN [ViewOption] INT NOT NULL

GO