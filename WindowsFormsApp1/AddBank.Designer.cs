namespace WindowsFormsApp1
{
    partial class AddBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBank));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBankType = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.txtCBNo = new System.Windows.Forms.TextBox();
            this.txtCBrNo = new System.Windows.Forms.TextBox();
            this.lblCBNo = new System.Windows.Forms.Label();
            this.lblCBrNo = new System.Windows.Forms.Label();
            this.lblCName = new System.Windows.Forms.Label();
            this.txtCName = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtlDesDataCenter = new System.Windows.Forms.TextBox();
            this.labelDesDataCenter = new System.Windows.Forms.Label();
            this.txtOrID = new System.Windows.Forms.TextBox();
            this.lblOrID = new System.Windows.Forms.Label();
            this.txtAcc = new System.Windows.Forms.TextBox();
            this.lblAcc = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.lblCredit = new System.Windows.Forms.Label();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.lblDebit = new System.Windows.Forms.Label();
            this.btnProceed = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.btnProceed);
            this.panel1.Controls.Add(this.txtDebit);
            this.panel1.Controls.Add(this.lblDebit);
            this.panel1.Controls.Add(this.txtCredit);
            this.panel1.Controls.Add(this.lblCredit);
            this.panel1.Controls.Add(this.txtCBNo);
            this.panel1.Controls.Add(this.txtCBrNo);
            this.panel1.Controls.Add(this.lblCBNo);
            this.panel1.Controls.Add(this.lblCBrNo);
            this.panel1.Controls.Add(this.lblCName);
            this.panel1.Controls.Add(this.txtCName);
            this.panel1.Controls.Add(this.txtHeader);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.txtlDesDataCenter);
            this.panel1.Controls.Add(this.labelDesDataCenter);
            this.panel1.Controls.Add(this.txtOrID);
            this.panel1.Controls.Add(this.lblOrID);
            this.panel1.Controls.Add(this.txtAcc);
            this.panel1.Controls.Add(this.lblAcc);
            this.panel1.Controls.Add(this.lblBankType);
            this.panel1.Controls.Add(this.lblBank);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 452);
            this.panel1.TabIndex = 0;
            // 
            // lblBankType
            // 
            this.lblBankType.AutoSize = true;
            this.lblBankType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBankType.Location = new System.Drawing.Point(100, 59);
            this.lblBankType.Name = "lblBankType";
            this.lblBankType.Size = new System.Drawing.Size(110, 16);
            this.lblBankType.TabIndex = 55;
            this.lblBankType.Text = "Bank Type is : ";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBank.Location = new System.Drawing.Point(584, 59);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(111, 16);
            this.lblBank.TabIndex = 52;
            this.lblBank.Text = "Bank Name is :";
            // 
            // txtCBNo
            // 
            this.txtCBNo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCBNo.Location = new System.Drawing.Point(123, 168);
            this.txtCBNo.Name = "txtCBNo";
            this.txtCBNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBNo.TabIndex = 73;
            // 
            // txtCBrNo
            // 
            this.txtCBrNo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCBrNo.Location = new System.Drawing.Point(123, 201);
            this.txtCBrNo.Name = "txtCBrNo";
            this.txtCBrNo.Size = new System.Drawing.Size(100, 20);
            this.txtCBrNo.TabIndex = 72;
            // 
            // lblCBNo
            // 
            this.lblCBNo.AutoSize = true;
            this.lblCBNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCBNo.Location = new System.Drawing.Point(12, 174);
            this.lblCBNo.Name = "lblCBNo";
            this.lblCBNo.Size = new System.Drawing.Size(99, 13);
            this.lblCBNo.TabIndex = 71;
            this.lblCBNo.Text = "Company Bank No.";
            // 
            // lblCBrNo
            // 
            this.lblCBrNo.AutoSize = true;
            this.lblCBrNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCBrNo.Location = new System.Drawing.Point(13, 207);
            this.lblCBrNo.Name = "lblCBrNo";
            this.lblCBrNo.Size = new System.Drawing.Size(108, 13);
            this.lblCBrNo.TabIndex = 70;
            this.lblCBrNo.Text = "Company Branch No.";
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCName.Location = new System.Drawing.Point(12, 141);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(82, 13);
            this.lblCName.TabIndex = 69;
            this.lblCName.Text = "Company Name";
            // 
            // txtCName
            // 
            this.txtCName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCName.Location = new System.Drawing.Point(123, 135);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(100, 20);
            this.txtCName.TabIndex = 68;
            // 
            // txtHeader
            // 
            this.txtHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtHeader.Location = new System.Drawing.Point(688, 222);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(100, 20);
            this.txtHeader.TabIndex = 67;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHeader.Location = new System.Drawing.Point(640, 228);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(42, 13);
            this.lblHeader.TabIndex = 66;
            this.lblHeader.Text = "Header";
            // 
            // txtlDesDataCenter
            // 
            this.txtlDesDataCenter.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtlDesDataCenter.Location = new System.Drawing.Point(688, 189);
            this.txtlDesDataCenter.Name = "txtlDesDataCenter";
            this.txtlDesDataCenter.Size = new System.Drawing.Size(100, 20);
            this.txtlDesDataCenter.TabIndex = 65;
            // 
            // labelDesDataCenter
            // 
            this.labelDesDataCenter.AutoSize = true;
            this.labelDesDataCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDesDataCenter.Location = new System.Drawing.Point(562, 195);
            this.labelDesDataCenter.Name = "labelDesDataCenter";
            this.labelDesDataCenter.Size = new System.Drawing.Size(120, 13);
            this.labelDesDataCenter.TabIndex = 64;
            this.labelDesDataCenter.Text = "Destination Data Center";
            // 
            // txtOrID
            // 
            this.txtOrID.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtOrID.Location = new System.Drawing.Point(384, 137);
            this.txtOrID.Name = "txtOrID";
            this.txtOrID.Size = new System.Drawing.Size(100, 20);
            this.txtOrID.TabIndex = 63;
            // 
            // lblOrID
            // 
            this.lblOrID.AutoSize = true;
            this.lblOrID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrID.Location = new System.Drawing.Point(287, 141);
            this.lblOrID.Name = "lblOrID";
            this.lblOrID.Size = new System.Drawing.Size(91, 13);
            this.lblOrID.TabIndex = 62;
            this.lblOrID.Text = "Originator ID RBC";
            // 
            // txtAcc
            // 
            this.txtAcc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAcc.Location = new System.Drawing.Point(384, 166);
            this.txtAcc.Name = "txtAcc";
            this.txtAcc.Size = new System.Drawing.Size(100, 20);
            this.txtAcc.TabIndex = 60;
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAcc.Location = new System.Drawing.Point(284, 174);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(94, 13);
            this.lblAcc.TabIndex = 58;
            this.lblAcc.Text = "Company Account";
            // 
            // txtCredit
            // 
            this.txtCredit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCredit.Location = new System.Drawing.Point(688, 134);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(100, 20);
            this.txtCredit.TabIndex = 75;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCredit.Location = new System.Drawing.Point(546, 137);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(136, 13);
            this.lblCredit.TabIndex = 74;
            this.lblCredit.Text = "Originator ID BMO CREDIT";
            // 
            // txtDebit
            // 
            this.txtDebit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDebit.Location = new System.Drawing.Point(688, 162);
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new System.Drawing.Size(100, 20);
            this.txtDebit.TabIndex = 77;
            // 
            // lblDebit
            // 
            this.lblDebit.AutoSize = true;
            this.lblDebit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDebit.Location = new System.Drawing.Point(554, 162);
            this.lblDebit.Name = "lblDebit";
            this.lblDebit.Size = new System.Drawing.Size(128, 13);
            this.lblDebit.TabIndex = 76;
            this.lblDebit.Text = "Originator ID BMO DEBIT";
            // 
            // btnProceed
            // 
            this.btnProceed.Location = new System.Drawing.Point(395, 282);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Size = new System.Drawing.Size(75, 23);
            this.btnProceed.TabIndex = 78;
            this.btnProceed.Text = "Proceed";
            this.btnProceed.UseVisualStyleBackColor = true;
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // AddBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddBank";
            this.Text = "ACH";
            this.Load += new System.EventHandler(this.AddBank_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBankType;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.TextBox txtDebit;
        private System.Windows.Forms.Label lblDebit;
        private System.Windows.Forms.TextBox txtCredit;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.TextBox txtCBNo;
        private System.Windows.Forms.TextBox txtCBrNo;
        private System.Windows.Forms.Label lblCBNo;
        private System.Windows.Forms.Label lblCBrNo;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.TextBox txtCName;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtlDesDataCenter;
        private System.Windows.Forms.Label labelDesDataCenter;
        private System.Windows.Forms.TextBox txtOrID;
        private System.Windows.Forms.Label lblOrID;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.Button btnProceed;
    }
}