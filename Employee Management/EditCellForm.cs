using System;
using System.Windows.Forms;

namespace EmployeeDatabase
{
	public partial class EditCellForm : Form
	{
		public string UpdatedValue { get; private set; }

		public EditCellForm(string currentValue)
		{
			InitializeComponent();
			textBoxValue.Text = currentValue;
		}

		private void InitializeComponent()
		{
			this.textBoxValue = new TextBox();
			this.buttonSave = new Button();
			this.labelCurrentValue = new Label();
			this.SuspendLayout();
			// 
			// textBoxValue
			// 
			this.textBoxValue.Location = new System.Drawing.Point(150, 12);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new System.Drawing.Size(240, 22);
			this.textBoxValue.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(315, 50);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new EventHandler(this.ButtonSave_Click);
			// 
			// labelCurrentValue
			// 
			this.labelCurrentValue.AutoSize = true;
			this.labelCurrentValue.Location = new System.Drawing.Point(12, 15);
			this.labelCurrentValue.Name = "labelCurrentValue";
			this.labelCurrentValue.Size = new System.Drawing.Size(132, 17);
			this.labelCurrentValue.TabIndex = 2;
			this.labelCurrentValue.Text = "Текущее значение:";
			// 
			// EditCellForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 100);
			this.Controls.Add(this.labelCurrentValue);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxValue);
			this.Name = "EditCellForm";
			this.Text = "Редактировать ячейку";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			UpdatedValue = textBoxValue.Text; // Получаем новое значение
			this.DialogResult = DialogResult.OK; // Закрываем форму и возвращаем результат
			this.Close();
		}

		private TextBox textBoxValue;
		private Button buttonSave;
		private Label labelCurrentValue;
	}
}
