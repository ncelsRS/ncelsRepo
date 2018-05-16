using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PW.Ncels.Database.Models.OBK;

namespace PW.Prism.Helpers
{
    /// <summary>
    /// Инструкция АНД из Гос. реeстра
    /// </summary>
    public static class InstructionFileHelper
    {
        public static int GetInstructionFileCount(int instructionid)
        {
            int dbFields = 0;
            var conString = GetConnectionString();
            var queryString = "SELECT COUNT(*) FROM register_nd WHERE nd_file_type_id=1 AND register_id = @instructionid";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.AddWithValue("@instructionid", instructionid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader(); 
                while (reader.Read())
                {
                    dbFields = reader.FieldCount;
                    break;
                }
                reader.Close();
            }
            return dbFields;
        }

        public static byte[] GetInstructionFile(int instructionid)
        {
            byte[] fileBytes = null;
            var conString = GetConnectionString();
            var queryString = "SELECT TOP 1 * FROM register_nd WHERE nd_file_type_id=1 AND register_id = @instructionid ORDER BY Id desc";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.AddWithValue("@instructionid", instructionid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fileBytes = reader["nd_file"] != null ? (byte[])reader["nd_file"] : null;
                    break;
                }
                reader.Close();
            }
            return fileBytes;
        }

        private static string GetConnectionString()
        {
            var conString = ConfigurationManager.ConnectionStrings["register_portal"].ToString();
            return conString;
        }
    }
}