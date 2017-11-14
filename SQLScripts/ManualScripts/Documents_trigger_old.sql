USE [ncels]
GO
/****** Object:  Trigger [dbo].[update_documents]    Script Date: 26.05.2017 16:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <16.07.2011>
-- Description:	<������� �� UPDATE ��� Documents>
-- =============================================
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
	if (UPDATE(StateType) and (select StateType from deleted) = 0 and (select StateType from inserted) = 1 and (select RepeaterId from inserted)  is null)
	BEGIN
			-- �������� �������
		if ((select DocumentType from inserted) = 0 
		and (select DocumentKindDictionaryId from inserted) <> '95E21020-089C-470F-B20E-CE8F0023D66B' 
		and (select DocumentKindDictionaryId from inserted) <> 'ABF72347-C50C-46DD-B86C-61D170A5A417'
		and ((select AnswersId from inserted) ='' or (select AnswersId from inserted) is null)
		)
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'IncCount' and UserId = (select OrganizationId from inserted)) + 1;

			SET @reg = cast(@number as nvarchar(100));
			 
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncCount' and UserId = (select OrganizationId from inserted);
		END
		-- �������� �����
		if ((select DocumentType from inserted) = 0
		 and (select DocumentKindDictionaryId from inserted) = '95E21020-089C-470F-B20E-CE8F0023D66B'
		 and ((select AnswersId from inserted) ='' or (select AnswersId from inserted) is null)
		 )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'IncEmailCount' and UserId = (select OrganizationId from inserted)) + 1;

			SET @reg = cast(@number as nvarchar(100));
			 
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncEmailCount' and UserId = (select OrganizationId from inserted);
		END
		-- �������� ���������
		if ((select DocumentType from inserted) = 0
		 and (select DocumentKindDictionaryId from inserted) = 'ABF72347-C50C-46DD-B86C-61D170A5A417'
		 and ((select AnswersId from inserted) ='' or (select AnswersId from inserted) is null)
		 )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'IncDirectCount' and UserId = (select OrganizationId from inserted)) + 1;

			SET @reg =  cast(@number as nvarchar(100)) + '�' ;
			 
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncDirectCount' and UserId = (select OrganizationId from inserted);
		END
			-- �������� ��������
		if ((select DocumentType from inserted) = 0  and (select AnswersId from inserted) <>'' and (select AnswersId from inserted) is not null)
		 
		BEGIN
			--SET @number = (select d.Number from Documents as d where d.Id = (select AnswersId from inserted));

			SET @reg =   'K�' + (select d.Number from Documents as d where d.Id = (select AnswersId from inserted)) 
			 
			update Documents set
				Number = @reg,
				SortNumber = 999,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

	
		END

		-- ���������
		if ((select DocumentType from inserted) = 1)
		BEGIN
			--SET @number = (select cast(Value as int) from Settings where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Dictionaries where Id = (select NomenclatureDictionaryId from inserted))

			--SET @reg = @prefix + '/' + cast(@number as nvarchar(100));
			--SET @dn = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

			--update Documents set
			--	Number = @reg,
			--	SortNumber = @number,
			--	DisplayName = @dn
			--	where Id = (select Id from inserted)

			--update Settings set
			--	Value = @number
			--	where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted);
			declare @type nvarchar(100)
			if ((select OutgoingType from inserted) = 1 or (select OutgoingType from inserted) = 2)
			SET @type='AnswerOutCount'
			else 
			SET @type='OutCount'
			DECLARE @AnswerType int;
			DECLARE @AnswerNumber nvarchar(100);
			DECLARE @AnswerEmail nvarchar(100);
			SET @number = (select cast(Value as int) from Settings where UniqueName = @type and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Units where Id = (select ParentId from Units where Id = (select PositionId from Employees where Id = (select CreatedUserId from inserted))))
			--SET @code =   (select d.Code from  Dictionaries as d where d.Id = (select UnitTypeDictionaryId from Units where Id = (SELECT top 1 Value FROM [dbo].[split] ( (select CorrespondentsId from inserted), ','))))
			if ((select AnswersId from inserted) is not null and (select AnswersId from inserted)<>'')
			begin
			SET @AnswerType = (select DocumentType from Documents where Id = (select AnswersId from inserted))

			if (@AnswerType=2)
			begin 
			SET @AnswerNumber = (select Number from Documents where Id = (select AnswersId from inserted))
			SET @AnswerEmail = (select ApplicantEmail from Documents where Id = (select AnswersId from inserted))
			SET @reg = @AnswerNumber;
			SET @dn = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @dn,
				ApplicantEmail = @AnswerEmail
				where Id = (select Id from inserted)

			end 
			else 
			begin
			--SET @reg = @prefix + '-' + @code + '/' + cast(@number as nvarchar(100));
			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @dn
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = @type and UserId = (select OrganizationId from inserted);
				end
				end
				 else 			
				 begin
	-- ��������� �������� 
		if ((select DocumentType from inserted) = 1 and ( (select OutgoingType from inserted) = 1 or (select OutgoingType from inserted) = 2) )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Units where Id = (select ParentId from Units where Id = (select PositionId from Employees where Id = (select CreatedUserId from inserted))))
			--SET @code =   (select d.Code from  Dictionaries as d where d.Id = (select UnitTypeDictionaryId from Units where Id = (SELECT top 1 Value FROM [dbo].[split] ( (select CorrespondentsId from inserted), ','))))

			--SET @reg = @prefix + '-' + @code + '/' + cast(@number as nvarchar(100));
			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted);
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted);
		END
		-- ��������� ������������ 
		if ((select DocumentType from inserted) = 1 and  (select OutgoingType from inserted) = 0 )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'OutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Units where Id = (select ParentId from Units where Id = (select PositionId from Employees where Id = (select CreatedUserId from inserted))))
			--SET @code =   (select d.Code from  Dictionaries as d where Id = (select UnitTypeDictionaryId from Units where Id = (SELECT top 1 Value FROM [dbo].[split] ( (select CorrespondentsId from inserted), ','))))

			--SET @reg = @prefix + '-' + @code + '/' + cast(@number as nvarchar(100));
			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted);
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' �� ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'OutCount' and UserId = (select OrganizationId from inserted);
		END
			
			
			end

			UPDATE [dbo].[Documents]
				SET DestinationValue = @dn
			WHERE [Documents].Id IN (SELECT cast(inserted.SourceId as uniqueidentifier) FROM inserted)
		END
	END

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

	DECLARE @i int
	DECLARE @numrows int
	DECLARE @temp_id uniqueidentifier
	DECLARE @temp_table TABLE (
		idx smallint Primary Key IDENTITY(1,1),
		temp_id uniqueidentifier
	)

	DECLARE @Id nvarchar(MAX)
	DECLARE @StateType int
	DECLARE @OutgoingType int
	DECLARE @DocumentType int
	DECLARE @MainDocumentId uniqueidentifier
	DECLARE @MainTaskId uniqueidentifier
	DECLARE @DisplayName nvarchar(MAX)
	DECLARE @ExecutorsId nvarchar(MAX)
	DECLARE @ExecutorsValue nvarchar(MAX)
	DECLARE @SourceId nvarchar(MAX)
	DECLARE @AnswersId nvarchar(MAX)
	DECLARE @ModifiedUser nvarchar(MAX)
	DECLARE @ParentTask uniqueidentifier

	DECLARE cur CURSOR LOCAL FOR
	SELECT  Id,
			StateType,
			OutgoingType,
			DocumentType,
			MainDocumentId,
			MainTaskId,
			DisplayName,
			CreatedUserId,
			CreatedUserValue,
			SourceId,
			AnswersId,
			ModifiedUser
			
	FROM inserted
	
	OPEN cur
    
    FETCH NEXT FROM cur INTO
			@Id,
			@StateType,
			@OutgoingType,
			@DocumentType,
			@MainDocumentId,
			@MainTaskId,
			@DisplayName,
			@ExecutorsId,
			@ExecutorsValue,
			@SourceId,
			@AnswersId,
			@ModifiedUser	
	
	WHILE @@FETCH_STATUS = 0
	BEGIN	
		
		-- �������� ���������, ���� �������� �������� ��������� ��������
		if (update([StateType]) AND @StateType = 1 AND @OutgoingType = 1 AND @DocumentType = 1)--AND @MainTaskId is not null)
		BEGIN
			DELETE FROM @temp_table
			INSERT @temp_table
			SELECT distinct Id FROM Documents
			WHERE Id IN (SELECT Value FROM [dbo].[split](@AnswersId, ', ') WHERE @ExecutorsId in (SELECT @ExecutorsId FROM Tasks WHERE DocumentId = Value and FunctionType = 1))

			SET @i = 1
			SET @numrows = (SELECT COUNT(*) FROM @temp_table)
			IF @numrows > 0
				WHILE (@i <= (SELECT MAX(idx) FROM @temp_table))
				BEGIN
					SET @temp_id = (SELECT temp_id FROM @temp_table WHERE idx = @i)

					SET @MainTaskId = (SELECT TOP(1) Id FROM Tasks WHERE DocumentId = @temp_id and ExecutorId = @ExecutorsId order by IsMainLine DESC)

					SET @ParentTask = @MainTaskId

					while(@ParentTask is not null)
					BEGIN
						UPDATE [dbo].[Tasks]
						SET State = 2, ModifiedUser = @ModifiedUser
						WHERE Id = @ParentTask

						INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
						VALUES (NEWID(), 0, '������� ������� ��������� ����������: ' +  @dn, @Id, @dn, GETDATE(), @temp_id, @ParentTask, @ModifiedUser)

						SET @ParentTask = (SELECT Id FROM [dbo].[Tasks] WHERE Id = (SELECT ParentTask FROM [dbo].[Activities] WHERE Id = (SELECT ActivityId FROM [dbo].[Tasks] WHERE Id = @ParentTask and FunctionType = 1)))
					END

					-- �������� ������� � ��������������
					if ((SELECT FunctionType + IsMainLine FROM [dbo].[Tasks] WHERE Id = @MainTaskId) = 2)
						UPDATE [dbo].[Tasks]
							SET State = 2, ModifiedUser = @ModifiedUser
							WHERE DocumentId = @temp_id and State <> 2

					if ((SELECT FunctionType + IsMainLine FROM [dbo].[Tasks] WHERE Id = @MainTaskId) = 2)
						UPDATE [dbo].[Documents]
						SET StateType = 9, ModifiedUser = @ModifiedUser, FactExecutionDate = GETDATE(), AutoFactExecutionDate = GETDATE() WHERE Id = @temp_id					

					SET @i = @i + 1

				END
		END
		
		-- �������� ������� ��� ���������� ������ � �������, ���� �������� ������������� �������� ��������� ��������
		if (update([StateType]) AND @StateType = 1 AND @OutgoingType = 2 AND @DocumentType = 1 AND @MainTaskId is not null)
		BEGIN
			DELETE FROM @temp_table
			INSERT @temp_table
			SELECT distinct Id FROM Documents
			WHERE Id IN (SELECT Value FROM [dbo].[split](@AnswersId, ', ') WHERE @ExecutorsId in (SELECT @ExecutorsId FROM Tasks WHERE DocumentId = Value and FunctionType = 1))

			SET @i = 1
			SET @numrows = (SELECT COUNT(*) FROM @temp_table)
			IF @numrows > 0
				WHILE (@i <= (SELECT MAX(idx) FROM @temp_table))
				BEGIN
					SET @temp_id = (SELECT temp_id FROM @temp_table WHERE idx = @i)

					SET @ParentTask = (SELECT Id FROM [dbo].[Tasks] WHERE Id = @MainTaskId and IsMainLine = 0)

					if(@ParentTask is null)
						INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
						VALUES 	(NEWID(), 0, '��� ��������� ������������� ��������� ��������: ' + @dn, @Id, @dn, GETDATE(), @temp_id, @MainTaskId, @ModifiedUser)
					else
						while(@ParentTask is not null)
						BEGIN
							UPDATE [dbo].[Tasks]
							SET State = 2, ModifiedUser = @ModifiedUser
							WHERE Id = @ParentTask

							INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
							VALUES (NEWID(), 0, '������� ������� ������������� ��������� ����������: ' +  @dn, @Id, @dn, GETDATE(), @temp_id, @ParentTask, @ModifiedUser)

							SET @ParentTask = (SELECT Id FROM [dbo].[Tasks] WHERE Id = (SELECT ParentTask FROM [dbo].[Activities] WHERE Id = (SELECT ActivityId FROM [dbo].[Tasks] WHERE Id = @ParentTask and FunctionType = 1)))
						END
						
					-- ������� ��������� � ������������
					--UPDATE [dbo].[Documents]
					--SET MonitoringType = 4
					--WHERE Id = @temp_id and MonitoringType <> 4
					
					SET @i = @i + 1
				END
		END

		-- �������� �������, ���� ������� ��������
		if (update([StateType]) AND @StateType = 9 AND @DocumentType IN (0, 2, 3, 5))
		BEGIN
			DELETE FROM @temp_table
			INSERT @temp_table
			SELECT distinct Id FROM Tasks
			WHERE DocumentId IN (SELECT Id FROM inserted) and State <> 2

			SET @i = 1
			SET @numrows = (SELECT COUNT(*) FROM @temp_table)
			IF @numrows > 0
				WHILE (@i <= (SELECT MAX(idx) FROM @temp_table))
				BEGIN
					SET @temp_id = (SELECT temp_id FROM @temp_table WHERE idx = @i)
					UPDATE [dbo].[Tasks]
					SET State = 2, ModifiedUser = @ModifiedUser WHERE Id = @temp_id
					SET @i = @i + 1
					INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
					VALUES 	(NEWID(), 0, '������� ������� ' + (case WHEN ((SELECT Note FROM Documents WHERE Id = @Id) is null or (SELECT Note FROM Documents WHERE Id = @Id) = '') THEN '' ELSE ': ' + (SELECT Note FROM Documents WHERE Id = @Id) end), null, null, GETDATE(), @Id, @temp_id, @ModifiedUser)
				END
		END
		
    FETCH NEXT FROM cur INTO
			@Id,
			@StateType,
			@OutgoingType,
			@DocumentType,
			@MainDocumentId,
			@MainTaskId,
			@DisplayName,
			@ExecutorsId,
			@ExecutorsValue,
			@SourceId,
			@AnswersId,
			@ModifiedUser

	END
	
	CLOSE cur
	DEALLOCATE cur

	if (update(StateType) and (select top(1) StateType from inserted) = 9 and (select top(1) DocumentType from inserted) = 4 and (select top(1) ProjectType from inserted) > 9)
	begin
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'PrtCount') + 1;

			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' �� ' + (select CONVERT(char(10), GETDATE(),104) inserted);
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @dn
				where Id in (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'PrtCount' and UserId = (select OrganizationId from inserted);
	end

END





