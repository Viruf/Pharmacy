using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    internal class GetCommand
    {
        public static SqlCommand GetQuery(string CommandText = "")
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectionString);
            try
            {
                con.Open();
                SqlCommand sqlCommand = con.CreateCommand();
                if (CommandText != "")
                    sqlCommand.CommandText = CommandText;
                return sqlCommand;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ошибка соединения с базой данных! " + ex);
            }
        }
    }
}
