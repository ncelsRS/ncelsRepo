CREATE VIEW OBKContractProductsView
AS
select
	(case RegTypeId
	when 1
	then NameRu
	when 2
	then NameRu + ' ' + 
(select s.pName from 
(select ProductId, (
	select a.Name + N', '
				from dbo.OBK_MtPart AS a 
				where mp.ProductId = a.ProductId 
				for xml path('')) as pName
		from OBK_MtPart as mp where mp.ProductId = p.Id  
		group by ProductId) as s)
	end) as NameRu,
			 p.Id as Id,
			 ps.OBK_RS_ProductsId as OBK_RS_ProductsId,
			 ps.Id as ProdSeriesId,
			 ps.Series as ProdSeries,
			 ps.SeriesEndDate as ProdSeriesEndDate, 
			 SeriesParty as ProdSeriesParty, 
			 p.CountryNameRu as ProdCountryNameRu, 
			 p.ProducerNameRu as ProdProducerNameRu, 
			 s.short_name as ProdShortName,
			 p.ContractId as ContractId
from OBK_RS_Products p inner join OBK_Procunts_Series ps on p.Id = ps.OBK_RS_ProductsId
inner join OBK_Contract c on c.Id = p.ContractId
inner join sr_measures s on ps.SeriesMeasureId = s.id