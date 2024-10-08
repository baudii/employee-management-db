using System.Collections.Generic;
using System.Reflection;

public static class DB_Data_Static
{

	public static string DB_FILENAME = "employees_data.db";
	public static string DB_NAME = "Employee_DB";
	public static List<string> Db_Column_Names;
	
	// Возьмем из определения класса DB_Data
	public static List<string> DB_Keys;

	/// <summary>
	/// Инициализация всей статической информации, которая необходима для работы программы
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="db_Filename">Название файла базы данных (файл должен распологаться в папке DB проекта!)</param>
	/// <param name="db_Name">Навзание базы данных</param>
	/// <param name="db_Column_Names">Опционально: список названий столбцов для отображения в приложении</param>
	public static void Initialize<T>(string db_Filename, string db_Name, List<string> db_Column_Names = null) where T : DB_Data
	{
		DB_FILENAME = db_Filename;
		DB_NAME = db_Name;
		if (db_Column_Names != null)
			Db_Column_Names = db_Column_Names;
		else
			Db_Column_Names = new List<string>();
		DB_Keys = new List<string>();

		var enumerator = GetEnumerator<T>();
		while (enumerator.MoveNext())
		{
			DB_Keys.Add(enumerator.Current);
			if (db_Column_Names == null)
			{
				Db_Column_Names.Add(enumerator.Current);
			}
		}
	}

	public static IEnumerator<string> GetEnumerator<T>() where T : DB_Data
	{
		var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

		foreach (var field in fields)
		{
			yield return field.Name;
		}
	}
}
