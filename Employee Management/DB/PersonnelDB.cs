using System;
using System.Linq;
using System.IO;
using System.Data.SQLite;

public static class PersonnelDB
{
	static string db_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..", "DB", DB_Data_Static.DB_FILENAME);


	public static DB_Data[] Get_Data(int from, int amount) 
	{
		// Проверка существования файла
		if (!File.Exists(db_path))
			return null;

		// Создание строки подключения к базе данных
		string connectionString = $"Data Source={db_path};Version=3;";

		// Создаем массив сотрудников
		DB_Data[] data = new DB_Data[amount];

		try
		{
			// Открываем подключение
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				string command = $"SELECT {DB_Data_Static.DB_Keys.First()}";
				int j = 0;
				foreach (var item in DB_Data_Static.DB_Keys)
				{
					if (j == 0)
					{
						j++;
						continue; // Поскольку первый элемент уже записан
					}

					command += ", " + item;
					j++;
				}

				command += $" FROM {DB_Data_Static.DB_NAME} WHERE ID BETWEEN {from} AND {from + amount}";
				// Создание SQL-команды

				using (var cmd = new SQLiteCommand(command, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						int i = 0;
						while (reader.Read() && i < amount)
						{
							data[i] = new EmployeeExample();
							for (int k = 0; k < reader.FieldCount; k++)
							{
								var key = DB_Data_Static.DB_Keys.ElementAt(k);
								var value = reader.GetString(k);
								data[i].UpdateData(key, value);

							}
							i++;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception($"Ошибка при подключении к базе данных: {ex.Message}");
		}

		return data;
	}
	public static bool EditData(int id, string field, string newValue)
	{
		// Проверка существования файла
		if (!File.Exists(db_path))
			return false;

		// Создание строки подключения к базе данных
		string connectionString = $"Data Source={db_path};Version=3;";

		try
		{
			// Открываем подключение
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				// Проверка, что поле безопасно
				if (!DB_Data_Static.DB_Keys.Contains(field))
					throw new ArgumentException("Invalid field name");

				// Создание SQL-команды с параметризованным запросом
				string command = $"UPDATE {DB_Data_Static.DB_NAME} SET {field} = @NewValue WHERE ID = @ID;";

				using (var cmd = new SQLiteCommand(command, connection))
				{
					// Добавляем параметры
					cmd.Parameters.AddWithValue("@NewValue", newValue);
					cmd.Parameters.AddWithValue("@ID", id + 1);

					// Выполнение команды
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0; // Возвращаем true, если обновление прошло успешно
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception($"Ошибка при подключении к базе данных: {ex.Message}");
		}
	}
}