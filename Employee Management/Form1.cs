using System;
using System.Data;
using System.Windows.Forms;

namespace EmployeeDatabase
{
	using System;
	using System.Data;
	using System.Windows.Forms;

	namespace EmployeeDatabase
	{
		public partial class Form1 : Form
		{
			public Form1()
			{
				InitializeComponent();
				LoadData();
			}

			private void LoadData()
			{
				// Создание DataTable для хранения данных сотрудников
				DataTable employeesTable = new DataTable();
				employeesTable.Columns.Add("Name", typeof(string));
				employeesTable.Columns.Add("Birth Date", typeof(DateTime));
				employeesTable.Columns.Add("Address", typeof(string));
				employeesTable.Columns.Add("Department", typeof(string));
				employeesTable.Columns.Add("About", typeof(string));

				// Пример данных (добавьте своих сотрудников здесь)
				employeesTable.Rows.Add("Иванов Иван", new DateTime(1985, 1, 1), "г. Москва, ул. Ленина, д. 1", "IT", "Программист");
				employeesTable.Rows.Add("Петров Петр", new DateTime(1990, 5, 15), "г. Санкт-Петербург, ул. Пушкина, д. 2", "HR", "Менеджер по кадрам");

				// Привязка DataTable к DataGridView
				dataGridView1.DataSource = employeesTable;

				// Настройка внешнего вида DataGridView
				dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			}

			private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
			{
				// Проверка нажатой ячейки
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
				{
					string currentValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
					EditCellForm editForm = new EditCellForm(currentValue);

					// Отображение окна редактирования
					if (editForm.ShowDialog() == DialogResult.OK)
					{
						// Сохранение нового значения в таблицу
						dataGridView1[e.ColumnIndex, e.RowIndex].Value = editForm.NewValue;
					}
				}
			}
		}
	}

}
