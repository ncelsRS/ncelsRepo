IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SeriesMeasureId'
          AND Object_ID = Object_ID(N'dbo.OBK_Procunts_Series'))
BEGIN
    ALTER TABLE OBK_Procunts_Series
	ADD SeriesMeasureId BIGINT NULL,
	CONSTRAINT FK_OBK_Procunts_Series_SeriesMeasureId__sr_measures_id
	FOREIGN KEY ([SeriesMeasureId])
	REFERENCES [sr_measures]([id])
END