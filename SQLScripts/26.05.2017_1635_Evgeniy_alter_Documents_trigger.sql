ALTER TRIGGER [dbo].[update_documents] 
   ON [dbo].[Documents]
   AFTER UPDATE
AS 
BEGIN

	DECLARE @number int
	DECLARE @prefix nvarchar(100);
	DECLARE @reg nvarchar(100);
	DECLARE @dn nvarchar(100);
	DECLARE @code nvarchar(100);

	if (update([StateType]))
	BEGIN
		UPDATE [dbo].[Activities]
		SET IsNotActive = 0
		WHERE DocumentId IN (SELECT Id FROM inserted WHERE StateType=4) AND (ParentId is NULL AND IsCurrent = 1)
		
		UPDATE [dbo].[Activities]
		SET IsCurrent = 0
		WHERE DocumentId IN (SELECT Id FROM inserted WHERE (StateType=6 OR StateType=7)) AND (IsCurrent = 1)
		
		UPDATE [dbo].[Tasks]
		SET IsActive = 0
		WHERE DocumentId IN (SELECT Id FROM inserted WHERE (StateType=6 OR StateType=7)) AND (IsActive = 1)

		UPDATE [dbo].[Activities]
		SET Branch = (select max(Branch) + 1 from [dbo].[Activities] WHERE DocumentId IN (SELECT Id FROM inserted WHERE (StateType=6 OR StateType=7)))			
		WHERE DocumentId IN (SELECT Id FROM inserted WHERE (StateType=6 OR StateType=7)) AND (Branch = 0)
	END
		
	if (update(Number) or update(DocumentDate))
	BEGIN
		UPDATE [dbo].[Documents]
		SET DestinationId = (SELECT i.Id FROM inserted as i WHERE i.SourceId = [Documents].Id), 
			DestinationValue = (SELECT i.DisplayName FROM inserted as i WHERE i.SourceId = [Documents].Id)
		WHERE Id IN (SELECT SourceId FROM inserted)
		UPDATE [dbo].[Documents]
		SET AnswersId = AnswersId,
			CompleteDocumentsId = CompleteDocumentsId,
			EditDocumentsId = EditDocumentsId,
			RepealDocumentsId = RepealDocumentsId
		WHERE Id IN (SELECT Id FROM inserted)
	END		

	if (update(ExecutionDate))
		BEGIN
			UPDATE Activities
				SET ExecutionDate = (SELECT i.ExecutionDate FROM inserted as i WHERE i.Id = [Activities].DocumentId)
				WHERE DocumentId IN (SELECT Id FROM inserted)
		END

END





