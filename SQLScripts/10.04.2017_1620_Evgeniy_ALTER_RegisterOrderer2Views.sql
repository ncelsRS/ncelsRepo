USE [ncels]
GO

/****** Object:  View [dbo].[RegisterOrderer2Views]    Script Date: 10.04.2017 16:14:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[RegisterOrderer2Views]
AS
SELECT   
		cast(ROW_NUMBER() OVER (ORDER BY sr_r.id) AS int) AS IntId
		, newid() AS Id
		, sr_r.id as regId
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
			) AS substance
		, CAST(CAST(CAST(sr_r_b1.volume AS DECIMAL(17,5)) AS FLOAT) AS nvarchar)  + ' ' + ISNULL(sr_m.name, '') AS volume
		, sr_r_d.dosage_comment
		, sr_r.name
		, sr_r_d._dosage_form_name
		, sr_r_d.concentration
		, sr_r.reg_number
		, sr_p.name as _producer_name
		, sr_r._country_name
		, sr_r_d._atc_code
		, CAST(CAST(CAST(sr_r_d.dosage_value AS DECIMAL(17,5)) AS FLOAT) AS nvarchar)  + ' ' + ISNULL(sm_d.short_name, '') AS dosage_value
		, dic_v.Name AS um
		, sr_d_f.pr_box_count + ' ' + sr_b1.name AS box_name1
		, sr_d_f.sec_box_count + ' ' + sr_b2.name AS box_name2
		, sr_d_f.box_count
		, sr_d_f.id as dfId
	FROM            
		dbo.sr_register AS sr_r 
		LEFT JOIN dbo.sr_register_drugs AS sr_r_d ON sr_r_d.id = sr_r.id 
		LEFT JOIN dbo.Dictionaries AS dic_c ON sr_r._country_name = dic_c.Name 
		LEFT JOIN dbo.sr_register_use_methods AS sr_r_um ON sr_r_um.register_id = sr_r.id 
		LEFT JOIN dbo.sr_use_methods AS sr_um ON sr_um.id = sr_r_um.use_method_id 
		LEFT JOIN dbo.Dictionaries AS dic_v ON sr_um.name = dic_v.Name 
		LEFT JOIN sr_register_producers AS sr_r_prod ON sr_r_prod.register_id = sr_r.id 
			AND sr_r_prod.producer_type_id = 8 
		LEFT JOIN sr_countries as sr_c ON sr_r_prod.country_id = sr_c.id
		LEFT JOIN dbo.Dictionaries AS dic_c_owner ON sr_c.name = dic_c_owner.Name 
		LEFT JOIN sr_producers AS sr_prod ON sr_r_prod.producer_id = sr_prod.id 
		LEFT JOIN sr_drug_forms AS sr_d_f on sr_d_f.register_id=sr_r.id 
		LEFT JOIN sr_register_boxes AS sr_r_b1 on sr_r_b1.id=sr_d_f.pr_box_id 
			AND sr_r_b1.register_id = sr_r.id
		LEFT JOIN sr_register_boxes_rk_ls AS sr_r_b_kl1 ON sr_r_b_kl1.id = sr_r_b1.id
		LEFT JOIN sr_measures AS sr_m on sr_r_b1.volume_measure_id=sr_m.id
		LEFT JOIN sr_register_boxes AS sr_r_b2 on sr_r_b2.id=sr_d_f.sec_box_id 
			AND sr_r_b2.register_id = sr_r.id
		LEFT JOIN sr_register_boxes_rk_ls AS sr_r_b_kl2 ON sr_r_b_kl2.id = sr_r_b1.id
		LEFT JOIN sr_boxes AS sr_b1 on sr_r_b1.box_id = sr_b1.id 
		LEFT JOIN sr_boxes AS sr_b2 on sr_r_b2.box_id = sr_b2.id 
		LEFT JOIN sr_measures AS sm_d on sr_r_d.dosage_measure_id=sm_d.id
		INNER JOIN sr_register_producers as sr_r_p on sr_r.id=sr_r_p.register_id
			AND sr_r_p.producer_type_id = 1
		INNER JOIN sr_producers as sr_p on sr_r_p.producer_id = sr_p.id
WHERE        
	(sr_r.expiration_date > GETDATE()) 
	AND (
		sr_r_b_kl1.state_date IS NULL
	)
	AND (
		sr_r_b_kl2.state_date IS NULL
	)
						 

GO


