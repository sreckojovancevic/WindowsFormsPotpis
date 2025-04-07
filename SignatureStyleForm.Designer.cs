namespace WindowsFormsPotpis
{
    partial class SignatureStyleForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.NumericUpDown numBorderWidth;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Button btnBackgroundColor;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.numBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.btnBackgroundColor = new System.Windows.Forms.Button();
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPreview = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).BeginInit();

            // numFontSize
            this.numFontSize.Location = new System.Drawing.Point(30, 30);
            this.numFontSize.Minimum = 6;
            this.numFontSize.Maximum = 72;
            this.numFontSize.Value = 10;

            // numBorderWidth
            this.numBorderWidth.Location = new System.Drawing.Point(30, 70);
            this.numBorderWidth.Minimum = 0;
            this.numBorderWidth.Maximum = 10;
            this.numBorderWidth.Value = 1;

            // txtReason
            this.txtReason.Location = new System.Drawing.Point(30, 110);
            this.txtReason.Width = 200;
            this.txtReason.PlaceholderText = "Razlog potpisa";

            // txtLocation
            this.txtLocation.Location = new System.Drawing.Point(30, 150);
            this.txtLocation.Width = 200;
            this.txtLocation.PlaceholderText = "Lokacija";

            // btnFontColor
            this.btnFontColor.Location = new System.Drawing.Point(30, 190);
            this.btnFontColor.Text = "Font boja";

            // btnBackgroundColor
            this.btnBackgroundColor.Location = new System.Drawing.Point(130, 190);
            this.btnBackgroundColor.Text = "Pozadina";

            // btnBorderColor
            this.btnBorderColor.Location = new System.Drawing.Point(230, 190);
            this.btnBorderColor.Text = "Ivica";

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(30, 240);
            this.btnOK.Text = "OK";

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(130, 240);
            this.btnCancel.Text = "Otkaži";

            // lblPreview
            this.lblPreview.Location = new System.Drawing.Point(30, 280);
            this.lblPreview.Size = new System.Drawing.Size(250, 60);
            this.lblPreview.Text = "Potpisao: Primer Imena";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Form
            this.ClientSize = new System.Drawing.Size(350, 360);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.numBorderWidth);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.btnBackgroundColor);
            this.Controls.Add(this.btnBorderColor);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPreview);
            this.Text = "Personalizuj potpis";

            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
