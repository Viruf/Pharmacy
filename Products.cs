using System;
using System.Data.SqlClient;

namespace Pharmacy
{
    internal class Products
    {
        public void Add()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Введите наименование товара для добавления:");
            string productName = Console.ReadLine();
            if(productName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery("INSERT INTO dbo.Products (ProductName) VALUES (@ProductName)");
                cmd.Parameters.AddWithValue("@ProductName", productName);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new ArgumentException("Ошибка при сохранении нового товара! " + ex);
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
            Console.WriteLine("Введите наименование удаляемого товара:");
            string productName = Console.ReadLine();
            int ID = 0;
            if (productName.Trim() != "")
            {
                SqlCommand cmd = GetCommand.GetQuery("SELECT ID FROM dbo.Products WHERE ProductName = @ProductName");
                cmd.Parameters.AddWithValue("@ProductName", productName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        reader.Read();
                        ID = Convert.ToInt32(reader["ID"]);
                    }
                    else
                    {
                        Console.WriteLine("Товар с таким наименованием не найден!");
                        cmd.Connection.Close();
                        return;
                    }
                }
                cmd.Connection.Close();
                cmd = GetCommand.GetQuery("");
                try
                {
                    
                    cmd.CommandText = "DELETE FROM dbo.Products WHERE ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ошибка при удалении продукта! " + ex);
                }
                finally
                {
                    cmd.Connection.Close();
                }
                Console.WriteLine("Удалены партии с товаром на складах и наименование из справочника");
            }
            Console.WriteLine("|-----------------------------------------------------------|");
        }
        public void Show()
        {
            Console.WriteLine("|-----------------------------------------------------------|");
            Console.WriteLine("Вывожу список наименований товара:");
            SqlCommand cmd = GetCommand.GetQuery("SELECT ID, ProductName FROM dbo.Products ORDER BY ID");
            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        i++;
                        Console.WriteLine(" - " + reader["ID"].ToString() + ". " + reader["ProductName"].ToString());
                    }
            }
            cmd.Connection.Close();
            Console.WriteLine("Итого единиц наименований товаров в справочнике: " + i.ToString());
            Console.WriteLine("|-----------------------------------------------------------|");
        }
    }
}
