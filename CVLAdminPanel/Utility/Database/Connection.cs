using CallCenterPanel.Helper;
using Microsoft.CodeAnalysis;
using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.Utility.Database
{
    public static class Connection
    {
        private static string connectionString = URLs.Database;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}