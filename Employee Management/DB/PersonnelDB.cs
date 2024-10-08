using System;
using System.Linq;
using System.IO;
using System.Data.SQLite;

public static class PersonnelDB
{
	static string db_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..", "DB", "employees_data.db");
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

				// Создание SQL-команды
				string command = $"SELECT First_Name, Last_Name, Sur_Name, Birth_Date, Address, Department, About FROM Employee_DB WHERE ID BETWEEN {from} AND {from + amount};";

				using (var cmd = new SQLiteCommand(command, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						int i = 0;
						while (reader.Read() && i < amount)
						{
							employees[i] = new Employee(
								reader.GetString(0), // firstName
								reader.GetString(1), // lastName
								reader.GetString(2), // surName
								reader.GetString(3), // birthDate
								reader.GetString(4), // adress
								reader.GetString(5), // department
								reader.GetString(6)  // about
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
		// Путь к базе данных
		string db_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..", "DB", "employees_data.db");

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
				string[] allowedFields = { "First_Name", "Last_Name", "Sur_Name", "Birth_Date", "Address", "Department", "About" };
				if (!allowedFields.Contains(field))
					throw new ArgumentException("Invalid field name");

				// Создание SQL-команды с параметризованным запросом
				string command = $"UPDATE Employee_DB SET {field} = @NewValue WHERE ID = @ID;";

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