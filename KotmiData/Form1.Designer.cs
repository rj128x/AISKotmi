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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.axScadaCli1 = new AxScdSys.AxScadaCli();
			this.axScadaAbo1 = new AxScdSys.AxScadaAbo();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tcKotmiData = new System.Windows.Forms.TabControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DTPStart = new System.Windows.Forms.DateTimePicker();
			this.txtTI = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txtStep = new System.Windows.Forms.TextBox();
			this.DTPEnd = new System.Windows.Forms.DateTimePicker();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chbHH = new System.Windows.Forms.CheckBox();
			this.chbStep = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lbItems = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.axScadaCli1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axScadaAbo1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// axScadaCli1
			// 
			this.axScadaCli1.Location = new System.Drawing.Point(13, 13);
			this.axScadaCli1.Name = "axScadaCli1";
			this.axScadaCli1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axScadaCli1.OcxState")));
			this.axScadaCli1.Size = new System.Drawing.Size(75, 23);
			this.axScadaCli1.TabIndex = 0;
			// 
			// axScadaAbo1
			// 
			this.axScadaAbo1.Location = new System.Drawing.Point(1, 2);
			this.axScadaAbo1.Name = "axScadaAbo1";
			this.axScadaAbo1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axScadaAbo1.OcxState")));
			this.axScadaAbo1.Size = new System.Drawing.Size(75, 23);
			this.axScadaAbo1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(915, 458);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(907, 432);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tcKotmiData);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(445, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(459, 426);
			this.panel2.TabIndex = 7;
			// 
			// tcKotmiData
			// 
			this.tcKotmiData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcKotmiData.Location = new System.Drawing.Point(0, 0);
			this.tcKotmiData.Name = "tcKotmiData";
			this.tcKotmiData.SelectedIndex = 0;
			this.tcKotmiData.Size = new System.Drawing.Size(459, 426);
			this.tcKotmiData.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lbItems);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.chbStep);
			this.panel1.Controls.Add(this.chbHH);
			this.panel1.Controls.Add(this.DTPStart);
			this.panel1.Controls.Add(this.txtTI);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.txtStep);
			this.panel1.Controls.Add(this.DTPEnd);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(442, 426);
			this.panel1.TabIndex = 6;
			// 
			// DTPStart
			// 
			this.DTPStart.CustomFormat = "dd.MM.yyyy HH:mm";
			this.DTPStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DTPStart.Location = new System.Drawing.Point(6, 17);
			this.DTPStart.Name = "DTPStart";
			this.DTPStart.ShowUpDown = true;
			this.DTPStart.Size = new System.Drawing.Size(112, 20);
			this.DTPStart.TabIndex = 4;
			this.DTPStart.Value = new System.DateTime(2017, 4, 13, 15, 52, 38, 0);
			// 
			// txtTI
			// 
			this.txtTI.Location = new System.Drawing.Point(78, 95);
			this.txtTI.Name = "txtTI";
			this.txtTI.Size = new System.Drawing.Size(40, 20);
			this.txtTI.TabIndex = 5;
			this.txtTI.Text = "0";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(11, 164);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Show";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtStep
			// 
			this.txtStep.Location = new System.Drawing.Point(79, 69);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(40, 20);
			this.txtStep.TabIndex = 5;
			this.txtStep.Text = "60";
			// 
			// DTPEnd
			// 
			this.DTPEnd.CustomFormat = "dd.MM.yyyy HH:mm";
			this.DTPEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DTPEnd.Location = new System.Drawing.Point(7, 43);
			this.DTPEnd.Name = "DTPEnd";
			this.DTPEnd.ShowUpDown = true;
			this.DTPEnd.Size = new System.Drawing.Size(112, 20);
			this.DTPEnd.TabIndex = 4;
			this.DTPEnd.Value = new System.DateTime(2017, 4, 13, 15, 52, 47, 0);
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(907, 432);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// chbHH
			// 
			this.chbHH.AutoSize = true;
			this.chbHH.Location = new System.Drawing.Point(11, 130);
			this.chbHH.Name = "chbHH";
			this.chbHH.Size = new System.Drawing.Size(42, 17);
			this.chbHH.TabIndex = 6;
			this.chbHH.Text = "HH";
			this.chbHH.UseVisualStyleBackColor = true;
			// 
			// chbStep
			// 
			this.chbStep.AutoSize = true;
			this.chbStep.Location = new System.Drawing.Point(59, 130);
			this.chbStep.Name = "chbStep";
			this.chbStep.Size = new System.Drawing.Size(48, 17);
			this.chbStep.TabIndex = 6;
			this.chbStep.Text = "Step";
			this.chbStep.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(55, 98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "TI";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Step (Sec)";
			// 
			// lbItems
			// 
			this.lbItems.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbItems.FormattingEnabled = true;
			this.lbItems.Location = new System.Drawing.Point(140, 0);
			this.lbItems.Name = "lbItems";
			this.lbItems.Size = new System.Drawing.Size(302, 426);
			this.lbItems.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(915, 458);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.axScadaAbo1);
			this.Controls.Add(this.axScadaCli1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.axScadaCli1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axScadaAbo1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private AxScdSys.AxScadaCli axScadaCli1;
		private AxScdSys.AxScadaAbo axScadaAbo1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DateTimePicker DTPStart;
		private System.Windows.Forms.DateTimePicker DTPEnd;
		private System.Windows.Forms.TextBox txtStep;
		private System.Windows.Forms.TextBox txtTI;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tcKotmiData;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbStep;
		private System.Windows.Forms.CheckBox chbHH;
		private System.Windows.Forms.ListBox lbItems;
	}
}

