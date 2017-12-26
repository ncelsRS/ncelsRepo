alter table OBK_Taskmaterial add ExpertiseResult bit null
alter table OBK_TaskMaterial add Regulation nvarchar (255) null

alter table OBK_TaskExecutor add SignedData nvarchar(1024) null
ALTER TABLE OBK_TaskExecutor
	ADD	[TaskMaterialId] uniqueidentifier NULL,
		 CONSTRAINT FK_OBK_TaskExecutor_TaskMaterialId_OBK_TaskMaterial_Id
		 FOREIGN KEY ([TaskMaterialId])
		 REFERENCES [OBK_TaskMaterial]([Id])