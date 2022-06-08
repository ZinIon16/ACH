namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtBank = new System.Windows.Forms.Label();
            this.lblAcc = new System.Windows.Forms.Label();
            this.lblFileNo = new System.Windows.Forms.Label();
            this.txtAcc = new System.Windows.Forms.TextBox();
            this.txtFileNo = new System.Windows.Forms.TextBox();
            this.lblOrID = new System.Windows.Forms.Label();
            this.txtOrID = new System.Windows.Forms.TextBox();
            this.labelDesDataCenter = new System.Windows.Forms.Label();
            this.txtlDesDataCenter = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtCName = new System.Windows.Forms.TextBox();
            this.lblCName = new System.Windows.Forms.Label();
            this.lblCBrNo = new System.Windows.Forms.Label();
            this.lblCBNo = new System.Windows.Forms.Label();
            this.txtCBrNo = new System.Windows.Forms.TextBox();
            this.txtCBNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportxl = new System.Windows.Forms.Button();
            this.lblOrIDBMO = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblTotAmount = new System.Windows.Forms.Label();
            this.TotAmount = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtFileType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(773, 257);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExport.Location = new System.Drawing.Point(272, 423);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(99, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export as text file";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtBank
            // 
            this.txtBank.AutoSize = true;
            this.txtBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBank.Location = new System.Drawing.Point(612, 311);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(63, 13);
            this.txtBank.TabIndex = 7;
            this.txtBank.Text = "Bank Name";
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAcc.Location = new System.Drawing.Point(277, 378);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(94, 13);
            this.lblAcc.TabIndex = 9;
            this.lblAcc.Text = "Company Account";
            // 
            // lblFileNo
            // 
            this.lblFileNo.AutoSize = true;
            this.lblFileNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileNo.Location = new System.Drawing.Point(612, 344);
            this.lblFileNo.Name = "lblFileNo";
            this.lblFileNo.Size = new System.Drawing.Size(63, 13);
            this.lblFileNo.TabIndex = 11;
            this.lblFileNo.Text = "File Number";
            // 
            // txtAcc
            // 
            this.txtAcc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAcc.Location = new System.Drawing.Point(377, 370);
            this.txtAcc.Name = "txtAcc";
            this.txtAcc.Size = new System.Drawing.Size(100, 20);
            this.txtAcc.TabIndex = 12;
            // 
            // txtFileNo
            // 
            this.txtFileNo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtFileNo.Location = new System.Drawing.Point(681, 338);
            this.txtFileNo.Name = "txtFileNo";
            this.txtFileNo.Size = new System.Drawing.Size(100, 20);
            this.txtFileNo.TabIndex = 14;
            // 
            // lblOrID
            // 
            this.lblOrID.AutoSize = true;
            this.lblOrID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrID.Location = new System.Drawing.Point(305, 345);
            this.lblOrID.Name = "lblOrID";
            this.lblOrID.Size = new System.Drawing.Size(66, 13);
            this.lblOrID.TabIndex = 15;
            this.lblOrID.Text = "Originator ID";
            // 
            // txtOrID
            // 
            this.txtOrID.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtOrID.Location = new System.Drawing.Point(377, 341);
            this.txtOrID.Name = "txtOrID";
            this.txtOrID.Size = new System.Drawing.Size(100, 20);
            this.txtOrID.TabIndex = 16;
            // 
            // labelDesDataCenter
            // 
            this.labelDesDataCenter.AutoSize = true;
            this.labelDesDataCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDesDataCenter.Location = new System.Drawing.Point(555, 377);
            this.labelDesDataCenter.Name = "labelDesDataCenter";
            this.labelDesDataCenter.Size = new System.Drawing.Size(120, 13);
            this.labelDesDataCenter.TabIndex = 17;
            this.labelDesDataCenter.Text = "Destination Data Center";
            // 
            // txtlDesDataCenter
            // 
            this.txtlDesDataCenter.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtlDesDataCenter.Location = new System.Drawing.Point(681, 371);
            this.txtlDesDataCenter.Name = "txtlDesDataCenter";
            this.txtlDesDataCenter.Size = new System.Drawing.Size(100, 20);
            this.txtlDesDataCenter.TabIndex = 18;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHeader.Location = new System.Drawing.Point(633, 410);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(42, 13);
            this.lblHeader.TabIndex = 19;
            this.lblHeader.Text = "Header";
            // 
            // txtHeader
            // 
            this.txtHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtHeader.Location = new System.Drawing.Point(681, 404);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(100, 20);
            this.txtHeader.TabIndex = 20;
            // 
            // txtCName
            // 
            this.txtCName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCName.Location = new System.Drawing.Point(122, 338);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(100, 20);
            this.txtCName.TabIndex = 21;
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCName.Location = new System.Drawing.Point(11, 344);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(82, 13);
            this.lblCName.TabIndex = 22;
            this.lblCName.Text = "Company Name";
            // 
            // lblCBrNo
            // 
            this.lblCBrNo.AutoSize = true;
            this.lblCBrNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCBrNo.Location = new System.Drawing.Point(12, 410);
            this.lblCBrNo.Name = "lblCBrNo";
            this.lblCBrNo.Size = new System.Drawing.Size(108, 13);
            this.lblCBrNo.TabIndex = 25;
            this.lblCBrNo.Text = "Company Branch No.";
            // 
            // lblCBNo
            // 
            this.lblCBNo.AutoSize = true;
            this.lblCBNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCBNo.Location = new System.Drawing.Point(11, 377);
            this.lblCBNo.Name = "lblCBNo";
            this.lblCBNo.Size = new System.Drawing.Size(99, 13);
            this.lblCBNo.TabIndex = 26;
            this.lblCBNo.Text = "Company Bank No.";
            // 
            // txtCBrNo
            // 
            this.txtCBrNo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCBrNo.Location = new System.Drawing.Point(122, 404);
            this.txtCBrNo.Name = "txtCBrNo";
            this.txtCBrNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBrNo.TabIndex = 27;
            // 
            // txtCBNo
            // 
            this.txtCBNo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCBNo.Location = new System.Drawing.Point(122, 371);
            this.txtCBNo.Name = "txtCBNo";
            this.txtCBNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBNo.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mistral", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(12, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "zayn";
            // 
            // btnExportxl
            // 
            this.btnExportxl.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExportxl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportxl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExportxl.Location = new System.Drawing.Point(377, 423);
            this.btnExportxl.Name = "btnExportxl";
            this.btnExportxl.Size = new System.Drawing.Size(106, 23);
            this.btnExportxl.TabIndex = 31;
            this.btnExportxl.Text = "Export as excel file";
            this.btnExportxl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExportxl.UseVisualStyleBackColor = false;
            this.btnExportxl.Click += new System.EventHandler(this.btnExportxl_Click);
            // 
            // lblOrIDBMO
            // 
            this.lblOrIDBMO.AutoSize = true;
            this.lblOrIDBMO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrIDBMO.Location = new System.Drawing.Point(305, 344);
            this.lblOrIDBMO.Name = "lblOrIDBMO";
            this.lblOrIDBMO.Size = new System.Drawing.Size(66, 13);
            this.lblOrIDBMO.TabIndex = 32;
            this.lblOrIDBMO.Text = "Originator ID";
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileType.Location = new System.Drawing.Point(12, 311);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(50, 13);
            this.lblFileType.TabIndex = 34;
            this.lblFileType.Text = "File Type";
            // 
            // lblTotAmount
            // 
            this.lblTotAmount.AutoSize = true;
            this.lblTotAmount.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotAmount.Location = new System.Drawing.Point(277, 276);
            this.lblTotAmount.Name = "lblTotAmount";
            this.lblTotAmount.Size = new System.Drawing.Size(97, 16);
            this.lblTotAmount.TabIndex = 35;
            this.lblTotAmount.Text = "Total Amount:";
            this.lblTotAmount.Visible = false;
            // 
            // TotAmount
            // 
            this.TotAmount.AutoSize = true;
            this.TotAmount.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotAmount.Location = new System.Drawing.Point(410, 276);
            this.TotAmount.Name = "TotAmount";
            this.TotAmount.Size = new System.Drawing.Size(14, 16);
            this.TotAmount.TabIndex = 36;
            this.TotAmount.Text = "0";
            this.TotAmount.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(489, 423);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(118, 23);
            this.btnDelete.TabIndex = 38;
            this.btnDelete.Text = "Delete Selected Row";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDate.Location = new System.Drawing.Point(341, 312);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(30, 13);
            this.lblDate.TabIndex = 40;
            this.lblDate.Text = "Date";
            // 
            // txtDate
            // 
            this.txtDate.CalendarForeColor = System.Drawing.Color.Aqua;
            this.txtDate.CalendarMonthBackground = System.Drawing.Color.AliceBlue;
            this.txtDate.Location = new System.Drawing.Point(377, 306);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(187, 20);
            this.txtDate.TabIndex = 41;
            // 
            // txtBankName
            // 
            this.txtBankName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBankName.Location = new System.Drawing.Point(681, 305);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(100, 20);
            this.txtBankName.TabIndex = 42;
            // 
            // txtFileType
            // 
            this.txtFileType.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtFileType.Location = new System.Drawing.Point(122, 305);
            this.txtFileType.Name = "txtFileType";
            this.txtFileType.Size = new System.Drawing.Size(100, 20);
            this.txtFileType.TabIndex = 43;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(794, 450);
            this.Controls.Add(this.txtFileType);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.TotAmount);
            this.Controls.Add(this.lblTotAmount);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.lblOrIDBMO);
            this.Controls.Add(this.btnExportxl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCBNo);
            this.Controls.Add(this.txtCBrNo);
            this.Controls.Add(this.lblCBNo);
            this.Controls.Add(this.lblCBrNo);
            this.Controls.Add(this.lblCName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtlDesDataCenter);
            this.Controls.Add(this.labelDesDataCenter);
            this.Controls.Add(this.txtOrID);
            this.Controls.Add(this.lblOrID);
            this.Controls.Add(this.txtFileNo);
            this.Controls.Add(this.txtAcc);
            this.Controls.Add(this.lblFileNo);
            this.Controls.Add(this.lblAcc);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACH";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label txtBank;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.Label lblFileNo;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.TextBox txtFileNo;
        private System.Windows.Forms.Label lblOrID;
        private System.Windows.Forms.TextBox txtOrID;
        private System.Windows.Forms.Label labelDesDataCenter;
        private System.Windows.Forms.TextBox txtlDesDataCenter;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtCName;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.Label lblCBrNo;
        private System.Windows.Forms.Label lblCBNo;
        private System.Windows.Forms.TextBox txtCBrNo;
        private System.Windows.Forms.TextBox txtCBNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportxl;
        private System.Windows.Forms.Label lblOrIDBMO;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblTotAmount;
        private System.Windows.Forms.Label TotAmount;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtFileType;
    }
}

