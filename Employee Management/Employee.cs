using System;
using System.Reflection;

class Employee
{
	string firstName, lastName, fatherName;
	DateTime birthDate;
	string address;
	string department;
	string about;

	public Employee(string firstName = "", string lastName = "", string fatherName = "", DateTime birthDate = default, string address = "", string department = "", string about = "")
	{
		this.firstName = firstName;
		this.lastName = lastName;
		this.fatherName = fatherName;
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
				field.SetValue(this, value);
				return true;
			}
		}

		Console.WriteLine($"{fieldName} was not found");
		return false;
	}
}