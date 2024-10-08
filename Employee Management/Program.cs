using System;
using System.Windows.Forms;
using System.Collections.Generic;
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

			var db_Filename = "employees_data.db";
			var db_Name = "Employee_DB";

			var form = new MainForm();
			var db_column_names = new List<string>
			{
				"Имя",
				"Фамилия",
				"Отчество",
				"Дата рождения",
				"Адрес",
				"Отдел",
				"О сотруднике"
			};

			DB_Data_Static.Initialize<EmployeeExample>(db_Filename, db_Name, db_column_names);
			DB_Data[] initialData = PersonnelDB.Get_Data(0, 22);
			form.InitializeTable(initialData);

			Application.Run(form);
		}
	}


}
