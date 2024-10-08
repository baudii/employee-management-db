using System;
using System.Reflection;

public abstract class DB_Data
{
	public virtual bool GetData(string fieldName, out object data)
	{
		var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

		foreach (var field in fields)
		{
			if (field.Name == fieldName)
			{
				data = field.GetValue(this);
				Console.WriteLine($"Found field {field.Name} and retrieved {field.GetValue(this)}");
				return true;
			}
		}

		Console.WriteLine($"{fieldName} was not found");
		data = null;
		return false;
	}

	public virtual bool UpdateData(string fieldName, string value)
	{
		var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

		foreach (var field in fields)
		{
			if (field.Name == fieldName)
			{
				Console.WriteLine($"Updating the field {field.Name} from {field.GetValue(this)} to {value}!");
				field.SetValue(this, value);

				Console.WriteLine("Success!");
				return true;
			}
		}

		Console.WriteLine($"{fieldName} was not found");
		return false;
	}
}