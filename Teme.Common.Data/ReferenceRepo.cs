using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;

namespace Teme.Common.Data
{
    public class ReferenceRepo : EntityRepo, IReferenceRepo
    {
        private readonly IConfiguration _config;
        public ReferenceRepo(TemeContext context, IConfiguration config) : base(context)
        {
            _config = config;
        }

        public async Task<object> GetGosRegistry(string name, string culture, int page, int counter)
        {
            var connectionString = _config["ConnectionStrings:GosRegister"];
            var queryString = $"SELECT * FROM register r WHERE r.name LIKE '%{name}%'";
            using (SqlConnection c = new SqlConnection(connectionString))
            {
                c.Open();
                SqlCommand command = new SqlCommand(queryString, c);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                }
                reader.Close();
            }
            return new { };
        }
    }
}
