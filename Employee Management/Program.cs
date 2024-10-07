using System;
using System.Windows.Forms;
using EmployeeDatabase;

namespace Employee_Management
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var form = new MainForm();
			Employee[] initialData = PersonnelDB.GetEmployees("./DB/employees_data.db", 0, 20);
			form.InitializeTable(initialData);

			Application.Run(form);
		}
	}


}
