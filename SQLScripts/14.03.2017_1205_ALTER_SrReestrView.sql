﻿SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[SrReestrView]
AS
with d_sr_r_boxes(register_id, box_id, inner_sign, volume, volume_measure_id, unit_count, box_size, state_date) as (
	select 
		distinct register_id, box_id, inner_sign, volume, volume_measure_id, unit_count, box_size, state_date 
	from sr_register_boxes as b left join sr_register_boxes_rk_ls AS sr_r_b_kl ON sr_r_b_kl.id = b.id
)
select cast(ROW_NUMBER() OVER (ORDER BY id) AS int) AS n, * from(
	SELECT   
		DISTINCT 
			sr_r.id
			, sr_r._producer_name_kz
			, sr_r._producer_name
			, sr_r._country_name
			, dic_c.Id AS _country_Id
			, sr_r.name_kz
			, sr_r.name
			, sr_r.reg_number
			, sr_r.reg_date
			, sr_r_d._int_name
			, (
			SELECT SUBSTRING((	
				SELECT        
					', ' + sr_substances.name AS 'data()'
				FROM
					sr_substances
				WHERE        
					sr_substances.id IN (
						SELECT        
							sr_register_substances.substance_id
						FROM            
							sr_register_substances
						WHERE        
							sr_register_substances.register_id = sr_r.id) 
						FOR XML PATH('')), 3, 1000)
				) AS SubstanceName
				, sr_r_d._dosage_form_name
				, CAST(CAST(CAST(sr_r_d.dosage_value AS DECIMAL(17,5)) AS FLOAT) AS nvarchar)  + ' ' + ISNULL(sm_d.short_name, '') AS dosage_value
				, sr_r_d.concentration
				, sr_r_d._atc_code
				, CASE 
					WHEN sr_r_d.generic_sign = 0 THEN N'Нет признака' 
					WHEN sr_r_d.generic_sign IS NULL THEN N'Нет признака' 
					WHEN sr_r_d.generic_sign = 1 THEN N'Признак генерик' 
				END AS description
				, dic_v.Name AS um
				, dic_v.Id AS umId
				, sr_r.expiration_date
				, sr_r.reg_type_id AS type
				, CAST(CAST(CAST(sr_r_b.volume AS DECIMAL(17,5)) AS FLOAT) AS nvarchar)  + ' ' + ISNULL(sr_m.name, '') AS volume
				, sr_m.name AS volume_measure
				, sr_prod.name_kz AS owner_name_kz
				, sr_prod.name AS owner_name_ru
				, sr_prod.name_eng AS owner_name_en
				, sr_d_f.pr_box_count + ' ' + sr_b1.name AS box_name1
				, sr_d_f.sec_box_count + ' ' + sr_b2.name AS box_name2
				, sr_d_f.box_count
		FROM            
			dbo.sr_register AS sr_r 
			LEFT JOIN dbo.sr_register_drugs AS sr_r_d ON sr_r_d.id = sr_r.id 
			LEFT JOIN d_sr_r_boxes AS sr_r_b ON sr_r_b.register_id = sr_r.id 
			LEFT JOIN dbo.sr_measures AS sr_m ON sr_m.id = sr_r_b.volume_measure_id 
			LEFT JOIN dbo.Dictionaries AS dic_c ON sr_r._country_name = dic_c.Name 
			LEFT JOIN dbo.sr_register_use_methods AS sr_r_um ON sr_r_um.register_id = sr_r.id 
			LEFT JOIN dbo.sr_use_methods AS sr_um ON sr_um.id = sr_r_um.use_method_id 
			LEFT JOIN dbo.Dictionaries AS dic_v ON sr_um.name = dic_v.Name 
			LEFT JOIN sr_register_producers AS sr_r_prod ON sr_r_prod.register_id = sr_r.id 
				AND sr_r_prod.producer_type_id = 8 
			LEFT JOIN sr_producers AS sr_prod ON sr_r_prod.producer_id = sr_prod.id 
			LEFT JOIN sr_drug_forms AS sr_d_f on sr_d_f.register_id=sr_r.id 
			LEFT JOIN sr_register_boxes AS sr_r_b1 on sr_r_b1.id=sr_d_f.pr_box_id 
			LEFT JOIN sr_register_boxes AS sr_r_b2 on sr_r_b2.id=sr_d_f.sec_box_id 
			LEFT JOIN sr_boxes AS sr_b1 on sr_r_b1.box_id = sr_b1.id 
			LEFT JOIN sr_boxes AS sr_b2 on sr_r_b2.box_id = sr_b2.id 
			LEFT JOIN sr_measures AS sm_d on sr_r_d.dosage_measure_id=sm_d.id
	WHERE        
		(sr_r.expiration_date > GETDATE()) 
		AND (
			state_date > GETDATE() 
			OR state_date IS NULL
		)
) AS t_all		 

GO


