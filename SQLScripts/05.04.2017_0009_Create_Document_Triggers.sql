-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <16.07.2011>
-- Description:	<Триггер на UPDATE для Documents>
-- =============================================
CREATE TRIGGER [dbo].[update_documents] 
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
			-- ВХОДЯЩИЕ обычные
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
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncCount' and UserId = (select OrganizationId from inserted);
		END
		-- ВХОДЯЩИЕ почта
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
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncEmailCount' and UserId = (select OrganizationId from inserted);
		END
		-- ВХОДЯЩИЕ директивы
		if ((select DocumentType from inserted) = 0
		 and (select DocumentKindDictionaryId from inserted) = 'ABF72347-C50C-46DD-B86C-61D170A5A417'
		 and ((select AnswersId from inserted) ='' or (select AnswersId from inserted) is null)
		 )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'IncDirectCount' and UserId = (select OrganizationId from inserted)) + 1;

			SET @reg =  cast(@number as nvarchar(100)) + 'Д' ;
			 
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'IncDirectCount' and UserId = (select OrganizationId from inserted);
		END
			-- ВХОДЯЩИЕ ответные
		if ((select DocumentType from inserted) = 0  and (select AnswersId from inserted) <>'' and (select AnswersId from inserted) is not null)
		 
		BEGIN
			--SET @number = (select d.Number from Documents as d where d.Id = (select AnswersId from inserted));

			SET @reg =   'K№' + (select d.Number from Documents as d where d.Id = (select AnswersId from inserted)) 
			 
			update Documents set
				Number = @reg,
				SortNumber = 999,
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

	
		END

		-- ИСХОДЯЩИЕ
		if ((select DocumentType from inserted) = 1)
		BEGIN
			--SET @number = (select cast(Value as int) from Settings where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Dictionaries where Id = (select NomenclatureDictionaryId from inserted))

			--SET @reg = @prefix + '/' + cast(@number as nvarchar(100));
			--SET @dn = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

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
			SET @dn = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

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
			SET @dn = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)

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
	-- ИСХОДЯЩИЕ ответные 
		if ((select DocumentType from inserted) = 1 and ( (select OutgoingType from inserted) = 1 or (select OutgoingType from inserted) = 2) )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Units where Id = (select ParentId from Units where Id = (select PositionId from Employees where Id = (select CreatedUserId from inserted))))
			--SET @code =   (select d.Code from  Dictionaries as d where d.Id = (select UnitTypeDictionaryId from Units where Id = (SELECT top 1 Value FROM [dbo].[split] ( (select CorrespondentsId from inserted), ','))))

			--SET @reg = @prefix + '-' + @code + '/' + cast(@number as nvarchar(100));
			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted);
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
				where Id = (select Id from inserted)

			update Settings set
				Value = @number
				where UniqueName = 'AnswerOutCount' and UserId = (select OrganizationId from inserted);
		END
		-- ИСХОДЯЩИЕ инициативные 
		if ((select DocumentType from inserted) = 1 and  (select OutgoingType from inserted) = 0 )
		BEGIN
			SET @number = (select cast(Value as int) from Settings where UniqueName = 'OutCount' and UserId = (select OrganizationId from inserted)) + 1;
			--SET @prefix = (select Code from Units where Id = (select ParentId from Units where Id = (select PositionId from Employees where Id = (select CreatedUserId from inserted))))
			--SET @code =   (select d.Code from  Dictionaries as d where Id = (select UnitTypeDictionaryId from Units where Id = (SELECT top 1 Value FROM [dbo].[split] ( (select CorrespondentsId from inserted), ','))))

			--SET @reg = @prefix + '-' + @code + '/' + cast(@number as nvarchar(100));
			SET @reg = cast(@number as nvarchar(100));
			SET @dn = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted);
			update Documents set
				Number = @reg,
				SortNumber = @number,
				DisplayName = @reg + ' от ' + (select CONVERT(char(10), DocumentDate,104) from inserted)
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
		
		-- Закрытие документа, если зарегили ответный исходящий документ
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
						VALUES (NEWID(), 0, 'Задание закрыто исходящим документом: ' +  @dn, @Id, @dn, GETDATE(), @temp_id, @ParentTask, @ModifiedUser)

						SET @ParentTask = (SELECT Id FROM [dbo].[Tasks] WHERE Id = (SELECT ParentTask FROM [dbo].[Activities] WHERE Id = (SELECT ActivityId FROM [dbo].[Tasks] WHERE Id = @ParentTask and FunctionType = 1)))
					END

					-- Закрытие заданий у соисполнителей
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
		
		-- Закрытие заданий или добавление отчета к заданию, если зарегили промежуточный ответный исходящий документ
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
						VALUES 	(NEWID(), 0, 'Был направлен промежуточный исходящий документ: ' + @dn, @Id, @dn, GETDATE(), @temp_id, @MainTaskId, @ModifiedUser)
					else
						while(@ParentTask is not null)
						BEGIN
							UPDATE [dbo].[Tasks]
							SET State = 2, ModifiedUser = @ModifiedUser
							WHERE Id = @ParentTask

							INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
							VALUES (NEWID(), 0, 'Задание закрыто промежуточным исходящим документом: ' +  @dn, @Id, @dn, GETDATE(), @temp_id, @ParentTask, @ModifiedUser)

							SET @ParentTask = (SELECT Id FROM [dbo].[Tasks] WHERE Id = (SELECT ParentTask FROM [dbo].[Activities] WHERE Id = (SELECT ActivityId FROM [dbo].[Tasks] WHERE Id = @ParentTask and FunctionType = 1)))
						END
						
					-- Перевод документа в долгосрочный
					--UPDATE [dbo].[Documents]
					--SET MonitoringType = 4
					--WHERE Id = @temp_id and MonitoringType <> 4
					
					SET @i = @i + 1
				END
		END

		-- Закрытие заданий, если закрыли документ
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
					VALUES 	(NEWID(), 0, 'Задание закрыто ' + (case WHEN ((SELECT Note FROM Documents WHERE Id = @Id) is null or (SELECT Note FROM Documents WHERE Id = @Id) = '') THEN '' ELSE ': ' + (SELECT Note FROM Documents WHERE Id = @Id) end), null, null, GETDATE(), @Id, @temp_id, @ModifiedUser)
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
			SET @dn = @reg + ' от ' + (select CONVERT(char(10), GETDATE(),104) inserted);
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





GO

ALTER TABLE [dbo].[Documents] ENABLE TRIGGER [update_documents]
GO

------- Activity


-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <18.07.2011>
-- Description:	<Триггер на INSERT для Activities>
-- =============================================
CREATE TRIGGER [dbo].[insertTasks_activities]
   ON  [dbo].[Activities] 
   AFTER INSERT
AS 
BEGIN
	DECLARE @Text nvarchar(MAX)
	DECLARE @ExecutionDate datetime
	DECLARE @CreatedDate datetime
	DECLARE @AuthorId nvarchar(MAX)
	DECLARE @AuthorValue nvarchar(MAX)
	DECLARE @DocumentId uniqueidentifier
	DECLARE @DocumentValue nvarchar(MAX)
	DECLARE @Type int
	DECLARE @ActivityId uniqueidentifier
	DECLARE @ExecutorsId nvarchar(MAX)
	DECLARE @ExecutorsValue nvarchar(MAX)
	DECLARE @ResponsibleId nvarchar(MAX)
	DECLARE @ResponsibleValue nvarchar(MAX)
	DECLARE @IsNotActive bit
	DECLARE @ModifiedUser nvarchar(MAX)
	DECLARE @IsMainLine bit
	DECLARE @TypeEx int

	DECLARE @ExecutorsTableId TABLE 
	(
		Id INT IDENTITY(1, 1),
		Value NVARCHAR(MAX),
		Type INT
	)
	DECLARE @ExecutorsTableValue TABLE 
	(
		Id INT IDENTITY(1, 1),
		Value NVARCHAR(MAX)
	)
	
	DECLARE @Delta INT
	DECLARE @TaskCount INT
	SET @Delta = 0
	SET @TaskCount = (SELECT CAST(Value as INT) FROM [dbo].[Settings] WHERE UniqueName = 'TaskCount')
	
	DECLARE cur CURSOR LOCAL FOR
	SELECT  Text,
			ExecutionDate,
			CreatedDate,
			AuthorId,
			AuthorValue,
			DocumentId,
			DocumentValue,
			Type,
			Id,
			ExecutorsId,
			ExecutorsValue,
			ResponsibleId,
			ResponsibleValue,
			IsNotActive,
			ModifiedUser,
			IsMainLine,
			TypeEx
	FROM inserted
	
	OPEN cur
    
    FETCH NEXT FROM cur INTO	
			@Text,
			@ExecutionDate,
			@CreatedDate,
			@AuthorId,
			@AuthorValue,
			@DocumentId,
			@DocumentValue,
			@Type,
			@ActivityId,
			@ExecutorsId,
			@ExecutorsValue,
			@ResponsibleId,
			@ResponsibleValue,
			@IsNotActive,
			@ModifiedUser,
			@IsMainLine,
			@TypeEx
			
	WHILE @@FETCH_STATUS = 0
	BEGIN
		if (@IsNotActive = 0)
		BEGIN
			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				SET @ExecutorsId = @ExecutorsId + ', ' + @ResponsibleId
				SET @ExecutorsValue = @ExecutorsValue + ', ' + @ResponsibleValue
			END

			DELETE FROM @ExecutorsTableId
			INSERT INTO @ExecutorsTableId(Value, Type)
			SELECT Value, 0 FROM [dbo].[split](@ExecutorsId,', ')
			DELETE FROM @ExecutorsTableValue
			INSERT INTO @ExecutorsTableValue(Value)
			SELECT Value FROM [dbo].[split](@ExecutorsValue,', ')
			
			UPDATE @ExecutorsTableId
			SET Type = 1 WHERE Value = @ResponsibleId
			
			SET @TaskCount = @TaskCount - @Delta
		
			INSERT INTO [dbo].[Tasks]
				([Text], [ExecutionDate], [CreatedDate], [AuthorId], [AuthorValue], [ExecutorId], [ExecutorValue], [DocumentId], [DocumentValue], [Type], [ActivityId], [Number], [FunctionType], [ModifiedUser], [IsMainLine], [TypeEx], [Stage])
			SELECT
				@Text, @ExecutionDate, @CreatedDate, @AuthorId, @AuthorValue, ids.Value, vls.Value, @DocumentId, @DocumentValue, @Type, @ActivityId, @TaskCount + ids.Id, ids.Type, @ModifiedUser, 0, 0, @TypeEx FROM @ExecutorsTableId as ids INNER JOIN @ExecutorsTableValue as vls ON ids.Id = vls.Id
			
			SET @Delta = (SELECT MAX(Id) FROM @ExecutorsTableId)
			if (@Delta is NULL)
				SET @Delta = 0
				
			SET @TaskCount = @TaskCount + @Delta
				
			UPDATE [dbo].[Settings]
			SET [Value] = @TaskCount
			WHERE UniqueName = 'TaskCount'
		END
		
	    FETCH NEXT FROM cur INTO	
			@Text,
			@ExecutionDate,
			@CreatedDate,
			@AuthorId,
			@AuthorValue,
			@DocumentId,
			@DocumentValue,
			@Type,
			@ActivityId,
			@ExecutorsId,
			@ExecutorsValue,
			@ResponsibleId,
			@ResponsibleValue,
			@IsNotActive,
			@ModifiedUser,
			@IsMainLine,
			@TypeEx
	END
	
	CLOSE cur
	DEALLOCATE cur

	--UPDATE doc
	--SET doc.ResponsibleId = [dbo].[trim]((SELECT CAST(ExecutorId as nvarchar(50)) + ', ' FROM Tasks WHERE 
	--						DocumentId = i.DocumentId and 
	--						Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId= i.DocumentId and ParentTask is not null) and
	--						IsMainLine = 1 and
	--						FunctionType = 1 for xml path('')), 2),
	--	doc.ResponsibleValue = [dbo].[trim]((SELECT [dbo].DisplayNameEmployee(ExecutorId) + ', ' FROM Tasks WHERE 
	--						DocumentId =i.DocumentId  and 
	--						Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId = i.DocumentId  and ParentTask is not null) and
	--						IsMainLine = 1 and
	--						FunctionType = 1  for xml path('')), 2)
	--from Documents as doc
	--left join inserted as i on i.DocumentId = doc.Id
	--WHERE  i.DocumentId is not null
END





GO

ALTER TABLE [dbo].[Activities] ENABLE TRIGGER [insertTasks_activities]
GO




-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <18.07.2011>
-- Description:	<Триггер на UPDATE для Activities>
-- =============================================
CREATE TRIGGER [dbo].[updateTasks_activities]
   ON  [dbo].[Activities] 
   AFTER UPDATE
AS 
BEGIN

	DECLARE @Text nvarchar(MAX)
	DECLARE @ExecutionDate datetime
	DECLARE @CreatedDate datetime
	DECLARE @AuthorId nvarchar(MAX)
	DECLARE @AuthorValue nvarchar(MAX)
	DECLARE @DocumentId uniqueidentifier
	DECLARE @DocumentValue nvarchar(MAX)
	DECLARE @Type int
	DECLARE @ActivityId uniqueidentifier
	DECLARE @ExecutorsId nvarchar(MAX)
	DECLARE @ExecutorsValue nvarchar(MAX)
	DECLARE @ResponsibleId nvarchar(MAX)
	DECLARE @ResponsibleValue nvarchar(MAX)
	DECLARE @IsNotActive bit
	DECLARE @ModifiedUser nvarchar(MAX)
	
	DECLARE @DelExecutorsId nvarchar(MAX)
	DECLARE @DelResponsibleId nvarchar(MAX)
	DECLARE @DelExecutorsValue nvarchar(MAX)
	DECLARE @DelResponsibleValue nvarchar(MAX)
	DECLARE @IsMainLine bit
	DECLARE @TypeEx int
	

	DECLARE @ExecutorsTableId TABLE 
	(
		Id INT IDENTITY(1, 1),
		Value NVARCHAR(MAX),
		Type INT
	)
	DECLARE @ExecutorsTableValue TABLE 
	(
		Id INT IDENTITY(1, 1),
		Value NVARCHAR(MAX)
	)
	
	DECLARE @Delta INT
	DECLARE @TaskCount INT
	SET @Delta = 0
	SET @TaskCount = (SELECT CAST(Value as INT) FROM [dbo].[Settings] WHERE UniqueName = 'TaskCount')
	
	DECLARE cur CURSOR LOCAL FOR
	SELECT  Text,
			ExecutionDate,
			CreatedDate,
			AuthorId,
			AuthorValue,
			DocumentId,
			DocumentValue,
			Type,
			Id,
			ExecutorsId,
			ExecutorsValue,
			ResponsibleId,
			ResponsibleValue,
			IsNotActive,
			ModifiedUser,
			IsMainLine,
			TypeEx
	FROM inserted
	
	OPEN cur
    
    FETCH NEXT FROM cur INTO	
			@Text,
			@ExecutionDate,
			@CreatedDate,
			@AuthorId,
			@AuthorValue,
			@DocumentId,
			@DocumentValue,
			@Type,
			@ActivityId,
			@ExecutorsId,
			@ExecutorsValue,
			@ResponsibleId,
			@ResponsibleValue,
			@IsNotActive,
			@ModifiedUser,
			@IsMainLine,
			@TypeEx
			
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		if (UPDATE(IsCurrent))
		BEGIN
		
			UPDATE [dbo].[Tasks]
			SET [IsActive] = 0, [ModifiedUser] = @ModifiedUser
			WHERE ActivityId = @ActivityId
		
		END
		
		if (UPDATE(ExecutorsId) AND @IsNotActive = 0)
		BEGIN
			
			SET @DelExecutorsId = (SELECT ExecutorsId FROM deleted WHERE Id = @ActivityId)
			SET @DelResponsibleId = (SELECT ResponsibleId FROM deleted WHERE Id = @ActivityId)
			SET @DelExecutorsValue = (SELECT ExecutorsValue FROM deleted WHERE Id = @ActivityId)
			SET @DelResponsibleValue = (SELECT ResponsibleValue FROM deleted WHERE Id = @ActivityId)
			
			if (CHARINDEX(@DelResponsibleId, @DelExecutorsId) = 0)
			BEGIN
				SET @DelExecutorsId = @DelExecutorsId + ', ' + @DelResponsibleId
				SET @DelExecutorsValue = @DelExecutorsValue + ', ' + @DelResponsibleValue
			END
			
			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				SET @ExecutorsId = @ExecutorsId + ', ' + @ResponsibleId
				SET @ExecutorsValue = @ExecutorsValue + ', ' + @ResponsibleValue
			END
			
			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				SET @ExecutorsId = @ExecutorsId + ', ' + @ResponsibleId
				SET @ExecutorsValue = @ExecutorsValue + ', ' + @ResponsibleValue
			END
			
			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				SET @ExecutorsId = @ExecutorsId + ', ' + @ResponsibleId
				SET @ExecutorsValue = @ExecutorsValue + ', ' + @ResponsibleValue
			END
			
			-- Удаляем  старые Tasks
			
			UPDATE [dbo].[Tasks]
			SET [ModifiedUser] = @ModifiedUser
			WHERE [ActivityId] = @ActivityId AND [ExecutorId] IN (SELECT Value FROM [dbo].[minus](@DelExecutorsId, @ExecutorsId))
			
			DELETE FROM [dbo].[Tasks]
			WHERE [ActivityId] = @ActivityId AND [ExecutorId] IN (SELECT Value FROM [dbo].[minus](@DelExecutorsId, @ExecutorsId))
			
			-- Добавляем новые Tasks
			
			DELETE FROM @ExecutorsTableId
			INSERT INTO @ExecutorsTableId(Value, Type)
			SELECT Value, 0 FROM [dbo].[minus](@ExecutorsId, @DelExecutorsId)
			DELETE FROM @ExecutorsTableValue
			INSERT INTO @ExecutorsTableValue(Value)
			SELECT Value FROM [dbo].[minus](@ExecutorsValue, @DelExecutorsValue)
			
			UPDATE @ExecutorsTableId
			SET Type = 1 WHERE Value = @ResponsibleId

			SET @TaskCount = @TaskCount - @Delta
		
			INSERT INTO [dbo].[Tasks]
				([Text], [ExecutionDate], [CreatedDate], [AuthorId], [AuthorValue], [ExecutorId], [ExecutorValue], [DocumentId], [DocumentValue], [Type], [ActivityId], [Number], [FunctionType], [ModifiedUser], [IsMainLine], [TypeEx], [Stage])
			SELECT
				@Text, @ExecutionDate, @CreatedDate, @AuthorId, @AuthorValue, ids.Value, vls.Value, @DocumentId, @DocumentValue, @Type, @ActivityId, @TaskCount + ids.Id, 0, @ModifiedUser, ids.Type & @IsMainLine, 0, @TypeEx FROM @ExecutorsTableId as ids INNER JOIN @ExecutorsTableValue as vls ON ids.Id = vls.Id
			
			SET @Delta = (SELECT MAX(Id) FROM @ExecutorsTableId)
			if (@Delta is NULL)
				SET @Delta = 0
			
			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 0, [IsMainLine] = 0
			WHERE ActivityId = @ActivityId
			
			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 1, [IsMainLine] = @IsMainLine
			WHERE ActivityId = @ActivityId AND ExecutorId = @ResponsibleId
			
			SET @TaskCount = @TaskCount + @Delta
			
			UPDATE [dbo].[Settings]
			SET [Value] = @TaskCount
			WHERE UniqueName = 'TaskCount'
		END
		ELSE
		if (UPDATE(IsMainLine))
		begin 
		UPDATE [dbo].[Tasks]
			SET [FunctionType] = 0, [IsMainLine] = 0
			WHERE ActivityId = @ActivityId
			
			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 1, [IsMainLine] = @IsMainLine
			WHERE ActivityId = @ActivityId AND ExecutorId = @ResponsibleId
		end 
		if (update(ResponsibleId))
		BEGIN
			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 0
			WHERE ActivityId = @ActivityId

			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				INSERT INTO [dbo].[Tasks]
				([Text], [ExecutionDate], [CreatedDate], [AuthorId], [AuthorValue], [ExecutorId], [ExecutorValue], [DocumentId], [DocumentValue], [Type], [ActivityId], [Number], [FunctionType], [ModifiedUser], [TypeEx], [Stage])
				VALUES
				(@Text, @ExecutionDate, @CreatedDate, @AuthorId, @AuthorValue, @ResponsibleId, @ResponsibleValue, @DocumentId, @DocumentValue, @Type, @ActivityId, @TaskCount + 1, 0, @ModifiedUser, 0, @TypeEx)
				
				SET @TaskCount = @TaskCount + 1
			
				UPDATE [dbo].[Settings]
				SET [Value] = @TaskCount
				WHERE UniqueName = 'TaskCount'
			END

			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 0, [IsMainLine] = 0
			WHERE ActivityId = @ActivityId
			
			UPDATE [dbo].[Tasks]
			SET [FunctionType] = 1, [IsMainLine] = @IsMainLine
			WHERE ActivityId = @ActivityId AND ExecutorId = @ResponsibleId
		END
		
		if (update(Text))
			UPDATE [dbo].[Tasks]
			SET [Text] = @Text, [ModifiedUser] = @ModifiedUser
			WHERE ActivityId = @ActivityId
		
		if (update(ExecutionDate))
			UPDATE [dbo].[Tasks]
			SET [ExecutionDate] = @ExecutionDate, [ModifiedUser] = @ModifiedUser
			WHERE ActivityId = @ActivityId
		
		if (UPDATE(IsNotActive) and (SELECT IsNotActive from inserted)= 0 and (SELECT IsNotActive from deleted)= 1 )
		BEGIN
			if (CHARINDEX(@ResponsibleId, @ExecutorsId) = 0)
			BEGIN
				SET @ExecutorsId = @ExecutorsId + ', ' + @ResponsibleId
				SET @ExecutorsValue = @ExecutorsValue + ', ' + @ResponsibleValue
			END

			DELETE FROM @ExecutorsTableId
			INSERT INTO @ExecutorsTableId(Value, Type)
			SELECT Value, 0 FROM [dbo].[split](@ExecutorsId,', ')
			DELETE FROM @ExecutorsTableValue
			INSERT INTO @ExecutorsTableValue(Value)
			SELECT Value FROM [dbo].[split](@ExecutorsValue,', ')
			
			UPDATE @ExecutorsTableId
			SET Type = 1 WHERE Value = @ResponsibleId
			
			SET @TaskCount = @TaskCount - @Delta
		
			INSERT INTO [dbo].[Tasks]
				([Text], [ExecutionDate], [CreatedDate], [AuthorId], [AuthorValue], [ExecutorId], [ExecutorValue], [DocumentId], [DocumentValue], [Type], [ActivityId], [Number], [FunctionType], [ModifiedUser], [TypeEx], [Stage])
			SELECT
				@Text, @ExecutionDate, @CreatedDate, @AuthorId, @AuthorValue, ids.Value, vls.Value, @DocumentId, @DocumentValue, @Type, @ActivityId, @TaskCount + ids.Id, ids.Type, @ModifiedUser, 0, @TypeEx FROM @ExecutorsTableId as ids INNER JOIN @ExecutorsTableValue as vls ON ids.Id = vls.Id
			
			SET @Delta = (SELECT MAX(Id) FROM @ExecutorsTableId)
			if (@Delta is NULL)
				SET @Delta = 0
				
			SET @TaskCount = @TaskCount + @Delta
				
			UPDATE [dbo].[Settings]
			SET [Value] = @TaskCount
			WHERE UniqueName = 'TaskCount'
			
			if(@Type = 4)
				UPDATE [dbo].[Documents]
				SET StateType = 5
				WHERE Id = @DocumentId
		END
		
		

		FETCH NEXT FROM cur INTO	
			@Text,
			@ExecutionDate,
			@CreatedDate,
			@AuthorId,
			@AuthorValue,
			@DocumentId,
			@DocumentValue,
			@Type,
			@ActivityId,
			@ExecutorsId,
			@ExecutorsValue,
			@ResponsibleId,
			@ResponsibleValue,
			@IsNotActive,
			@ModifiedUser,
			@IsMainLine,
			@TypeEx
	END
	
	CLOSE cur
	DEALLOCATE cur

	--UPDATE doc
	--SET doc.ResponsibleId = [dbo].[trim]((SELECT CAST(ExecutorId as nvarchar(50)) + ', ' FROM Tasks WHERE 
	--						DocumentId = i.DocumentId and 
	--						Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId= i.DocumentId and ParentTask is not null) and
	--						IsMainLine = 1 and
	--						FunctionType = 1 for xml path('')), 2),
	--	doc.ResponsibleValue = [dbo].[trim]((SELECT [dbo].DisplayNameEmployee(ExecutorId) + ', ' FROM Tasks WHERE 
	--						DocumentId =i.DocumentId  and 
	--						Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId = i.DocumentId  and ParentTask is not null) and
	--						IsMainLine = 1 and
	--						FunctionType = 1  for xml path('')), 2)
	--from Documents as doc
	--left join inserted as i on i.DocumentId = doc.Id
	--WHERE  i.DocumentId is not null
END





GO

ALTER TABLE [dbo].[Activities] ENABLE TRIGGER [updateTasks_activities]
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[deleteTasks_activities]
   ON  [dbo].[Activities] 
   AFTER DELETE
AS 
BEGIN
	UPDATE Tasks 
	SET [ModifiedUser] = (SELECT ModifiedUser FROM deleted)
	WHERE ActivityId = Id

	UPDATE [dbo].[Documents]
	SET ResponsibleId = [dbo].[trim]((SELECT CAST(ExecutorId as nvarchar(50)) + ', ' FROM Tasks WHERE 
							DocumentId IN (SELECT DocumentId FROM deleted) and 
							Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId IN (SELECT DocumentId FROM deleted) and ParentTask is not null) and
							IsMainLine = 1 and
							FunctionType = 1 for xml path('')), 2),
		ResponsibleValue = [dbo].[trim]((SELECT ExecutorValue + ', ' FROM Tasks WHERE 
							DocumentId IN (SELECT DocumentId FROM deleted) and 
							Id NOT IN (SELECT ParentTask FROM Activities WHERE DocumentId IN (SELECT DocumentId FROM deleted) and ParentTask is not null) and
							IsMainLine = 1 and
							FunctionType = 1  for xml path('')), 2)
	WHERE Id IN (SELECT DocumentId FROM deleted)
END




GO

ALTER TABLE [dbo].[Activities] ENABLE TRIGGER [deleteTasks_activities]
GO


------ Tasks


-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <19.07.2011>
-- Description:	<Триггер на INSERT для Tasks>
-- =============================================
CREATE TRIGGER [dbo].[insert_tasks]
   ON  [dbo].[Tasks] 
   AFTER INSERT
AS 
BEGIN
	INSERT INTO [dbo].[AccessTasks]
        (UserId ,ObjectId ,PropertyName)
		(SELECT ExecutorId, Id, 'Tasks.ExecutorId' FROM inserted)
		
	INSERT INTO [dbo].[AccessDocuments]
        (UserId ,ObjectId ,PropertyName)
		(SELECT ExecutorId, DocumentId, 'Tasks.ExecutorId' FROM inserted)

	DECLARE @Ids NVARCHAR(MAX) 
	SELECT @Ids = (SELECT STUFF((SELECT ';' + cast(Id as nvarchar(max)) FROM inserted FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''))

	--EXEC [dbo].[SendEmail] @taskId = @Ids
END


GO

ALTER TABLE [dbo].[Tasks] ENABLE TRIGGER [insert_tasks]
GO


-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <19.07.2011>
-- Description:	<Триггер на UPDATE для Tasks>
-- =============================================
CREATE TRIGGER [dbo].[update_tasks]
   ON  [dbo].[Tasks] 
   AFTER UPDATE
AS 
BEGIN
	if (update([ExecutorId]))
	BEGIN
		DELETE FROM [dbo].[AccessTasks]
			WHERE ObjectId IN (SELECT d.Id FROM deleted as d)
		
		DELETE FROM [dbo].[AccessDocuments]
			WHERE ObjectId IN (SELECT d.DocumentId FROM deleted as d) and PropertyName = 'Tasks.ExecutorId'
			
		INSERT INTO [dbo].[AccessTasks]
			(UserId ,ObjectId ,PropertyName)
			(SELECT i.ExecutorId, i.Id, 'Tasks.ExecutorId' FROM inserted as i)
		
		INSERT INTO [dbo].[AccessDocuments]
			(UserId ,ObjectId ,PropertyName)
			(SELECT i.ExecutorId, i.DocumentId, 'Tasks.ExecutorId' FROM inserted as i)
	END
	--Активация этапов согласования
		if (update([State]) AND (SELECT top 1 i.[State] FROM inserted as i) = 2 AND ((SELECT top 1 i.[Type] FROM inserted as i) = 3 OR (SELECT top 1 i.[Type] FROM inserted as i) = 4 OR (SELECT top 1 i.[Type] FROM inserted as i) = 6) AND (SELECT top 1 StateType FROM Documents WHERE Id in (SELECT top 1 DocumentId FROM inserted)) <> 6)
		if ((SELECT COUNT(*) FROM Tasks as t WHERE t.ActivityId IN (SELECT i.ActivityId FROM inserted as i) AND (t.State != 2)) = 0)
			UPDATE [dbo].[Activities]
			SET IsNotActive = 0
			WHERE DocumentId in (SELECT DocumentId FROM inserted) AND 
				(ParentId in (SELECT ActivityId FROM inserted))
	
	----Исполнение заданий при ответе на служебку
	--if (update([State]) AND (SELECT [State] FROM inserted) = 2 AND (SELECT [Type] FROM inserted) != 3 AND (SELECT [Type] FROM inserted) != 4 AND (SELECT [MainDocumentId] FROM Documents WHERE (Id IN (SELECT [DocumentId] FROM inserted))) is not null )
	--BEGIN
	--	UPDATE [dbo].[Tasks]
	--	SET State = 2
	--	WHERE Id = (SELECT [MainTaskId] FROM Documents WHERE (Id = (SELECT [DocumentId] FROM inserted)))
	--END
	
	----Исполнение Дерева Заданий вверх по линии ответственного
	--if (update([State]) AND (SELECT [State] FROM inserted) = 2 AND (SELECT [Type] FROM inserted) != 3 AND (SELECT [Type] FROM inserted) != 4 AND (SELECT [FunctionType] FROM inserted) = 1)
	--BEGIN
	--	--Исполнение Заданий вверх по линии ответственного
	--	UPDATE [dbo].[Tasks]
	--	SET State = 2
	--	WHERE Id = (SELECT ParentTask FROM Activities WHERE Id = (SELECT ActivityId FROM inserted)) and Type <> 3 and Type <> 4
		
	--	if ((SELECT ParentTask FROM Activities WHERE Id = (SELECT ActivityId FROM inserted)) is null)
	--	--Исполнение документа вверх
	--	BEGIN
	--		UPDATE [dbo].[Documents]
	--		SET StateType = 9, ModifiedUser = 'Система'
	--		WHERE (Id = (SELECT i.DocumentId FROM inserted as i))
	--	END
	--	--Создание авто-отчета
	--	ELSE
	--	BEGIN
	--		if ((SELECT Type FROM Tasks WHERE Id=(SELECT ParentTask FROM Activities WHERE Id = (SELECT ActivityId FROM inserted))) <> 3 and (SELECT Type FROM Tasks WHERE Id=(SELECT ParentTask FROM Activities WHERE Id = (SELECT ActivityId FROM inserted))) <> 4)
	--			INSERT INTO [dbo].[Reports] ([Id], [Type], [Text], [AnswersId], [AnswersValue], [ExecutionDate], [DocumentId], [TaskId], [ModifiedUser])
	--			VALUES
	--		   (NEWID(), 0, 'Задание закрыто системой автоматически'  ,null, null, GETDATE(), (SELECT i.DocumentId FROM inserted as i), (SELECT ParentTask FROM Activities WHERE Id = (SELECT ActivityId FROM inserted)), 'Система')
	--	END
	--END
	
	----Заполнение ответсвтенного исполнителя по линии отвественного
	--if (UPDATE([State]) AND ((SELECT [State] FROM inserted) = 4) AND (SELECT [IsMainLine] FROM inserted) = 1 AND (SELECT [Type] FROM inserted) != 3 AND (SELECT [Type] FROM inserted) != 4)
	--	UPDATE [dbo].[Documents]
	--	SET ResponsibleId = (SELECT ExecutorId FROM inserted), ResponsibleValue = (SELECT ExecutorValue FROM inserted)
	--	WHERE (Id = (SELECT DocumentId FROM inserted))
END


GO

ALTER TABLE [dbo].[Tasks] ENABLE TRIGGER [update_tasks]
GO



-- =============================================
-- Author:		<Kairat V. Beysenov>
-- Create date: <19.07.2011>
-- Description:	<Триггер на DELETE для Tasks>
-- =============================================
CREATE TRIGGER [dbo].[delete_tasks]
   ON  [dbo].[Tasks] 
   AFTER DELETE
AS 
BEGIN
	DELETE FROM [dbo].[AccessTasks]
	WHERE ObjectId IN (SELECT Id FROM deleted)
	
	DELETE FROM [dbo].[AccessDocuments]
	WHERE ObjectId IN (SELECT DocumentId FROM deleted) and PropertyName = 'Tasks.ExecutorId'
	
	DELETE FROM [dbo].[Activities]
	WHERE ParentTask IN (SELECT Id FROM deleted)
	
	--DELETE FROM [dbo].[Documents]
	--WHERE MainTaskId IN (SELECT Id FROM deleted)
END


GO

ALTER TABLE [dbo].[Tasks] ENABLE TRIGGER [delete_tasks]
GO


