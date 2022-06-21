namespace WindowsFormsApp1
{
    partial class Start
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.GridView = new System.Windows.Forms.DataGridView();
            this.lblFileType = new System.Windows.Forms.Label();
            this.cboOrID = new System.Windows.Forms.ComboBox();
            this.cboBank = new System.Windows.Forms.ComboBox();
            this.txtBank = new System.Windows.Forms.Label();
            this.txtSheet = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.txtFilename = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddBank = new System.Windows.Forms.Button();
            this.lblBankType = new System.Windows.Forms.Label();
            this.cboBankType = new System.Windows.Forms.ComboBox();
            btnBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            btnBrowse.Location = new System.Drawing.Point(716, 306);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(75, 23);
            btnBrowse.TabIndex = 41;
            btnBrowse.Text = "Browse..";
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // GridView
            // 
            this.GridView.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.GridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.GridColor = System.Drawing.SystemColors.InactiveCaption;
            this.GridView.Location = new System.Drawing.Point(12, 22);
            this.GridView.Name = "GridView";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.GridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridView.Size = new System.Drawing.Size(773, 257);
            this.GridView.TabIndex = 2;
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileType.Location = new System.Drawing.Point(312, 349);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(50, 13);
            this.lblFileType.TabIndex = 51;
            this.lblFileType.Text = "File Type";
            // 
            // cboOrID
            // 
            this.cboOrID.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboOrID.FormattingEnabled = true;
            this.cboOrID.Location = new System.Drawing.Point(380, 341);
            this.cboOrID.Name = "cboOrID";
            this.cboOrID.Size = new System.Drawing.Size(100, 21);
            this.cboOrID.TabIndex = 50;
            this.cboOrID.SelectedIndexChanged += new System.EventHandler(this.cboOrID_SelectedIndexChanged);
            // 
            // cboBank
            // 
            this.cboBank.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboBank.DropDownWidth = 280;
            this.cboBank.FormattingEnabled = true;
            this.cboBank.Location = new System.Drawing.Point(688, 341);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(100, 21);
            this.cboBank.TabIndex = 47;
            this.cboBank.SelectedIndexChanged += new System.EventHandler(this.cboBank_SelectedIndexChanged);
            // 
            // txtBank
            // 
            this.txtBank.AutoSize = true;
            this.txtBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBank.Location = new System.Drawing.Point(606, 349);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(63, 13);
            this.txtBank.TabIndex = 46;
            this.txtBank.Text = "Bank Name";
            // 
            // txtSheet
            // 
            this.txtSheet.AutoSize = true;
            this.txtSheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSheet.Location = new System.Drawing.Point(18, 349);
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(35, 13);
            this.txtSheet.TabIndex = 45;
            this.txtSheet.Text = "Sheet";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(73, 306);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(637, 20);
            this.textBox1.TabIndex = 44;
            // 
            // cboSheet
            // 
            this.cboSheet.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(73, 341);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(100, 21);
            this.cboSheet.TabIndex = 43;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cboSheet_SelectedIndexChanged);
            // 
            // txtFilename
            // 
            this.txtFilename.AutoSize = true;
            this.txtFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFilename.Location = new System.Drawing.Point(18, 306);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(51, 13);
            this.txtFilename.TabIndex = 42;
            this.txtFilename.Text = "FileName";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNext.Location = new System.Drawing.Point(477, 401);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(79, 23);
            this.btnNext.TabIndex = 52;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUpdate.Location = new System.Drawing.Point(396, 401);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 53;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddBank
            // 
            this.btnAddBank.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAddBank.Location = new System.Drawing.Point(301, 401);
            this.btnAddBank.Name = "btnAddBank";
            this.btnAddBank.Size = new System.Drawing.Size(89, 23);
            this.btnAddBank.TabIndex = 54;
            this.btnAddBank.Text = "Add New Bank";
            this.btnAddBank.UseVisualStyleBackColor = false;
            this.btnAddBank.Click += new System.EventHandler(this.btnAddBank_Click);
            // 
            // lblBankType
            // 
            this.lblBankType.AutoSize = true;
            this.lblBankType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBankType.Location = new System.Drawing.Point(9, 406);
            this.lblBankType.Name = "lblBankType";
            this.lblBankType.Size = new System.Drawing.Size(66, 13);
            this.lblBankType.TabIndex = 57;
            this.lblBankType.Text = "* Bank Type";
            // 
            // cboBankType
            // 
            this.cboBankType.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboBankType.FormattingEnabled = true;
            this.cboBankType.Location = new System.Drawing.Point(73, 401);
            this.cboBankType.Name = "cboBankType";
            this.cboBankType.Size = new System.Drawing.Size(100, 21);
            this.cboBankType.TabIndex = 56;
            this.cboBankType.SelectedIndexChanged += new System.EventHandler(this.cboBankType_SelectedIndexChanged);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(794, 450);
            this.Controls.Add(this.lblBankType);
            this.Controls.Add(this.cboBankType);
            this.Controls.Add(this.btnAddBank);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.cboOrID);
            this.Controls.Add(this.cboBank);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.txtSheet);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(btnBrowse);
            this.Controls.Add(this.GridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACH";
            this.Load += new System.EventHandler(this.Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.ComboBox cboOrID;
        private System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.Label txtBank;
        private System.Windows.Forms.Label txtSheet;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.Label txtFilename;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAddBank;
        private System.Windows.Forms.Label lblBankType;
        private System.Windows.Forms.ComboBox cboBankType;
    }
}