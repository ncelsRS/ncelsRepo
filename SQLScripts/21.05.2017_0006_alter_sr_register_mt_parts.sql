alter table sr_register_mt_parts
alter column model varchar(500);
go
alter table sr_register_mt_parts
alter column specification [varchar](500);
go
alter table sr_register_mt_parts
alter column [specification_kz] [nvarchar](500);
go
alter table sr_register_mt_parts
alter column [producer_name] [varchar](2000);
go
alter table sr_register_mt_parts
alter column [country_name] [varchar](500);
go
alter table sr_register_mt_parts
alter column [producer_name_kz] [nvarchar](2000);
go
alter table sr_register_mt_parts
alter column [country_name_kz] [nvarchar](500);
go
alter table sr_register_boxes
alter column inner_sign bit null;