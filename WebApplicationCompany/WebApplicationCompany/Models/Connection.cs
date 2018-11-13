using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplicationCompany.Models
{
    public class Connection
    {
        SqlConnection connection = new SqlConnection();
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

        public SqlConnection sqlConnection
        {
            get => connection;
        }
        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\GIT\Turchin_Michael_HomeWork_WPF\WPF_Company_Employees\db\HWWPF.mdf;Integrated Security=True;Connect Timeout=30
        public void connect()
        {
            connectionString.DataSource = @"(LocalDB)\MSSQLLocalDB";
            connectionString.InitialCatalog = @"X:\GIT\Turchin_Michael_HomeWork_WPF\WPF_Company_Employees\db\HWWPF.mdf";
            connectionString.IntegratedSecurity = true;
            connectionString.Pooling = false;

            connection = new SqlConnection(connectionString.ToString());

            connection.Open();
        }
    }
}