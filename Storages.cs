using System;
using System.Data.SqlClient;

namespace Pharmacy
{
    internal class Storages
    {
        public void Add()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование склада:");
            string StorgeName = Console.ReadLine();
            Console.WriteLine("Введите наименование аптеки, на которой находится склад:");
            string PharmacyName = Console.ReadLine();
            if (StorgeName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery(@"INSERT INTO dbo.Storages (StorageName, PharmacyID)
                                                       (SELECT @StorageName, ID FROM Pharmacies WHERE PharmacyName = @PharmacyName)");
                cmd.Parameters.AddWithValue("@StorageName", StorgeName);
                cmd.Parameters.AddWithValue("@PharmacyName", PharmacyName);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ошибка при сохранении нового склада! " + ex);
                }
                finally
                {
                    cmd.Connection.Close();
                }
                Console.WriteLine("|-----------------------------------------------------------|");
            }
        }
        public void Delete()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование удаляемого склада:");
            string StorgeName = Console.ReadLine();
            int ID = 0;
            if (StorgeName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery("SELECT ID FROM dbo.Storages WHERE StorageName = @StorageName");
                cmd.Parameters.AddWithValue("@StorageName", StorgeName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ID = Convert.ToInt32(reader["ID"]);
                    }
                    else
                    {
                        Console.WriteLine("Склад с таким наименованием не найден!");
                        cmd.Connection.Close();
                        return;
                    }
                }
                try
                {
                    cmd.CommandText = "DELETE FROM dbo.Storages WHERE ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ошибка при удалении склада! " + ex);
                }
                finally
                {
                    cmd.Connection.Close();
                }
                Console.WriteLine("Удалены все партии на складе и склад");
            }
            Console.WriteLine("|-----------------------------------------------------------|");
        }
        public void Show()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Вывожу список складов:");
            SqlCommand cmd = GetCommand.GetQuery(@"SELECT s.ID, s.StorageName, p.PharmacyName, p.PharmacyAddres, p.PharmacyPhones
                                                  FROM dbo.Storages s INNER JOIN dbo.Pharmacies p ON s.PharmacyID = p.ID ORDER BY s.ID");
            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        i++;
                        Console.WriteLine(" - " + reader["ID"].ToString() + ". " + reader["StorageName"].ToString() + " аптека: "
                                            + reader["PharmacyName"].ToString() + ". адрес: " + reader["PharmacyAddres"].ToString() + 
                                            ". тел: " + reader["PharmacyPhones"].ToString());
                    }
            }
            cmd.Connection.Close();
            Console.WriteLine("Итого складов: " + i.ToString());
            Console.WriteLine("|-----------------------------------------------------------|");
        }
    }
}
