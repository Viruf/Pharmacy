using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    internal class Pharmacies
    {
        public void Add()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование аптеки для добавления:");
            string PharmacyName = Console.ReadLine();
            Console.WriteLine("Введите адрес аптеки:");
            string PharmacyAddres = Console.ReadLine();
            Console.WriteLine("Введите телефоны аптеки:");
            string PharmacyPhones = Console.ReadLine();
            if (PharmacyName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery(@"INSERT INTO dbo.Pharmacies (PharmacyName, PharmacyAddres, PharmacyPhones)
                                                       VALUES (@PharmacyName, @PharmacyAddres, @PharmacyPhones)");
                cmd.Parameters.AddWithValue("@PharmacyName", PharmacyName);
                cmd.Parameters.AddWithValue("@PharmacyAddres", PharmacyAddres);
                cmd.Parameters.AddWithValue("@PharmacyPhones", PharmacyPhones);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ошибка при сохранении новой аптеки! " + ex);
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            Console.WriteLine("|-----------------------------------------------------------|");
        }
        public void Delete()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование удаляемой аптеки:");
            string PharmacyName = Console.ReadLine();
            int ID = 0;
            if (PharmacyName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery("SELECT ID FROM dbo.Pharmacies WHERE PharmacyName = @PharmacyName");
                cmd.Parameters.AddWithValue("@PharmacyName", PharmacyName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ID = Convert.ToInt32(reader["ID"]);
                    }
                    else
                    {
                        Console.WriteLine("Аптека с таким наименованием не найдена!");
                        cmd.Connection.Close();
                        return;
                    }
                }
                try
                {
                    cmd.CommandText = "DELETE FROM dbo.Pharmacies WHERE ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ошибка при удалении аптеки! " + ex);
                }
                finally
                {
                    cmd.Connection.Close();
                }
                Console.WriteLine("Удалены партии на складах аптеки, склады и аптека");
            }
            Console.WriteLine("|-----------------------------------------------------------|");
        }
        public void Show()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Вывожу список аптек:");
            SqlCommand cmd = GetCommand.GetQuery("SELECT * FROM dbo.Pharmacies ORDER BY ID");
            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        i++;
                        Console.WriteLine(" - " + reader["ID"].ToString() + ". " + reader["PharmacyName"].ToString() + ". адрес: " +
                        reader["PharmacyAddres"].ToString() + ". тел: " + reader["PharmacyPhones"].ToString());
                    }
            }
            cmd.Connection.Close();
            Console.WriteLine("Итого аптек: " + i.ToString());
            Console.WriteLine("|-----------------------------------------------------------|");
        }
    }
}
