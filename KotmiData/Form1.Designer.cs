namespace KotmiData
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
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.pnlTI = new System.Windows.Forms.Panel();
			this.pnlTIList = new System.Windows.Forms.Panel();
			this.lbItems = new System.Windows.Forms.ListBox();
			this.pnlTIName = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.chbStep = new System.Windows.Forms.CheckBox();
			this.chbHH = new System.Windows.Forms.CheckBox();
			this.DTPStart = new System.Windows.Forms.DateTimePicker();
			this.txtTI = new System.Windows.Forms.TextBox();
			this.btnShow = new System.Windows.Forms.Button();
			this.txtStep = new System.Windows.Forms.TextBox();
			this.DTPEnd = new System.Windows.Forms.DateTimePicker();
			this.tcKotmiData = new System.Windows.Forms.TabControl();
			this.pnlLog = new System.Windows.Forms.Panel();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.txtLog = new System.Windows.Forms.RichTextBox();
			this.pnlGrid = new System.Windows.Forms.Panel();
			this.btnBreak = new System.Windows.Forms.Button();
			this.pnlLeft.SuspendLayout();
			this.pnlTI.SuspendLayout();
			this.pnlTIList.SuspendLayout();
			this.pnlTIName.SuspendLayout();
			this.pnlLog.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.pnlGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.pnlTI);
			this.pnlLeft.Controls.Add(this.label2);
			this.pnlLeft.Controls.Add(this.label1);
			this.pnlLeft.Controls.Add(this.chbStep);
			this.pnlLeft.Controls.Add(this.chbHH);
			this.pnlLeft.Controls.Add(this.DTPStart);
			this.pnlLeft.Controls.Add(this.txtTI);
			this.pnlLeft.Controls.Add(this.btnShow);
			this.pnlLeft.Controls.Add(this.txtStep);
			this.pnlLeft.Controls.Add(this.DTPEnd);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(478, 429);
			this.pnlLeft.TabIndex = 7;
			// 
			// pnlTI
			// 
			this.pnlTI.Controls.Add(this.pnlTIList);
			this.pnlTI.Controls.Add(this.pnlTIName);
			this.pnlTI.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlTI.Location = new System.Drawing.Point(167, 0);
			this.pnlTI.Name = "pnlTI";
			this.pnlTI.Size = new System.Drawing.Size(311, 429);
			this.pnlTI.TabIndex = 10;
			// 
			// pnlTIList
			// 
			this.pnlTIList.Controls.Add(this.lbItems);
			this.pnlTIList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTIList.Location = new System.Drawing.Point(0, 25);
			this.pnlTIList.Name = "pnlTIList";
			this.pnlTIList.Size = new System.Drawing.Size(311, 404);
			this.pnlTIList.TabIndex = 11;
			// 
			// lbItems
			// 
			this.lbItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbItems.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbItems.FormattingEnabled = true;
			this.lbItems.ItemHeight = 14;
			this.lbItems.Location = new System.Drawing.Point(0, 0);
			this.lbItems.Name = "lbItems";
			this.lbItems.Size = new System.Drawing.Size(311, 404);
			this.lbItems.TabIndex = 10;
			this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
			// 
			// pnlTIName
			// 
			this.pnlTIName.Controls.Add(this.textBox1);
			this.pnlTIName.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTIName.Location = new System.Drawing.Point(0, 0);
			this.pnlTIName.Name = "pnlTIName";
			this.pnlTIName.Size = new System.Drawing.Size(311, 25);
			this.pnlTIName.TabIndex = 10;
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(0, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(308, 20);
			this.textBox1.TabIndex = 11;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(4, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 14);
			this.label2.TabIndex = 7;
			this.label2.Text = "Step (Sec)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(7, 98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 14);
			this.label1.TabIndex = 7;
			this.label1.Text = "№ TI";
			// 
			// chbStep
			// 
			this.chbStep.AutoSize = true;
			this.chbStep.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.chbStep.Location = new System.Drawing.Point(92, 130);
			this.chbStep.Name = "chbStep";
			this.chbStep.Size = new System.Drawing.Size(54, 18);
			this.chbStep.TabIndex = 6;
			this.chbStep.Text = "Step";
			this.chbStep.UseVisualStyleBackColor = true;
			// 
			// chbHH
			// 
			this.chbHH.AutoSize = true;
			this.chbHH.Checked = true;
			this.chbHH.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbHH.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.chbHH.Location = new System.Drawing.Point(11, 130);
			this.chbHH.Name = "chbHH";
			this.chbHH.Size = new System.Drawing.Size(40, 18);
			this.chbHH.TabIndex = 6;
			this.chbHH.Text = "HH";
			this.chbHH.UseVisualStyleBackColor = true;
			// 
			// DTPStart
			// 
			this.DTPStart.CustomFormat = "dd.MM.yyyy HH:mm";
			this.DTPStart.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DTPStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DTPStart.Location = new System.Drawing.Point(6, 17);
			this.DTPStart.Name = "DTPStart";
			this.DTPStart.ShowUpDown = true;
			this.DTPStart.Size = new System.Drawing.Size(140, 20);
			this.DTPStart.TabIndex = 4;
			this.DTPStart.Value = new System.DateTime(2017, 4, 13, 15, 52, 38, 0);
			// 
			// txtTI
			// 
			this.txtTI.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtTI.Location = new System.Drawing.Point(49, 95);
			this.txtTI.Name = "txtTI";
			this.txtTI.Size = new System.Drawing.Size(97, 20);
			this.txtTI.TabIndex = 5;
			this.txtTI.Text = "0";
			// 
			// btnShow
			// 
			this.btnShow.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnShow.Location = new System.Drawing.Point(32, 154);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(75, 23);
			this.btnShow.TabIndex = 3;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtStep
			// 
			this.txtStep.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtStep.Location = new System.Drawing.Point(87, 69);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(59, 20);
			this.txtStep.TabIndex = 5;
			this.txtStep.Text = "60";
			// 
			// DTPEnd
			// 
			this.DTPEnd.CustomFormat = "dd.MM.yyyy HH:mm";
			this.DTPEnd.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DTPEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DTPEnd.Location = new System.Drawing.Point(7, 43);
			this.DTPEnd.Name = "DTPEnd";
			this.DTPEnd.ShowUpDown = true;
			this.DTPEnd.Size = new System.Drawing.Size(139, 20);
			this.DTPEnd.TabIndex = 4;
			this.DTPEnd.Value = new System.DateTime(2017, 4, 13, 15, 52, 47, 0);
			// 
			// tcKotmiData
			// 
			this.tcKotmiData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcKotmiData.Location = new System.Drawing.Point(0, 0);
			this.tcKotmiData.Name = "tcKotmiData";
			this.tcKotmiData.SelectedIndex = 0;
			this.tcKotmiData.Size = new System.Drawing.Size(680, 429);
			this.tcKotmiData.TabIndex = 8;
			this.tcKotmiData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tcKotmiData_MouseDoubleClick);
			// 
			// pnlLog
			// 
			this.pnlLog.Controls.Add(this.btnBreak);
			this.pnlLog.Controls.Add(this.txtLog);
			this.pnlLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlLog.Location = new System.Drawing.Point(0, 429);
			this.pnlLog.Name = "pnlLog";
			this.pnlLog.Size = new System.Drawing.Size(1158, 174);
			this.pnlLog.TabIndex = 9;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.pnlGrid);
			this.pnlTop.Controls.Add(this.pnlLeft);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(1158, 429);
			this.pnlTop.TabIndex = 10;
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.EnableAutoDragDrop = true;
			this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F);
			this.txtLog.Location = new System.Drawing.Point(0, 0);
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(1158, 174);
			this.txtLog.TabIndex = 0;
			this.txtLog.Text = "";
			// 
			// pnlGrid
			// 
			this.pnlGrid.Controls.Add(this.tcKotmiData);
			this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGrid.Location = new System.Drawing.Point(478, 0);
			this.pnlGrid.Name = "pnlGrid";
			this.pnlGrid.Size = new System.Drawing.Size(680, 429);
			this.pnlGrid.TabIndex = 9;
			// 
			// btnBreak
			// 
			this.btnBreak.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnBreak.Location = new System.Drawing.Point(1059, 6);
			this.btnBreak.Name = "btnBreak";
			this.btnBreak.Size = new System.Drawing.Size(75, 23);
			this.btnBreak.TabIndex = 3;
			this.btnBreak.Text = "Break";
			this.btnBreak.UseVisualStyleBackColor = true;
			this.btnBreak.Click += new System.EventHandler(this.btnBreak_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1158, 603);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlLog);
			this.Name = "Form1";
			this.Text = "Form1";
			this.pnlLeft.ResumeLayout(false);
			this.pnlLeft.PerformLayout();
			this.pnlTI.ResumeLayout(false);
			this.pnlTIList.ResumeLayout(false);
			this.pnlTIName.ResumeLayout(false);
			this.pnlTIName.PerformLayout();
			this.pnlLog.ResumeLayout(false);
			this.pnlTop.ResumeLayout(false);
			this.pnlGrid.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbStep;
		private System.Windows.Forms.CheckBox chbHH;
		private System.Windows.Forms.DateTimePicker DTPStart;
		private System.Windows.Forms.TextBox txtTI;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.TextBox txtStep;
		private System.Windows.Forms.DateTimePicker DTPEnd;
		private System.Windows.Forms.TabControl tcKotmiData;
		private System.Windows.Forms.Panel pnlTI;
		private System.Windows.Forms.Panel pnlTIList;
		private System.Windows.Forms.ListBox lbItems;
		private System.Windows.Forms.Panel pnlTIName;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel pnlLog;
		private System.Windows.Forms.RichTextBox txtLog;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlGrid;
		private System.Windows.Forms.Button btnBreak;
	}
}

