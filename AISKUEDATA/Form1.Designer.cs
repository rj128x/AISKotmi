namespace AISKUEDATA
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.selDate = new System.Windows.Forms.MonthCalendar();
			this.lbSource = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.selDate);
			this.panel1.Controls.Add(this.lbSource);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(241, 573);
			this.panel1.TabIndex = 0;
			// 
			// selDate
			// 
			this.selDate.Location = new System.Drawing.Point(13, 22);
			this.selDate.Name = "selDate";
			this.selDate.TabIndex = 9;
			// 
			// lbSource
			// 
			this.lbSource.FormattingEnabled = true;
			this.lbSource.Location = new System.Drawing.Point(12, 196);
			this.lbSource.Name = "lbSource";
			this.lbSource.Size = new System.Drawing.Size(204, 95);
			this.lbSource.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(141, 308);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel2
			// 
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(241, 473);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1171, 100);
			this.panel2.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(241, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1171, 473);
			this.panel3.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1412, 573);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListBox lbSource;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MonthCalendar selDate;
	}
}

