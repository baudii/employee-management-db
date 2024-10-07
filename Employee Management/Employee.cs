using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class Employee
{
	public string FullName => lastName + " " + firstName + " " + surName;
	public string BirthDate => birthDate;
	public string Adress => address;
	public string Department => department;
	public string About => about;

	string firstName, lastName, surName;
	string birthDate;
	string address;
	string department;
	string about;

	public Employee(string firstName = "_", string lastName = "_", string surName = "_", string birthDate = "Unknown", string address = "Unknown", string department = "Unknown", string about = "Unknown")
	{
		this.firstName = firstName;
		this.lastName = lastName;
		this.surName = surName;
		this.birthDate = birthDate;
		this.address = address;
		this.department = department;
		this.about = about;
	}


	public bool GetData(string fieldName, out object data)
	{
		var fields = typeof(Employee).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

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


	public IEnumerator<object> GetEnumerator()
	{
		var fields = typeof(Employee).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

		foreach (var field in fields)
		{
			yield return field.GetValue(this);
		}
	}

	public bool UpdateData(string fieldName, string value)
	{
		var fields = typeof(Employee).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

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