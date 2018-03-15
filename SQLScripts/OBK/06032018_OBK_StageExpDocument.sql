ALTER TABLE OBK_StageExpDocument ADD DecisionRefuse BIT NULL
  GO
UPDATE OBK_StageExpDocument SET DecisionRefuse=0
  GO
ALTER TABLE OBK_StageExpDocument ALTER COLUMN DecisionRefuse BIT NOT NULL 
  GO