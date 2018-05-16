USE ncels;  
GO    
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'ind_sr_countries_name')   
    DROP INDEX ind_sr_countries_name ON sr_countries;   
GO  
 
CREATE NONCLUSTERED INDEX ind_sr_countries_name   
    ON sr_countries (name);   
GO  


USE ncels;  
GO    
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'ind_dictionaries_name')   
    DROP INDEX ind_dictionaries_name ON Dictionaries;   
GO  
 
CREATE NONCLUSTERED INDEX ind_dictionaries_name   
    ON Dictionaries (name);   
GO  

USE ncels;  
GO    
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'ind_registers_country_name')   
    DROP INDEX ind_registers_country_name ON sr_register;   
GO  
 
CREATE NONCLUSTERED INDEX ind_registers_country_name   
    ON sr_register (_country_name);   
GO  
