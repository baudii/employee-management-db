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

			Employee bob = new Employee("Bob", "Pierson", "Specter", new DateTime(1996, 01, 15), "Pinkerton st. 1640", "Marketing", "Dammm");
			form.InitializeTable(new Employee[] { bob });

			Application.Run(form);
		}
	}


}
