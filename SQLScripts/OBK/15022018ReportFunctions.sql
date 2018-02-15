-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION BuildBlankNumber 
(
	-- Add the parameters for the function here
	@Num int
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @Result NVARCHAR(20);
	IF @Num is not null
		BEGIN
			DECLARE @Temp NVARCHAR(10);
			SELECT @Temp = CONVERT(NVARCHAR(7) , COALESCE(@Num, ''));
			DECLARE @Len int;
			SELECT @Len = 6 - LEN(@Temp);
			DECLARE @template NVARCHAR(10) = '000000';
			SELECT @Result = CONVERT(NVARCHAR(7), SUBSTRING(@template, 1, @Len) + @Temp);
		END
	ELSE 
		SELECT @Result = N'';
	
	RETURN @Result

END
GO

--select (dbo.BuildBlankNumber(15) + ' - ' + dbo.BuildBlankNumber(879))

CREATE FUNCTION BuildBlankInterval 
(
	-- Add the parameters for the function here
	@StartNumber int, @EndNumber int
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @Result NVARCHAR(20);
	IF(@StartNumber != '' OR @EndNumber != '')
		SELECT @Result = [ncels].[dbo].BuildBlankNumber(@StartNumber) + ' - ' + [ncels].[dbo].BuildBlankNumber(@EndNumber);
	ELSE
		SELECT @Result = N'';

	RETURN @Result

END
GO


CREATE FUNCTION CountZBKCopyBlanks 
(
	-- Add the parameters for the function here
	@ZBKCopyId UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @Result INT;

	SELECT @Result = COUNT(*) FROM [ncels].[dbo].[OBK_BlankNumber] 
	INNER JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
	WHERE [Corrupted] != 1 AND [OBK_BlankType].[Code] = 'ZBKcopy' AND [OBK_BlankNumber].[Object_Id] = @ZBKCopyId
	
	RETURN @Result

END
GO


