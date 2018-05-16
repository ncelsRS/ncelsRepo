
DECLARE @Id uniqueidentifier
DECLARE @Value float
DECLARE @Year INT

SET @Id = 'F0A43E25-E4E2-4844-B33F-86A70308AABB'
SET @Value = 12
SET @Year = 2017

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ValueAddedTax] WHERE Id = @Id)
BEGIN
	INSERT INTO [OBK_Ref_ValueAddedTax]
           ([Id]
           ,[Value]
           ,[Year])
     VALUES
           (@Id
           ,@Value
           ,@Year)
END