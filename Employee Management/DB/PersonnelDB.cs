using System;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

public static class PersonnelDB
{
	// Идентификаторы базы данных (можно подключить любую бд)

	public const string DB_FILENAME = "employees_data.db";
	public const string DB_NAME = "Employee_DB";
	static string db_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..", "DB", DB_FILENAME);

	public static Dictionary<string, string> DB_Keys = new Dictionary<string, string>
	{
		{ "First_Name", "Имя" },
		{ "Last_Name", "Фамилия" },
		{ "Sur_Name", "Отчество" },
		{ "Birth_Date", "Дата рождения" },
		{ "Address", "Адрес" },
		{ "Department", "Отдел" },
		{ "About", "О сотруднике" }
	};

	public static Employee[] GetEmployees(int from, int amount)
	{
		// Проверка существования файла
		if (!File.Exists(db_path))
			return null;

		// Создание строки подключения к базе данных
		string connectionString = $"Data Source={db_path};Version=3;";

		// Создаем массив сотрудников
		Employee[] employees = new Employee[amount];

		try
		{
			// Открываем подключение
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				string command = $"SELECT {DB_Keys.First()}";

				foreach (var item in DB_Keys)
				{
					command += ", " + item.Key;
				}

				command += $" FROM {DB_NAME} WHERE ID BETWEEN {from} AND {from + amount}";
				// Создание SQL-команды

				using (var cmd = new SQLiteCommand(command, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						int i = 0;
						while (reader.Read() && i < amount)
						{
							employees[i] = new Employee(
								reader.GetString(0), // First_Name
								reader.GetString(1), // Last_Name
								reader.GetString(2), // Sur_Name
								reader.GetString(3), // Birth_Date
								reader.GetString(4), // Adress
								reader.GetString(5), // Department
								reader.GetString(6)  // About
							);
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

		return employees;
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
				if (!DB_Keys.ContainsKey(field))
					throw new ArgumentException("Invalid field name");

				// Создание SQL-команды с параметризованным запросом
				string command = $"UPDATE {DB_NAME} SET {field} = @NewValue WHERE ID = @ID;";

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