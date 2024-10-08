using System;
using System.Windows.Forms;

namespace EmployeeDatabase
{
	public partial class MainForm : Form
	{
		private Label labelTitle;
		private DataGridView dataGridView1;

		public MainForm()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.labelTitle = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTitle.Location = new System.Drawing.Point((this.ClientSize.Width / 2) - 195, 20); // Центрирование вручную
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(390, 46);
			this.labelTitle.TabIndex = 0;
			this.labelTitle.Text = "База данных сотрудников";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(20, 80);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(640, 300);  // Высота фиксированная
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom; // Изменяется только по ширине
			this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 400);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.labelTitle);
			this.Name = "MainForm";
			this.Text = "База данных сотрудников";
			this.Resize += new System.EventHandler(this.MainForm_Resize); // Обработчик изменения размера окна
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		public void InitializeTable(Employee[] employees)
		{
			// Добавляем колонки в DataGridView
			foreach (var element in PersonnelDB.DB_Keys)
			{
				dataGridView1.Columns.Add(element.Key, element.Value);
			}

			// Добавляем строки (данные о сотрудниках)
			foreach (Employee empl in employees)
			{
				dataGridView1.Rows.Add(empl.FirstName, empl.LastName, empl.SurName, empl.BirthDate, empl.Address, empl.Department, empl.About);
			}

			AdjustColumnWidths();
		}

		// Центрирование заголовка при изменении размеров формы
		private void MainForm_Resize(object sender, EventArgs e)
		{
			labelTitle.Location = new System.Drawing.Point((this.ClientSize.Width / 2) - (labelTitle.Width / 2), 20);

			// Пересчитываем ширину столбцов при изменении размеров окна
			AdjustColumnWidths();
		}

		private void AdjustColumnWidths()
		{
			// Получаем ширину окна
			int totalWidth = this.ClientSize.Width - 40; // Минус отступы формы

			// Количество столбцов
			int columnCount = dataGridView1.Columns.Count;

			// Проверка на деление на ноль
			if (columnCount > 0)
			{
				// Рассчитываем ширину каждого столбца
				int columnWidth = totalWidth / columnCount;

				// Устанавливаем ширину для каждого столбца
				foreach (DataGridViewColumn column in dataGridView1.Columns)
				{
					column.Width = columnWidth;
				}
			}
		}


		private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			var item = sender as DataGridView;
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
			{
				string currentValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

				string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

				using (EditCellForm editForm = new EditCellForm(currentValue))
				{
					if (editForm.ShowDialog() == DialogResult.OK)
					{
						string newValue = editForm.UpdatedValue;
						dataGridView1[e.ColumnIndex, e.RowIndex].Value = newValue; // Обновляем значение ячейки
						if (PersonnelDB.EditData(e.RowIndex, columnName, newValue))
						{
							Console.WriteLine("Done");
						}
						
					}
				}
			}
		}
	}
}
