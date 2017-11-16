ALTER TABLE OBK_StageExpDocument ADD RefReasonId int null
ALTER TABLE OBK_StageExpDocument WITH CHECK ADD CONSTRAINT FK_OBK_StageExpDocument_OBK_Ref_Reason 
FOREIGN KEY([RefReasonId]) REFERENCES OBK_Ref_Reason ([Id])
