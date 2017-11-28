USE [ncels]
GO

INSERT INTO [dbo].[Dictionaries]
           ([Id]
			,[Name]
           ,[Type]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           (newid()
           ,'�� ��'
           ,'ProductSample'
           ,'�� ��'
           ,0)
GO

INSERT INTO [dbo].[Dictionaries]
           ([Id]
			,[Name]
           ,[Type]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           (newid()
           ,'�� �����'
           ,'ProductSample'
           ,'�� ��'
           ,0)
GO

INSERT INTO [dbo].[Dictionaries]
           ([Id]
			,[Name]
           ,[Type]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           (newid()
           ,'��������'
           ,'ProductSample'
           ,'�� ��'
           ,0)
GO






USE [ncels]
GO



INSERT INTO [dbo].[OBK_Dictionaries]
           ([Id]
           ,[Type]
           ,[Name]
           ,[NameKz]
           ,[CreateDate]
           ,[ExpireDate])
     VALUES
           (NEWID()
           ,'InspectionInstalled'
           ,N'(��������) ��������� ���������������� �� ��������, ����������, ���� �����, ���������������������� ����������, ��������� �������� � �������� ��������� ���������'
           ,N'(��������) ��������� ���������������� �� ��������, ����������, ���� �����, ���������������������� ����������, ��������� �������� � �������� ��������� ���������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'InspectionInstalled'
           ,N'��������� ���������������� �� ��������, ����������,  ���������������������� ����������'
           ,N'��������� ���������������� �� ��������, ����������,  ���������������������� ����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'InspectionInstalled'
           ,N'��������� ���������������� �� ��������, ����������, ���� �����'
           ,N'��������� ���������������� �� ��������, ����������, ���� �����'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'InspectionInstalled'
           ,N'��������� �������� �� �������� ������ �� ���������� ������'
           ,N'��������� �������� �� �������� ������ �� ���������� ������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'InspectionInstalled'
           ,N'��������� �������� �� �������� ������ �� ���������� ������, � �������� ��������� ���������, ���������������� �� ��������, ����������'
           ,N'��������� �������� �� �������� ������ �� ���������� ������, � �������� ��������� ���������, ���������������� �� ��������, ����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'InspectionInstalled'
           ,N'��������� �������� �� ������ �� ���������� ������, � �������� ��������� ���������, ���������������� �� ��������, ����������'
           ,N'��������� �������� �� ������ �� ���������� ������, � �������� ��������� ���������, ���������������� �� ��������, ����������'
           ,GETDATE()
           ,null),
		   
           (NEWID()
           ,'StorageConditions'
           ,N'� ����� ����� � ����������� �������������� ������'
           ,N'� ����� ����� � ����������� �������������� ������'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'��������� � ������������� ����� ��������������'
           ,N'��������� � ������������� ����� ��������������'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� �������� � ����� ������������� � � ���������� �� �����  ���������, ��������� � ������������� �����'
           ,N'�� �������� � ����� ������������� � � ���������� �� �����  ���������, ��������� � ������������� �����'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� �������� � ����� ������������� ���������'
           ,N'�� �������� � ����� ������������� ���������'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� �������� � ����� ������������� ���������, ������������� ����������� ������� �� � � �� �238 �� 10.'
           ,N'�� �������� � ����� ������������� ���������, ������������� ����������� ������� �� � � �� �238 �� 10.'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� ��������, ��������� � ������������� ����� ��������������, ����������� �� +15 �� +25 �'
           ,N'�� ��������, ��������� � ������������� ����� ��������������, ����������� �� +15 �� +25 �'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� ����, ��������� � ������������� ����� �� ��������������,'
           ,N'�� ����, ��������� � ������������� ����� �� ��������������,'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� ��������� � ����� ������������� ���������, � ���������� �� ����� ����� � ����������� �������������� ������'
           ,N'�� ��������� � ����� ������������� ���������, � ���������� �� ����� ����� � ����������� �������������� ������'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'�� ���������, ��������� � ������������� ����� ��������������'
           ,N'�� ���������, ��������� � ������������� ����� ��������������'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'��� ����������� �� 2 �������� �� 8 �'
           ,N'��� ����������� �� 2 �������� �� 8 �'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'������������� ����������� ������� �� � � �� �238 �� 10.05.1999 �.'
           ,N'������������� ����������� ������� �� � � �� �238 �� 10.05.1999 �.'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'StorageConditions'
           ,N'������������� �����, ��������� �����������, �������� �� �������� � ����� ���������'
           ,N'������������� �����, ��������� �����������, �������� �� �������� � ����� ���������'
           ,GETDATE()
           ,null),
		   
		   (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� �������� ������������������'
           ,N'��� ��������, ��������, ��������� �������� ������������������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� �������� ������������������, � ����������� ���������� ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,N'��� ��������, ��������, ��������� �������� ������������������, � ����������� ���������� ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� �������� ������������������, ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,N'��� ��������, ��������, ��������� �������� ������������������, ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� �������� ������������������, ������ � ����������� �� ���������� �� �����. � ������� ������'
           ,N'��� ��������, ��������, ��������� �������� ������������������, ������ � ����������� �� ���������� �� �����. � ������� ������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� ������� ������������������, �������� ��������� � ��������� � ����������� �� ���. � ���. ������, ������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������, ������� ��������� � ��������� �������'
           ,N'��� ��������, ��������, ��������� ������� ������������������, �������� ��������� � ��������� � ����������� �� ���. � ���. ������, ������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������, ������� ��������� � ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��� ��������, ��������, ��������� ������� ������������������. �������� ��������� � ��������� � ����������� �� ���. � ���. ������'
           ,N'��� ��������, ��������, ��������� ������� ������������������. �������� ��������� � ��������� � ����������� �� ���. � ���. ������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'� ����������� ���������� ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,N'� ����������� ���������� ������ � ����������� �� ���������� �� �����. � ����. ������ �������� � ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'� ������������ � ���� 17768 - 90� - �������� �� �������� �� ������ � ������������� ������������� �������, ��� ��������, ��������, ��������� �������� ������������������'
           ,N'� ������������ � ���� 17768 - 90� - �������� �� �������� �� ������ � ������������� ������������� �������, ��� ��������, ��������, ��������� �������� ������������������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'� ������������ � ���� 17768 - 90� - �������� �� �������� �� ������ � ������������� ������������� �������, ����� �������� �������� � ���� ��������� �������'
           ,N'� ������������ � ���� 17768 - 90� - �������� �� �������� �� ������ � ������������� ������������� �������, ����� �������� �������� � ���� ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'� ������. �������������� ��������� ����������� ����������� ������, ����������� ���������� ������� � ������� ����������� ����������'
           ,N'� ������. �������������� ��������� ����������� ����������� ������, ����������� ���������� ������� � ������� ����������� ����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'������ � ����������� �� ���������� �� �����. � ������� ��. ������� � ������� �� �������.'
           ,N'������ � ����������� �� ���������� �� �����. � ������� ��. ������� � ������� �� �������.'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� � ������������ �������� � ������������ � ���� 17768 - 90�'
           ,N'��������� � ������������ �������� � ������������ � ���� 17768 - 90�'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'������������ ������ ��������� �� ������� �����������'
           ,N'������������ ������ ��������� �� ������� �����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'�� __ ���������� � ��������� ��������� �������� �� ������������������ ������ � ����������� � ��� ������ �����������'
           ,N'�� __ ���������� � ��������� ��������� �������� �� ������������������ ������ � ����������� � ��� ������ �����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'�� 5 ���������� � ��������� ��������� �������� �� ������������������ ������ � ����������� � ��� ������ �����������'
           ,N'�� 5 ���������� � ��������� ��������� �������� �� ������������������ ������ � ����������� � ��� ������ �����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'�������� � �������������� ������� � ���������'
           ,N'�������� � �������������� ������� � ���������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ��������� �  ��������� �������'
           ,N'��������� ��������� �  ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ��������� � �������������� ������'
           ,N'��������� ��������� � �������������� ������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ��������� � �������������� ������ � ��������� �������'
           ,N'��������� ��������� � �������������� ������ � ��������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ��������� � �������������� ������ � ��������� �������, ��� ��������, ��������, ��������� �������� ������������������'
           ,N'��������� ��������� � �������������� ������ � ��������� �������, ��� ��������, ��������, ��������� �������� ������������������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ��������� ������ � ������������ �� ���������� �� ��������������� � ������� ������'
           ,N'��������� ��������� ������ � ������������ �� ���������� �� ��������������� � ������� ������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� � ������������� ������� � ������������� �������'
           ,N'��������� � ������������� ������� � ������������� �������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'��������� ������������ ��������� ��������������������; �� ��������� � ��������. ������ ������� ����������� ����������� ���������, � ����� �������� ���������� �����'
           ,N'��������� ������������ ��������� ��������������������; �� ��������� � ��������. ������ ������� ����������� ����������� ���������, � ����� �������� ���������� �����'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������, ������� ��������� � ��������� ���������'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� � ������������ ����������, ������� ��������� � ��������� ���������'
           ,GETDATE()
           ,null),

           (NEWID()
           ,'PackageCondition'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� ����������, ������� ��������� � ��������� ���������, ��������� ��������� ������ � ������������ �� ���������� �� ��������������� � ������� ������'
           ,N'������� �� ����������� ������, ����������� �������� � ������������� ����������, ������� ��������� � ��������� ���������, ��������� ��������� ������ � ������������ �� ���������� �� ��������������� � ������� ������'
           ,GETDATE()
           ,null),
		   
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ���� 17768-90� � �� �����'
           ,N'� ������������ � ���� 17768-90� � �� �����'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ���� 24861-91'
           ,N'� ������������ � ���� 24861-91'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ���� 24861-91'
           ,N'� ������������ � ���� 24861-91'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ���� 25047-87, ���� 24861-91'
           ,N'� ������������ � ���� 25047-87, ���� 24861-91'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ���� � �� �����'
           ,N'� ������������ � ���� � �� �����'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� ��'
           ,N'� ������������ � �� ��'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � ��'
           ,N'� ������������ � ��'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� �����'
           ,N'� ������������ � �� �����'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� 9393-046-17121966-2002'
           ,N'� ������������ � �� 9393-046-17121966-2002'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� 9398-002-56257679-2004'
           ,N'� ������������ � �� 9398-002-56257679-2004'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� 9432-003-52876351-2003'
           ,N'� ������������ � �� 9432-003-52876351-2003'
           ,GETDATE()
           ,null), 
           
		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� � ����'
           ,N'� ������������ � �� � ����'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� �6-00152253.014-96'
           ,N'� ������������ � �� �6-00152253.014-96'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'Marking'
           ,N'� ������������ � �� �����'
           ,N'� ������������ � �� �����'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'Marking'
           ,N'���� 1172-93'
           ,N'���� 1172-93'
           ,GETDATE()
           ,null),

		   (NEWID()
           ,'Marking'
           ,N'���� ISO 4090-2011'
           ,N'���� ISO 4090-2011'
           ,GETDATE()
           ,null),

		   
		   (NEWID()
           ,'Marking'
           ,N'�������������'
           ,N'�������������'
           ,GETDATE()
           ,null)

		   
GO



USE [ncels]
GO

INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[DisplayName]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           ( NEWID ()  
           ,2
           ,N'������� ������ / ���������� GDP'
           ,'OBK_PARTY_SAFETY_ASSESSMENT'
           ,N'������� ������ / ���������� GDP'
           ,N'������� ������ / ���������� GDP'
           ,0)



INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[DisplayName]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           ( NEWID ()  
           ,2
           ,N'��� ������'
           ,'OBK_PARTY_SAFETY_ASSESSMENT'
           ,N'��� ������'
           ,N'��� ������'
           ,0)


GO