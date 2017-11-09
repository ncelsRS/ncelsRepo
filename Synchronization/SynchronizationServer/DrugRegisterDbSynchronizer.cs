using System;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace SynchronizationServer
{
    public class DrugRegisterDbSynchronizer : DbSynchronizer
    {
        public DrugRegisterDbSynchronizer(DbSynchronizerConfiguration localConfiguration, DbSynchronizerConfiguration remoteConfiguration) : base(localConfiguration, remoteConfiguration)
        {
            ApplyLocalChangesFailed += ApplyChangeFailed;
            Logger = s => Console.WriteLine(s);
        }

        protected override void DefineLocalScope(DbSyncScopeDescription scopeDescription)
        {
            using (var connection = new SqlConnection(LocalConfiguration.ConnectionString))
            {
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_measures", connection);
                tableDesc.GlobalName = "measures";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_reg_actions", connection);
                tableDesc.GlobalName = "reg_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_reg_types", connection);
                tableDesc.GlobalName = "reg_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_substance_types", connection);
                tableDesc.GlobalName = "substance_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_categories", connection);
                tableDesc.GlobalName = "categories";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_substances", connection);
                tableDesc.GlobalName = "substances";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_boxes", connection);
                tableDesc.GlobalName = "boxes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_form_types", connection);
                tableDesc.GlobalName = "form_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_producers", connection);
                tableDesc.GlobalName = "producers";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_producer_types", connection);
                tableDesc.GlobalName = "producer_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_countries", connection);
                tableDesc.GlobalName = "countries";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_international_names", connection);
                tableDesc.GlobalName = "international_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_atc_codes", connection);
                tableDesc.GlobalName = "atc_codes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_drug_types", connection);
                tableDesc.GlobalName = "drug_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_dosage_forms", connection);
                tableDesc.GlobalName = "dosage_forms";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_nd_names", connection);
                tableDesc.GlobalName = "nd_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_mt_categories", connection);
                tableDesc.GlobalName = "mt_categories";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_degree_risks", connection);
                tableDesc.GlobalName = "degree_risks";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_degree_risk_details", connection);
                tableDesc.GlobalName = "degree_risk_details";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_life_types", connection);
                tableDesc.GlobalName = "life_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_nd_types", connection);
                tableDesc.GlobalName = "nd_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_use_methods", connection);
                tableDesc.GlobalName = "use_methods";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_pharmacological_actions", connection);
                tableDesc.GlobalName = "pharmacological_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register", connection);
                tableDesc.GlobalName = "register";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_names", connection);
                tableDesc.GlobalName = "register_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_boxes", connection);
                tableDesc.GlobalName = "register_boxes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_boxes_rk_ls", connection);
                tableDesc.GlobalName = "register_boxes_rk_ls";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_producers", connection);
                tableDesc.GlobalName = "register_producers";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_drugs", connection);
                tableDesc.GlobalName = "register_drugs";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_mt", connection);
                tableDesc.GlobalName = "register_mt";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_mt_kz", connection);
                tableDesc.GlobalName = "register_mt_kz";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_mt_parts", connection);
                tableDesc.GlobalName = "register_mt_parts";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_drug_forms", connection);
                tableDesc.GlobalName = "drug_forms";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_substances", connection);
                tableDesc.GlobalName = "register_substances";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_pharmacological_actions", connection);
                tableDesc.GlobalName = "register_pharmacological_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_use_methods", connection);
                tableDesc.GlobalName = "register_use_methods";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_instruction_types", connection);
                tableDesc.GlobalName = "instruction_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_instructions", connection);
                tableDesc.GlobalName = "register_instructions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("sr_register_instructions_files", connection);
                tableDesc.GlobalName = "register_instructions_files";
                scopeDescription.Tables.Add(tableDesc);
            }
        }

        protected override void DefineRemoteScope(DbSyncScopeDescription scopeDescription)
        {
            using (var connection = new SqlConnection(RemoteConfiguration.ConnectionString))
            {
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("measures", connection);
                tableDesc.GlobalName = "measures";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("reg_actions", connection);
                tableDesc.GlobalName = "reg_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("reg_types", connection);
                tableDesc.GlobalName = "reg_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("substance_types", connection);
                tableDesc.GlobalName = "substance_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("categories", connection);
                tableDesc.GlobalName = "categories";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("substances", connection);
                tableDesc.GlobalName = "substances";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("boxes", connection);
                tableDesc.GlobalName = "boxes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("form_types", connection);
                tableDesc.GlobalName = "form_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("producers", connection);
                tableDesc.GlobalName = "producers";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("producer_types", connection);
                tableDesc.GlobalName = "producer_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("countries", connection);
                tableDesc.GlobalName = "countries";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("international_names", connection);
                tableDesc.GlobalName = "international_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("atc_codes", connection);
                tableDesc.GlobalName = "atc_codes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("drug_types", connection);
                tableDesc.GlobalName = "drug_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("dosage_forms", connection);
                tableDesc.GlobalName = "dosage_forms";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("nd_names", connection);
                tableDesc.GlobalName = "nd_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("mt_categories", connection);
                tableDesc.GlobalName = "mt_categories";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("degree_risks", connection);
                tableDesc.GlobalName = "degree_risks";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("degree_risk_details", connection);
                tableDesc.GlobalName = "degree_risk_details";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("life_types", connection);
                tableDesc.GlobalName = "life_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("nd_types", connection);
                tableDesc.GlobalName = "nd_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("use_methods", connection);
                tableDesc.GlobalName = "use_methods";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("pharmacological_actions", connection);
                tableDesc.GlobalName = "pharmacological_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register", connection);
                tableDesc.GlobalName = "register";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_names", connection);
                tableDesc.GlobalName = "register_names";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_boxes", connection);
                tableDesc.GlobalName = "register_boxes";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_boxes_rk_ls", connection);
                tableDesc.GlobalName = "register_boxes_rk_ls";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_producers", connection);
                tableDesc.GlobalName = "register_producers";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_drugs", connection);
                tableDesc.GlobalName = "register_drugs";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_mt", connection);
                tableDesc.GlobalName = "register_mt";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_mt_kz", connection);
                tableDesc.GlobalName = "register_mt_kz";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_mt_parts", connection);
                tableDesc.GlobalName = "register_mt_parts";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("drug_forms", connection);
                tableDesc.GlobalName = "drug_forms";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_substances", connection);
                tableDesc.GlobalName = "register_substances";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_pharmacological_actions", connection);
                tableDesc.GlobalName = "register_pharmacological_actions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_use_methods", connection);
                tableDesc.GlobalName = "register_use_methods";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("instruction_types", connection);
                tableDesc.GlobalName = "instruction_types";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_instructions", connection);
                tableDesc.GlobalName = "register_instructions";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("register_instructions_files", connection);
                tableDesc.GlobalName = "register_instructions_files";
                scopeDescription.Tables.Add(tableDesc);
            }
        }

        private void ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            if (e.Conflict != null)
                Log(e.Conflict.Type.ToString());
            if (e.Error != null)
                Log(e.Error.ToString());
        }
    }
}