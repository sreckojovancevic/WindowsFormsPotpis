using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsPotpis
{
    public class PinPromptForm : Form
    {
        private TextBox txtPin;
        private Button btnOk;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Sprečava serijalizaciju
        public string EnteredPin { get; private set; } = string.Empty;

        public PinPromptForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtPin = new TextBox();
            this.btnOk = new Button();

            // txtPin
            this.txtPin.PasswordChar = '*';
            this.txtPin.Location = new System.Drawing.Point(20, 20);
            this.txtPin.Size = new System.Drawing.Size(200, 20);

            // btnOk
            this.btnOk.Location = new System.Drawing.Point(20, 50);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            // PinPromptForm
            this.ClientSize = new System.Drawing.Size(240, 90);
            this.Controls.Add(this.txtPin);
            this.Controls.Add(this.btnOk);
            this.Text = "Unesite PIN";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            EnteredPin = txtPin.Text.Trim(); // Sprečava prazan unos i razmake
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
