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
			LoadSampleData();
		}

		private void InitializeComponent()
		{
			this.labelTitle = new Label();
			this.dataGridView1 = new DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTitle.Location = new System.Drawing.Point(292, 9);
			this.labelTitle.Margin = new Padding(2, 0, 2, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(408, 37);
			this.labelTitle.TabIndex = 0;
			this.labelTitle.Text = "База данных сотрудников";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelTitle.Click += new EventHandler(this.labelTitle_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(27, 68);
			this.dataGridView1.Margin = new Padding(2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(977, 608);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1024, 720);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.labelTitle);
			this.Margin = new Padding(2);
			this.Name = "MainForm";
			this.Text = "База данных сотрудников";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void LoadSampleData()
		{
			// Добавляем колонки в DataGridView
			dataGridView1.Columns.Add("fullName", "Полное имя");
			dataGridView1.Columns.Add("birthDate", "Дата рождения");
			dataGridView1.Columns.Add("address", "Адрес");
			dataGridView1.Columns.Add("department", "Отдел");
			dataGridView1.Columns.Add("about", "О сотруднике");

			// Пример тестовых данных
			dataGridView1.Rows.Add("Иван Иванов", DateTime.Now.AddYears(-30).ToShortDateString(), "Улица Ленина, дом 1", "Отдел продаж", "Менеджер");
			dataGridView1.Rows.Add("Петр Петров", DateTime.Now.AddYears(-25).ToShortDateString(), "Улица Мира, дом 2", "Отдел маркетинга", "Аналитик");
		}

		private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
			{
				string currentValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
				using (EditCellForm editForm = new EditCellForm(currentValue))
				{
					if (editForm.ShowDialog() == DialogResult.OK)
					{
						string newValue = editForm.UpdatedValue;
						dataGridView1[e.ColumnIndex, e.RowIndex].Value = newValue; // Обновляем значение ячейки
					}
				}
			}
		}

		private void labelTitle_Click(object sender, EventArgs e)
		{

		}
	}

}
