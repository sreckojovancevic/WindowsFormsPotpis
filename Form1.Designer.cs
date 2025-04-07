namespace WindowsFormsPotpis
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPkcs11LibPath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Button btnChoosePkcs11Lib;
        private System.Windows.Forms.Button btnRefreshPkcs11Libs;
        private System.Windows.Forms.TextBox txtSelectedFile;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.ComboBox cbPkcs11Libs;
        private System.Windows.Forms.ComboBox cbCertificates; // ComboBox for certificates
        private System.Windows.Forms.Button btnRefreshCertificates;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnChooseSignature;
        private System.Windows.Forms.CheckBox chkTimestamp;
        private System.Windows.Forms.Button btnRefreshPkcs11Certificates;
        private System.Windows.Forms.Button btnSignWithPkcs11;
        private Button btnSetSignaturePosition;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnCustomizeSignatureStyle;




        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtPkcs11LibPath = new TextBox();
            btnSavePath = new Button();
            btnChoosePkcs11Lib = new Button();
            btnRefreshPkcs11Libs = new Button();
            txtSelectedFile = new TextBox();
            btnChooseFile = new Button();
            btnSign = new Button();
            cbPkcs11Libs = new ComboBox();
            cbCertificates = new ComboBox();
            btnRefreshCertificates = new Button();
            txtLog = new TextBox();
            btnChooseSignature = new Button();
            chkTimestamp = new CheckBox();
            cbPkcs11Certificates = new ComboBox();
            btnRefreshPkcs11Certificates = new Button();
            btnSignWithPkcs11 = new Button();
            txtOutputFile = new TextBox();
            btnChooseOutput = new Button();
            btnSetSignaturePosition = new Button();
            toolTip = new ToolTip(components);
            btnCustomizeSignatureStyle = new Button();
            SuspendLayout();
            // 
            // txtPkcs11LibPath
            // 
            txtPkcs11LibPath.Location = new Point(23, 23);
            txtPkcs11LibPath.Name = "txtPkcs11LibPath";
            txtPkcs11LibPath.Size = new Size(349, 23);
            txtPkcs11LibPath.TabIndex = 0;
            // 
            // btnSavePath
            // 
            btnSavePath.Location = new Point(385, 23);
            btnSavePath.Name = "btnSavePath";
            btnSavePath.Size = new Size(88, 27);
            btnSavePath.TabIndex = 1;
            btnSavePath.Text = "Sačuvaj";
            btnSavePath.UseVisualStyleBackColor = true;
            btnSavePath.Click += btnSavePath_Click;
            // 
            // btnChoosePkcs11Lib
            // 
            btnChoosePkcs11Lib.Location = new Point(490, 23);
            btnChoosePkcs11Lib.Name = "btnChoosePkcs11Lib";
            btnChoosePkcs11Lib.Size = new Size(88, 27);
            btnChoosePkcs11Lib.TabIndex = 2;
            btnChoosePkcs11Lib.Text = "Izaberi";
            btnChoosePkcs11Lib.UseVisualStyleBackColor = true;
            btnChoosePkcs11Lib.Click += btnChoosePkcs11Lib_Click;
            // 
            // btnRefreshPkcs11Libs
            // 
            btnRefreshPkcs11Libs.Location = new Point(583, 23);
            btnRefreshPkcs11Libs.Name = "btnRefreshPkcs11Libs";
            btnRefreshPkcs11Libs.Size = new Size(105, 27);
            btnRefreshPkcs11Libs.TabIndex = 3;
            btnRefreshPkcs11Libs.Text = "Osveži lib.";
            btnRefreshPkcs11Libs.UseVisualStyleBackColor = true;
            btnRefreshPkcs11Libs.Click += btnRefreshPkcs11Libs_Click;
            // 
            // txtSelectedFile
            // 
            txtSelectedFile.Location = new Point(23, 140);
            txtSelectedFile.Name = "txtSelectedFile";
            txtSelectedFile.Size = new Size(349, 23);
            txtSelectedFile.TabIndex = 7;
            // 
            // btnChooseFile
            // 
            btnChooseFile.Location = new Point(385, 140);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(88, 27);
            btnChooseFile.TabIndex = 8;
            btnChooseFile.Text = "Izaberi fajl";
            toolTip.SetToolTip(btnChooseFile, "Odaberite PDF fajl koji želite da potpišete.");
            btnChooseFile.UseVisualStyleBackColor = true;
            btnChooseFile.Click += btnChooseFile_Click;
            // 
            // btnSign
            // 
            btnSign.Location = new Point(490, 140);
            btnSign.Name = "btnSign";
            btnSign.Size = new Size(88, 27);
            btnSign.TabIndex = 9;
            btnSign.Text = "Potpiši";
            toolTip.SetToolTip(btnSign, "Pokreni proces digitalnog potpisa.");
            btnSign.UseVisualStyleBackColor = true;
            btnSign.Click += btnSign_Click;
            // 
            // cbPkcs11Libs
            // 
            cbPkcs11Libs.Location = new Point(23, 60);
            cbPkcs11Libs.Name = "cbPkcs11Libs";
            cbPkcs11Libs.Size = new Size(790, 23);
            cbPkcs11Libs.TabIndex = 4;
            cbPkcs11Libs.SelectedIndexChanged += cbPkcs11Libs_SelectedIndexChanged;
            // 
            // cbCertificates
            // 
            cbCertificates.Location = new Point(23, 100);
            cbCertificates.Name = "cbCertificates";
            cbCertificates.Size = new Size(790, 23);
            cbCertificates.TabIndex = 5;
            toolTip.SetToolTip(cbCertificates, "Izaberite digitalni sertifikat iz Windows skladišta.");
            cbCertificates.SelectedIndexChanged += cbCertificates_SelectedIndexChanged;
            // 
            // btnRefreshCertificates
            // 
            btnRefreshCertificates.Location = new Point(708, 23);
            btnRefreshCertificates.Name = "btnRefreshCertificates";
            btnRefreshCertificates.Size = new Size(105, 27);
            btnRefreshCertificates.TabIndex = 6;
            btnRefreshCertificates.Text = "Osveži cert.";
            btnRefreshCertificates.UseVisualStyleBackColor = true;
            btnRefreshCertificates.Click += btnRefreshCertificates_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(839, 26);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(539, 362);
            txtLog.TabIndex = 10;
            // 
            // btnChooseSignature
            // 
            btnChooseSignature.Location = new Point(464, 219);
            btnChooseSignature.Name = "btnChooseSignature";
            btnChooseSignature.Size = new Size(171, 27);
            btnChooseSignature.TabIndex = 11;
            btnChooseSignature.Text = "Izaberi svojerucni potpis";
            toolTip.SetToolTip(btnChooseSignature, "Odaberite PNG sliku sa vašim potpisom (opciono).");
            btnChooseSignature.UseVisualStyleBackColor = true;
            btnChooseSignature.Click += btnChooseSignature_Click;
            // 
            // chkTimestamp
            // 
            chkTimestamp.Location = new Point(653, 222);
            chkTimestamp.Name = "chkTimestamp";
            chkTimestamp.Size = new Size(120, 24);
            chkTimestamp.TabIndex = 12;
            chkTimestamp.Text = "Dodaj vremensku oznaku";
            // 
            // cbPkcs11Certificates
            // 
            cbPkcs11Certificates.Location = new Point(23, 260);
            cbPkcs11Certificates.Name = "cbPkcs11Certificates";
            cbPkcs11Certificates.Size = new Size(790, 23);
            cbPkcs11Certificates.TabIndex = 0;
            // 
            // btnRefreshPkcs11Certificates
            // 
            btnRefreshPkcs11Certificates.Location = new Point(23, 300);
            btnRefreshPkcs11Certificates.Name = "btnRefreshPkcs11Certificates";
            btnRefreshPkcs11Certificates.Size = new Size(170, 27);
            btnRefreshPkcs11Certificates.TabIndex = 1;
            btnRefreshPkcs11Certificates.Text = "Osveži PKCS#11 cert.";
            btnRefreshPkcs11Certificates.Click += btnRefreshPkcs11Certificates_Click;
            // 
            // btnSignWithPkcs11
            // 
            btnSignWithPkcs11.Location = new Point(210, 300);
            btnSignWithPkcs11.Name = "btnSignWithPkcs11";
            btnSignWithPkcs11.Size = new Size(170, 27);
            btnSignWithPkcs11.TabIndex = 2;
            btnSignWithPkcs11.Text = "Potpiši sa PKCS#11";
            btnSignWithPkcs11.Click += btnSignWithPkcs11_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(23, 180);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(349, 23);
            txtOutputFile.TabIndex = 3;
            // 
            // btnChooseOutput
            // 
            btnChooseOutput.Location = new Point(385, 180);
            btnChooseOutput.Name = "btnChooseOutput";
            btnChooseOutput.Size = new Size(88, 27);
            btnChooseOutput.TabIndex = 4;
            btnChooseOutput.Text = "Sačuvaj kao";
            btnChooseOutput.Click += btnChooseOutput_Click;
            // 
            // btnSetSignaturePosition
            // 
            btnSetSignaturePosition.Location = new Point(23, 220);
            btnSetSignaturePosition.Name = "btnSetSignaturePosition";
            btnSetSignaturePosition.Size = new Size(200, 30);
            btnSetSignaturePosition.TabIndex = 0;
            btnSetSignaturePosition.Text = "📍 Postavi poziciju potpisa";
            toolTip.SetToolTip(btnSetSignaturePosition, "Otvorite prikaz PDF-a i izaberite gde da stavite potpis.");
            btnSetSignaturePosition.Click += btnSetSignaturePosition_Click;
            // 
            // btnCustomizeSignatureStyle
            // 
            btnCustomizeSignatureStyle.Location = new Point(608, 140);
            btnCustomizeSignatureStyle.Name = "btnCustomizeSignatureStyle";
            btnCustomizeSignatureStyle.Size = new Size(150, 30);
            btnCustomizeSignatureStyle.TabIndex = 0;
            btnCustomizeSignatureStyle.Text = "Personalizuj potpis";
            btnCustomizeSignatureStyle.UseVisualStyleBackColor = true;
            btnCustomizeSignatureStyle.Click += btnCustomizeSignatureStyle_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 589);
            Controls.Add(btnCustomizeSignatureStyle);
            Controls.Add(cbPkcs11Certificates);
            Controls.Add(btnRefreshPkcs11Certificates);
            Controls.Add(btnSignWithPkcs11);
            Controls.Add(txtOutputFile);
            Controls.Add(btnChooseOutput);
            Controls.Add(btnSetSignaturePosition);
            Controls.Add(txtPkcs11LibPath);
            Controls.Add(btnSavePath);
            Controls.Add(btnChoosePkcs11Lib);
            Controls.Add(btnRefreshPkcs11Libs);
            Controls.Add(cbPkcs11Libs);
            Controls.Add(cbCertificates);
            Controls.Add(btnRefreshCertificates);
            Controls.Add(txtSelectedFile);
            Controls.Add(btnChooseFile);
            Controls.Add(btnSign);
            Controls.Add(txtLog);
            Controls.Add(btnChooseSignature);
            Controls.Add(chkTimestamp);
            Name = "Form1";
            Text = "Aplikacija za potpisivanje PDF-a";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}