using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Helper
{
    public static class NullAvoider
    {
        public static DateTime? ReadNullOrDateTime(string column, SqlDataReader reader)
        {
            return (reader.IsDBNull(column)) ? (DateTime?)null : reader.GetDateTime(column);
        }

        public static string ReadNullRrString(string column, SqlDataReader reader)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }
    }
}
