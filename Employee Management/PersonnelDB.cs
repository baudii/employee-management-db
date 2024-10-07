using System;
using System.Data.SQLite;

/// <summary>
/// Database should be 
/// </summary>
public static class PersonnelDB
{
	public static Employee[] GetEmployees(string db_path, int from, int amount)
	{
		var connection = new SQLiteConnection(db_path);
		connection.Open();
		string command = $"SELECT firstName, lastName, surName, birthDate, adress, deparment, about FROM Employee_DB WHERE id BETWEEN {from} AND {from + amount - 1};";

		var cmd = new SQLiteCommand(command, connection);
		SQLiteDataReader reader = cmd.ExecuteReader();

		Employee[] employees = new Employee[amount];
		int i = 0;
		while (reader.Read())
		{
			employees[i] = new Employee(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
			i++;
		}

		return employees;
	}
}
