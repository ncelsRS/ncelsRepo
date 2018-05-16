SET ANSI_NULLS ON
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
SELECT 
	cast(ROW_NUMBER() OVER (ORDER BY sr_r.id) AS int) AS id, 
	sr_r._producer_name_kz, 
	sr_r._producer_name, 
	sr_r._country_name, 
	dic_c.Id AS _country_Id, 
	sr_r.name_kz, 
	sr_r.name, 
	sr_r.reg_number, 
	sr_r.reg_date, 
	sr_r_d._int_name,
    (SELECT        sr_substances.name + '/ ' AS 'data()'
      FROM            sr_substances
      WHERE        sr_substances.id IN
                                    (SELECT        sr_register_substances.substance_id
                                      FROM            sr_register_substances
                                      WHERE        sr_register_substances.register_id = sr_r.id) FOR XML PATH('')) AS SubstanceName, 
	sr_r_d._dosage_form_name, 
	sr_r_d.dosage_value, 
	sr_r_d.concentration, 
	sr_r_d._atc_code, 
	sr_r_b.unit_count, 
	CASE WHEN sr_r_d.generic_sign = 0 THEN 'Нет признака' WHEN sr_r_d.generic_sign IS NULL THEN 'Нет признака' WHEN sr_r_d.generic_sign = 1 THEN 'Признак генерик' END AS description,
	dic_v.Name AS um, 
	dic_v.Id AS umId, 
	sr_r.expiration_date, 
	sr_r.reg_type_id AS type, 
	sr_r_b.volume, 
	sr_m.name AS volume_measure, 
	sr_prod.name_kz AS owner_name_kz, 
	sr_prod.name AS owner_name_ru, 
	sr_prod.name_eng AS owner_name_en,
	sr_d_f.pr_box_count + ' ' + sr_b1.name as box_name1,
	sr_d_f.sec_box_count + ' ' + sr_b2.name as box_name2,
	sr_d_f.box_count
FROM            dbo.sr_register AS sr_r LEFT OUTER JOIN
                         dbo.sr_register_drugs AS sr_r_d ON sr_r_d.id = sr_r.id LEFT OUTER JOIN
                         d_sr_r_boxes AS sr_r_b ON sr_r_b.register_id = sr_r.id LEFT OUTER JOIN
                         --dbo.sr_register_boxes_rk_ls AS sr_r_b_kl ON sr_r_b_kl.id = sr_r_b.id LEFT OUTER JOIN
                         dbo.sr_measures AS sr_m ON sr_m.id = sr_r_b.volume_measure_id LEFT OUTER JOIN
                         dbo.Dictionaries AS dic_c ON sr_r._country_name = dic_c.Name LEFT OUTER JOIN
                         dbo.sr_register_use_methods AS sr_r_um ON sr_r_um.register_id = sr_r.id LEFT OUTER JOIN
                         dbo.sr_use_methods AS sr_um ON sr_um.id = sr_r_um.use_method_id LEFT OUTER JOIN
                         dbo.Dictionaries AS dic_v ON sr_um.name = dic_v.Name LEFT OUTER JOIN
                         sr_register_producers AS sr_r_prod ON sr_r_prod.register_id = sr_r.id AND sr_r_prod.producer_type_id = 8 LEFT OUTER JOIN
                         sr_producers AS sr_prod ON sr_r_prod.producer_id = sr_prod.id left outer join 
                         sr_drug_forms as sr_d_f on sr_d_f.register_id=sr_r.id left join 
                         sr_register_boxes as sr_r_b1 on sr_r_b1.id=sr_d_f.pr_box_id left join 
                         sr_register_boxes as sr_r_b2 on sr_r_b2.id=sr_d_f.sec_box_id left join 
                         sr_boxes as sr_b1 on sr_r_b1.box_id = sr_b1.id left join 
                         sr_boxes as sr_b2 on sr_r_b2.box_id = sr_b2.id
WHERE        (sr_r.expiration_date > GETDATE()) AND (state_date > getdate() OR
                         state_date IS NULL)
						 
GO


