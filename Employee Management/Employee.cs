using System;
using System.Reflection;

class Employee
{
	string firstName, lastName, surName;
	DateTime birthDate;
	string address;
	string department;
	string about;

	public Employee(string firstName = "", string lastName = "", string surName = "", DateTime birthDate = default, string address = "", string department = "", string about = "")
	{
		this.firstName = firstName;
		this.lastName = lastName;
		this.surName = surName;
		this.birthDate = birthDate;
		this.address = address;
		this.department = department;
		this.about = about;
	}

	public void PrintName() => Console.WriteLine(firstName);

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