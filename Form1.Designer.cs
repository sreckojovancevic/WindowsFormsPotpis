using System;
using System.Windows.Forms;

namespace WindowsFormsPotpis
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnChooseFile;
        private Button btnChooseFolder;
        private Button btnSign;
        private TextBox txtSelectedFile;
        private TextBox txtSelectedFolder;
        private TextBox txtLog;
        private ComboBox cbPkcs11Libs;

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
            btnChooseFile = new Button();
            btnChooseFolder = new Button();
            btnSign = new Button();
            txtSelectedFile = new TextBox();
            txtSelectedFolder = new TextBox();
            txtLog = new TextBox();
            cbPkcs11Libs = new ComboBox();
            SuspendLayout();
            // 
            // btnChooseFile
            // 
            btnChooseFile.Location = new Point(23, 23);
            btnChooseFile.Margin = new Padding(4, 3, 4, 3);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(140, 35);
            btnChooseFile.TabIndex = 0;
            btnChooseFile.Text = "Odaberi PDF";
            btnChooseFile.Click += ChooseFile_Click;
            // 
            // btnChooseFolder
            // 
            btnChooseFolder.Location = new Point(23, 69);
            btnChooseFolder.Margin = new Padding(4, 3, 4, 3);
            btnChooseFolder.Name = "btnChooseFolder";
            btnChooseFolder.Size = new Size(140, 35);
            btnChooseFolder.TabIndex = 2;
            btnChooseFolder.Text = "Odaberi folder";
            btnChooseFolder.Click += ChooseFolder_Click;
            // 
            // btnSign
            // 
            btnSign.Location = new Point(23, 162);
            btnSign.Margin = new Padding(4, 3, 4, 3);
            btnSign.Name = "btnSign";
            btnSign.Size = new Size(140, 35);
            btnSign.TabIndex = 5;
            btnSign.Text = "Potpiši PDF";
            btnSign.Click += Sign_Click;
            // 
            // txtSelectedFile
            // 
            txtSelectedFile.Location = new Point(175, 29);
            txtSelectedFile.Margin = new Padding(4, 3, 4, 3);
            txtSelectedFile.Name = "txtSelectedFile";
            txtSelectedFile.ReadOnly = true;
            txtSelectedFile.Size = new Size(611, 23);
            txtSelectedFile.TabIndex = 1;
            // 
            // txtSelectedFolder
            // 
            txtSelectedFolder.Location = new Point(175, 75);
            txtSelectedFolder.Margin = new Padding(4, 3, 4, 3);
            txtSelectedFolder.Name = "txtSelectedFolder";
            txtSelectedFolder.ReadOnly = true;
            txtSelectedFolder.Size = new Size(611, 23);
            txtSelectedFolder.TabIndex = 3;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(23, 208);
            txtLog.Margin = new Padding(4, 3, 4, 3);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(763, 172);
            txtLog.TabIndex = 6;
            // 
            // cbPkcs11Libs
            // 
            cbPkcs11Libs.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPkcs11Libs.Location = new Point(23, 115);
            cbPkcs11Libs.Margin = new Padding(4, 3, 4, 3);
            cbPkcs11Libs.Name = "cbPkcs11Libs";
            cbPkcs11Libs.Size = new Size(763, 23);
            cbPkcs11Libs.TabIndex = 4;
            cbPkcs11Libs.SelectedIndexChanged += Pkcs11Lib_Changed;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 404);
            Controls.Add(btnChooseFile);
            Controls.Add(txtSelectedFile);
            Controls.Add(btnChooseFolder);
            Controls.Add(txtSelectedFolder);
            Controls.Add(cbPkcs11Libs);
            Controls.Add(btnSign);
            Controls.Add(txtLog);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Smart Card PDF Signer";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
