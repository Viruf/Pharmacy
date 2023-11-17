using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    internal class Lots
    {
        public void Add()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование склада:");
            string StorageName = Console.ReadLine();
            Console.WriteLine("Введите наименование товара:");
            string ProductName = Console.ReadLine();
            Console.WriteLine("Введите количество товара в партии:");
            decimal quantity;
            try
            {
                quantity = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка при вводе количества товара в партии");
                return;
            }
            SqlCommand cmd = GetCommand.GetQuery(@"INSERT INTO dbo.Lots (ProductID, StorageID, Quantity)
                                                    (SELECT p.ID, s.ID, @Quantity FROM Storages s, Products p
                                                    WHERE StorageName = @StorageName and ProductName = @ProductName)");
            cmd.Parameters.AddWithValue("@StorageName", StorageName);
            cmd.Parameters.AddWithValue("@ProductName", ProductName);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ошибка при сохранении новой партии! " + ex);
            }
            finally
            {
                cmd.Connection.Close();
            }
            Console.WriteLine("|-----------------------------------------------------------|");
        }
        public void Delete()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите код партии:");
            int ID;
            try
            {
                ID = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка при кода партии");
                return;
            }
            SqlCommand cmd = GetCommand.GetQuery("DELETE FROM dbo.Lots WHERE ID = @ID");
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ошибка при удалении партии! " + ex);
            }
            finally
            {
                cmd.Connection.Close();
            }
            Console.WriteLine("Партия товара удалена");
            Console.WriteLine("|-----------------------------------------------------------|");
        }

        public void Show()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Вывожу список всех партий товаров:");
            SqlCommand cmd = GetCommand.GetQuery(@"SELECT l.ID, p.ProductName, l.Quantity, s.StorageName, ph.PharmacyName
                                                    FROM Lots l
                                                    INNER JOIN Products p ON l.ProductID = p.ID
                                                    INNER JOIN Storages s ON l.StorageID = s.ID
                                                    INNER JOIN Pharmacies ph ON s.PharmacyID = ph.ID
                                                    ORDER BY ph.PharmacyName, s.StorageName, l.ID");
            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        i++;
                        Console.WriteLine(" - " + reader["ID"].ToString() + ". " + reader["ProductName"].ToString() +
                                            " кол-во: " + Convert.ToDecimal(reader["Quantity"]).ToString("### ##0.000") +
                                            " склад: " + reader["StorageName"].ToString() + ". аптека: " + reader["PharmacyName"].ToString());
                    }
            }
            cmd.Connection.Close();
            Console.WriteLine("Итого партий: " + i.ToString());
            Console.WriteLine("|-----------------------------------------------------------|");
        }

        public void ShowOnStorage()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование аптеки:");
            string PharmacyName = Console.ReadLine();
            Console.WriteLine("Вывожу список всех партий товаров в указанной аптеке:");
            SqlCommand cmd = GetCommand.GetQuery(@"SELECT l.ID, p.ProductName, l.Quantity
                                                    FROM Lots l
                                                    INNER JOIN Products p ON l.ProductID = p.ID
                                                    INNER JOIN Storages s ON l.StorageID = s.ID
                                                    INNER JOIN Pharmacies ph ON s.PharmacyID = ph.ID
                                                    WHERE ph.PharmacyName = @PharmacyName
                                                    ORDER BY s.StorageName, l.ID");
            cmd.Parameters.AddWithValue("@PharmacyName", PharmacyName);
            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        i++;
                        Console.WriteLine(" - " + reader["ID"].ToString() + ". " + reader["ProductName"].ToString() +
                                            " кол-во: " + Convert.ToDecimal(reader["Quantity"]).ToString("### ##0.000"));
                    }
            }
            cmd.Connection.Close();
            Console.WriteLine("Итого партий: " + i.ToString());
            Console.WriteLine("|-----------------------------------------------------------|");
        }

    }
}
