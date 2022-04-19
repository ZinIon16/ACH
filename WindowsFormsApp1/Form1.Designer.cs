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
            System.Windows.Forms.Button btnBrowse;
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtFilename = new System.Windows.Forms.Label();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtSheet = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtBank = new System.Windows.Forms.Label();
            this.cboBank = new System.Windows.Forms.ComboBox();
            this.lblAcc = new System.Windows.Forms.Label();
            this.lblNoOfDays = new System.Windows.Forms.Label();
            this.lblFileNo = new System.Windows.Forms.Label();
            this.txtAcc = new System.Windows.Forms.TextBox();
            this.txtNoOfDays = new System.Windows.Forms.TextBox();
            this.txtFileNo = new System.Windows.Forms.TextBox();
            this.lblOrID = new System.Windows.Forms.Label();
            this.txtOrID = new System.Windows.Forms.TextBox();
            this.labelDesDataCenter = new System.Windows.Forms.Label();
            this.txtlDesDataCenter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtCName = new System.Windows.Forms.TextBox();
            this.lblCName = new System.Windows.Forms.Label();
            this.lblTco = new System.Windows.Forms.Label();
            this.txtTco = new System.Windows.Forms.TextBox();
            this.lblCBrNo = new System.Windows.Forms.Label();
            this.lblCBNo = new System.Windows.Forms.Label();
            this.txtCBrNo = new System.Windows.Forms.TextBox();
            this.txtCBNo = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new System.Drawing.Point(710, 291);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse..";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(773, 257);
            this.dataGridView1.TabIndex = 1;
            // 
            // txtFilename
            // 
            this.txtFilename.AutoSize = true;
            this.txtFilename.Location = new System.Drawing.Point(12, 291);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(51, 13);
            this.txtFilename.TabIndex = 2;
            this.txtFilename.Text = "FileName";
            // 
            // cboSheet
            // 
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(67, 323);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(121, 21);
            this.cboSheet.TabIndex = 3;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cboSheet_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 291);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(637, 20);
            this.textBox1.TabIndex = 4;
            // 
            // txtSheet
            // 
            this.txtSheet.AutoSize = true;
            this.txtSheet.Location = new System.Drawing.Point(12, 328);
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(35, 13);
            this.txtSheet.TabIndex = 5;
            this.txtSheet.Text = "Sheet";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(710, 323);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtBank
            // 
            this.txtBank.AutoSize = true;
            this.txtBank.Location = new System.Drawing.Point(514, 326);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(63, 13);
            this.txtBank.TabIndex = 7;
            this.txtBank.Text = "Bank Name";
            // 
            // cboBank
            // 
            this.cboBank.FormattingEnabled = true;
            this.cboBank.Location = new System.Drawing.Point(583, 323);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(121, 21);
            this.cboBank.TabIndex = 8;
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.Location = new System.Drawing.Point(483, 415);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(94, 13);
            this.lblAcc.TabIndex = 9;
            this.lblAcc.Text = "Company Account";
            // 
            // lblNoOfDays
            // 
            this.lblNoOfDays.AutoSize = true;
            this.lblNoOfDays.Location = new System.Drawing.Point(514, 389);
            this.lblNoOfDays.Name = "lblNoOfDays";
            this.lblNoOfDays.Size = new System.Drawing.Size(58, 13);
            this.lblNoOfDays.TabIndex = 10;
            this.lblNoOfDays.Text = "No of days";
            // 
            // lblFileNo
            // 
            this.lblFileNo.AutoSize = true;
            this.lblFileNo.Location = new System.Drawing.Point(514, 361);
            this.lblFileNo.Name = "lblFileNo";
            this.lblFileNo.Size = new System.Drawing.Size(63, 13);
            this.lblFileNo.TabIndex = 11;
            this.lblFileNo.Text = "File Number";
            // 
            // txtAcc
            // 
            this.txtAcc.Location = new System.Drawing.Point(583, 413);
            this.txtAcc.Name = "txtAcc";
            this.txtAcc.Size = new System.Drawing.Size(100, 20);
            this.txtAcc.TabIndex = 12;
            // 
            // txtNoOfDays
            // 
            this.txtNoOfDays.Location = new System.Drawing.Point(583, 389);
            this.txtNoOfDays.Name = "txtNoOfDays";
            this.txtNoOfDays.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfDays.TabIndex = 13;
            // 
            // txtFileNo
            // 
            this.txtFileNo.Location = new System.Drawing.Point(583, 356);
            this.txtFileNo.Name = "txtFileNo";
            this.txtFileNo.Size = new System.Drawing.Size(100, 20);
            this.txtFileNo.TabIndex = 14;
            // 
            // lblOrID
            // 
            this.lblOrID.AutoSize = true;
            this.lblOrID.Location = new System.Drawing.Point(283, 392);
            this.lblOrID.Name = "lblOrID";
            this.lblOrID.Size = new System.Drawing.Size(66, 13);
            this.lblOrID.TabIndex = 15;
            this.lblOrID.Text = "Originator ID";
            // 
            // txtOrID
            // 
            this.txtOrID.Location = new System.Drawing.Point(355, 386);
            this.txtOrID.Name = "txtOrID";
            this.txtOrID.Size = new System.Drawing.Size(100, 20);
            this.txtOrID.TabIndex = 16;
            // 
            // labelDesDataCenter
            // 
            this.labelDesDataCenter.AutoSize = true;
            this.labelDesDataCenter.Location = new System.Drawing.Point(229, 361);
            this.labelDesDataCenter.Name = "labelDesDataCenter";
            this.labelDesDataCenter.Size = new System.Drawing.Size(120, 13);
            this.labelDesDataCenter.TabIndex = 17;
            this.labelDesDataCenter.Text = "Destination Data Center";
            // 
            // txtlDesDataCenter
            // 
            this.txtlDesDataCenter.Location = new System.Drawing.Point(355, 358);
            this.txtlDesDataCenter.Name = "txtlDesDataCenter";
            this.txtlDesDataCenter.Size = new System.Drawing.Size(100, 20);
            this.txtlDesDataCenter.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 331);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Header";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(355, 326);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(100, 20);
            this.txtHeader.TabIndex = 20;
            // 
            // txtCName
            // 
            this.txtCName.Location = new System.Drawing.Point(123, 356);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(100, 20);
            this.txtCName.TabIndex = 21;
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Location = new System.Drawing.Point(12, 361);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(82, 13);
            this.lblCName.TabIndex = 22;
            this.lblCName.Text = "Company Name";
            // 
            // lblTco
            // 
            this.lblTco.AutoSize = true;
            this.lblTco.Location = new System.Drawing.Point(258, 416);
            this.lblTco.Name = "lblTco";
            this.lblTco.Size = new System.Drawing.Size(91, 13);
            this.lblTco.TabIndex = 23;
            this.lblTco.Text = "Transaction Code";
            // 
            // txtTco
            // 
            this.txtTco.Location = new System.Drawing.Point(355, 413);
            this.txtTco.Name = "txtTco";
            this.txtTco.Size = new System.Drawing.Size(100, 20);
            this.txtTco.TabIndex = 24;
            // 
            // lblCBrNo
            // 
            this.lblCBrNo.AutoSize = true;
            this.lblCBrNo.Location = new System.Drawing.Point(12, 416);
            this.lblCBrNo.Name = "lblCBrNo";
            this.lblCBrNo.Size = new System.Drawing.Size(108, 13);
            this.lblCBrNo.TabIndex = 25;
            this.lblCBrNo.Text = "Company Branch No.";
            // 
            // lblCBNo
            // 
            this.lblCBNo.AutoSize = true;
            this.lblCBNo.Location = new System.Drawing.Point(12, 392);
            this.lblCBNo.Name = "lblCBNo";
            this.lblCBNo.Size = new System.Drawing.Size(99, 13);
            this.lblCBNo.TabIndex = 26;
            this.lblCBNo.Text = "Company Bank No.";
            // 
            // txtCBrNo
            // 
            this.txtCBrNo.Location = new System.Drawing.Point(123, 408);
            this.txtCBrNo.Name = "txtCBrNo";
            this.txtCBrNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBrNo.TabIndex = 27;
            // 
            // txtCBNo
            // 
            this.txtCBNo.Location = new System.Drawing.Point(123, 385);
            this.txtCBNo.Name = "txtCBNo";
            this.txtCBNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBNo.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 450);
            this.Controls.Add(this.txtCBNo);
            this.Controls.Add(this.txtCBrNo);
            this.Controls.Add(this.lblCBNo);
            this.Controls.Add(this.lblCBrNo);
            this.Controls.Add(this.txtTco);
            this.Controls.Add(this.lblTco);
            this.Controls.Add(this.lblCName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtlDesDataCenter);
            this.Controls.Add(this.labelDesDataCenter);
            this.Controls.Add(this.txtOrID);
            this.Controls.Add(this.lblOrID);
            this.Controls.Add(this.txtFileNo);
            this.Controls.Add(this.txtNoOfDays);
            this.Controls.Add(this.txtAcc);
            this.Controls.Add(this.lblFileNo);
            this.Controls.Add(this.lblNoOfDays);
            this.Controls.Add(this.lblAcc);
            this.Controls.Add(this.cboBank);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtSheet);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(btnBrowse);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read Text";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label txtFilename;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label txtSheet;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label txtBank;
        private System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.Label lblNoOfDays;
        private System.Windows.Forms.Label lblFileNo;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.TextBox txtNoOfDays;
        private System.Windows.Forms.TextBox txtFileNo;
        private System.Windows.Forms.Label lblOrID;
        private System.Windows.Forms.TextBox txtOrID;
        private System.Windows.Forms.Label labelDesDataCenter;
        private System.Windows.Forms.TextBox txtlDesDataCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtCName;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.Label lblTco;
        private System.Windows.Forms.TextBox txtTco;
        private System.Windows.Forms.Label lblCBrNo;
        private System.Windows.Forms.Label lblCBNo;
        private System.Windows.Forms.TextBox txtCBrNo;
        private System.Windows.Forms.TextBox txtCBNo;
    }
}

