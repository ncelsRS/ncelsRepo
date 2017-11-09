SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[RegisterOrderer2Views]
AS
SELECT 
	ROW_NUMBER() OVER (ORDER BY srd.id) AS IntId, 
	newid() AS Id, 
	srd.id AS regId, 
	srd._int_name,
    (SELECT
		sr_substances.name + '/ ' AS 'data()'
     FROM
		sr_substances
     WHERE
		sr_substances.id IN (SELECT sr_register_substances.substance_id
								FROM sr_register_substances
                                WHERE sr_register_substances.register_id = srd.id) FOR XML PATH('')) 
		AS substance, 
	(cast(srb.volume AS nvarchar) + ' ' + sm.name) AS volume, 
	srd.dosage_comment,
	sr.name, 
	srd._dosage_form_name, 
	srd.concentration, 
	sr.reg_number, 
	sr._producer_name, 
	sr._country_name, 
	srd._atc_code, 
	(cast(srd.dosage_value AS nvarchar) + ' ' + sm_d.short_name) as dosage_value, 
	srb.unit_count,
	sr_d_f.pr_box_count + ' ' + sr_b1.name as box_name1,
	sr_d_f.sec_box_count + ' ' + sr_b2.name as box_name2,
	sr_d_f.box_count
FROM            
	[ncels].[dbo].[sr_register_drugs] AS srd LEFT JOIN
	sr_register_boxes AS srb ON srb.register_id = srd.id LEFT JOIN
	sr_register AS sr ON sr.id = srd.id LEFT JOIN
	sr_measures AS sm ON sm.id = srb.volume_measure_id LEFT OUTER JOIN
	dbo.sr_register_boxes_rk_ls AS sr_r_b_kl ON sr_r_b_kl.id = srb.id LEFT OUTER JOIN
	dbo.sr_register AS sr_r ON sr_r.id = srd.id left outer join 
	sr_drug_forms as sr_d_f on sr_d_f.register_id=sr_r.id left join 
	sr_register_boxes as sr_r_b1 on sr_r_b1.id=sr_d_f.pr_box_id left join 
	sr_register_boxes as sr_r_b2 on sr_r_b2.id=sr_d_f.sec_box_id left join 
	sr_boxes as sr_b1 on sr_r_b1.box_id = sr_b1.id left join 
	sr_boxes as sr_b2 on sr_r_b2.box_id = sr_b2.id left join 
	sr_measures AS sm_d on srd.dosage_measure_id=sm_d.id
WHERE
	(sr_r.expiration_date > GETDATE()) AND (state_date > getdate() OR state_date IS NULL)
						 
GO


