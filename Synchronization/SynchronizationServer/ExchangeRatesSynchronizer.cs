using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Linq;

namespace SynchronizationServer
{
    public class ExchangeRatesSynchronizer
    {
        public string ConnectionString { get; private set; }
        public string ExchangeRateUrl { get; private set; }

        public ExchangeRatesSynchronizer(string connectionString, string exchangeRateUrl)
        {
            ConnectionString = connectionString;
            ExchangeRateUrl = exchangeRateUrl;
        }

        public void Synchronize(DateTime exchangeRateDateFrom, DateTime exchangeRateDateTo)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var curencies = GetCurrencies(connection);
                using (var adapter = new SqlDataAdapter("select * from obk_exchangerate", connection))
                {
                    var dataSet = GetCRUDDataset();
                    InitAdapter(adapter, connection);
                    var syncDate = exchangeRateDateFrom.Date;
                    while (syncDate <= exchangeRateDateTo.Date)
                    {
                        var rates = GetExchangeRates(syncDate);
                        var savedRates = GetSavedExchangeRatesOnDate(connection, syncDate,
                            rates.Select(e => e.CurrencyCode).Distinct().ToList());
                        foreach (var rate in rates)
                        {
                            var savedRate = savedRates.FirstOrDefault(e => e.CurrencyCode == rate.CurrencyCode);
                            var currencyCode = rate.CurrencyCode == "RUB" ? "RUR" : rate.CurrencyCode;
                            var currency = curencies.FirstOrDefault(e => e.Code.Trim() == currencyCode);
                            if (currency != null)
                            {
                                var row = dataSet.Tables["obk_exchangerate"].NewRow();
                                row.SetField("currency_id", currency.Id);
                                row.SetField("rate_date", syncDate);
                                row.SetField("rate", rate.RateValue);
                                dataSet.Tables["obk_exchangerate"].Rows.Add(row);
                                if (savedRate == null)
                                {
                                    row.SetField("id", 0);
                                    row.AcceptChanges();
                                    row.SetAdded();
                                }
                                else
                                {
                                    row.SetField("id", savedRate.Id);
                                    row.AcceptChanges();
                                    row.SetModified();
                                }
                            }
                        }
                        adapter.Update(dataSet, "obk_exchangerate");
                        dataSet.Clear();
                        syncDate = syncDate.AddDays(1);
                    }
                }

            }
        }

        private DataSet GetCRUDDataset()
        {
            var dataSet = new DataSet();
            var table = dataSet.Tables.Add("obk_exchangerate");
            var col = table.Columns.Add("id", typeof(long));
            col.AutoIncrement = true;
            table.Columns.Add("currency_id", typeof(int));
            table.Columns.Add("rate", typeof(decimal));
            table.Columns.Add("rate_date", typeof(DateTime));
            return dataSet;
        }

        private void InitAdapter(SqlDataAdapter tableAdapter, SqlConnection connection)
        {
            tableAdapter.InsertCommand = new SqlCommand("insert into obk_exchangerate (currency_id, rate, rate_date) values(@currency_id, @rate, @rate_date)", connection);
            var p = tableAdapter.InsertCommand.Parameters.Add("@currency_id", SqlDbType.Int);
            p.SourceColumn = "currency_id";
            p = tableAdapter.InsertCommand.Parameters.Add("@rate", SqlDbType.Decimal);
            p.SourceColumn = "rate";
            p = tableAdapter.InsertCommand.Parameters.Add("@rate_date", SqlDbType.DateTime);
            p.SourceColumn = "rate_date";
            tableAdapter.UpdateCommand = new SqlCommand("update obk_exchangerate set rate=@rate, rate_date=@rate_date where id=@id", connection);
            p = tableAdapter.UpdateCommand.Parameters.Add("@currency_id", SqlDbType.Int);
            p.SourceColumn = "currency_id";
            p = tableAdapter.UpdateCommand.Parameters.Add("@rate", SqlDbType.Decimal);
            p.SourceColumn = "rate";
            p = tableAdapter.UpdateCommand.Parameters.Add("@rate_date", SqlDbType.DateTime);
            p.SourceColumn = "rate_date";
            p = tableAdapter.UpdateCommand.Parameters.Add("@id", SqlDbType.BigInt);
            p.SourceColumn = "id";
        }

        private List<Rate> GetSavedExchangeRatesOnDate(SqlConnection connection, DateTime rateDate, List<string> currencyCodes)
        {
            var result = new List<Rate>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT er.id, er.rate, c.abbreviation FROM obk_exchangerate er " +
                                      "inner join obk_currencies c on c.id = er.currency_id " +
                                      string.Format(" where er.rate_date=@rate_date and c.abbreviation in ({0})", string.Join(",", currencyCodes.Select(e => string.Format("'{0}'", e))));
                var param = command.Parameters.Add("@rate_date", SqlDbType.Date);
                param.Value = rateDate;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Rate()
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("id")),
                            CurrencyCode = reader.GetString(reader.GetOrdinal("abbreviation")).Trim(),
                            RateValue = reader.GetDecimal(reader.GetOrdinal("rate"))
                        });
                    }
                }
            }
            return result;
        }

        private List<Rate> GetExchangeRates(DateTime rateDate)
        {
            WebRequest request = WebRequest.Create(string.Format("{0}{1}", ExchangeRateUrl, rateDate.ToString("dd.MM.yyyy")));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            XDocument xmlDocument = XDocument.Parse(responseFromServer);
            var rateItems = xmlDocument.Root.Descendants("item");
            return rateItems.Select(e => new Rate()
            {
                CurrencyCode = e.Descendants("title").FirstOrDefault().Value,
                RateValue =
                    decimal.Parse(e.Descendants("description").FirstOrDefault().Value, NumberStyles.Any,
                        CultureInfo.InvariantCulture)
            }).ToList();
        }

        private List<Currency> GetCurrencies(SqlConnection connection)
        {
            var result = new List<Currency>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select id, abbreviation from obk_currencies";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Currency()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Code = reader.GetString(reader.GetOrdinal("abbreviation"))
                        });
                    }
                }
            }
            return result;
        }

        private class Rate
        {
            public long Id { get; set; }
            public string CurrencyCode { get; set; }
            public decimal RateValue { get; set; }
        }
        private class Currency
        {
            public int Id { get; set; }
            public string Code { get; set; }
        }
    }
}